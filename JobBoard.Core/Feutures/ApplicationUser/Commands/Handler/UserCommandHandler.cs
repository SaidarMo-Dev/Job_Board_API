using System.Security.Claims;
using AutoMapper;
using JobBoard.Core.Authrization.Requirements;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Commands.Models;
using JobBoard.Core.Feutures.Files.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.enums;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Handler
{
	public class UserCommandHandler : ResponseHandler,
			IRequestHandler<AddUserCommand, Response<int>>,
			IRequestHandler<UpdateUserCommand, Response<string>>,
			IRequestHandler<DeleteUserCommand, Response<string>>,
			IRequestHandler<SetUserProfileImageCommand, Response<string>>

	{

		#region Fields 
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly ICountryService _countryService;
		private readonly IUserService _userService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthorizationService _authorizationService;
		private readonly IAuthenticationService _authenticationService;
		private readonly IFileStorageService _fileStorageService;
		private readonly IMediator _mediator;
		#endregion

		#region Construtors
		public UserCommandHandler(
			UserManager<User> userManager,
			IMapper mapper,
			ICountryService countryService,
			IUserService userService,
			IStringLocalizer<SharedResources> stringLocalizer,
			IAuthorizationService authorizationService,
			IAuthenticationService authenticationService,
			IFileStorageService fileStrageService,
			IMediator mediator

			) : base(stringLocalizer)
		{
			_userManager = userManager;
			_mapper = mapper;
			_countryService = countryService;
			_userService = userService;
			_stringLocalizer = stringLocalizer;
			_authorizationService = authorizationService;
			_authenticationService = authenticationService;
			_fileStorageService = fileStrageService;
			_mediator = mediator;
		}

		#endregion

		#region Handle Methods
		public async Task<Response<int>> Handle(AddUserCommand request, CancellationToken cancellationToken)
		{

			// verify the username and email to be unique
			var Exist = await _userManager.Users.AnyAsync(x => x.Email == request.Email);

			if (Exist) return BadRequest<int>(_stringLocalizer[SharedResourcesKeys.EmailExist]);

			if (Exist) return BadRequest<int>("Username Already Exits");

			// map request with user
			var user = _mapper.Map<User>(request);

			user.UserName = $"{user.FirstName}{Guid.NewGuid().ToString("N").Substring(0, 8)}";

			var result = await _userService.AddNewUserAsync(user, request.Password, request.Role);

			if (!(result == "Success")) return BadRequest<int>(result);


			return Created(user.Id);

		}
		public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var OldUser = await _userManager.FindByIdAsync(request.Id.ToString());

			var isAuthorized = await _authorizationService.AuthorizeAsync(new ClaimsPrincipal(), OldUser, new SameUserRequirement());

			if (!isAuthorized.Succeeded) return Forbidden<string>();

			if (OldUser == null) return NotFound("");

			var newUser = _mapper.Map(request, OldUser);

			newUser.CountryId = string.IsNullOrEmpty(request.CountryName) ? null : await _countryService.GetIdByNameAsync(request.CountryName);

			var result = await _userManager.UpdateAsync(newUser);


			if (!result.Succeeded)
			{
				return BadRequest<string>("Cannot Update User :" + result?.Errors?
							.FirstOrDefault()?.Description);
			}


			return Success("Success");
		}

		public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.Id.ToString());

			var isAuthorized = await _authorizationService.AuthorizeAsync(new ClaimsPrincipal(), user, new SameUserRequirement());

			if (!isAuthorized.Succeeded) return Forbidden<string>();


			if (user == null) return NotFound("");

			var IsDeleted = await _userService.DeleteUsersAsync(user);
			if (!IsDeleted) return BadRequest<string>("Cannot delete this User");

			return Success("");
		}

		public async Task<Response<string>> Handle(SetUserProfileImageCommand request, CancellationToken cancellationToken)
		{

			var user = await _userManager.FindByIdAsync(request.UserId.ToString());

			if (user == null) return NotFound("User not found");


			var result = await _mediator.Send(
				new UploadFileCommand(
					request.ProfileImage,
					FileOwnerType.Users,
					user.Id,
					FileVisibility.Private,
					FilePathType.UuidFileName
					)
				);



			if (!(result.statusCode == System.Net.HttpStatusCode.OK))
			{
				return BadRequest<string>(result.message);
			}

			user.ProfileImageFileId = result.data;

			await _userService.UpdateUserAsync(user);

			return Success(result.data.ToString());
		}



		#endregion
	}
}
