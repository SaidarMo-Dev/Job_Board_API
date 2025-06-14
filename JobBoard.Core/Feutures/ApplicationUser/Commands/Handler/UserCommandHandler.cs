using System.Security.Claims;
using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Core.Security.Requirements;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Abstractions;
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
			IRequestHandler<ChangeUserPasswordCommand, Response<string>>
	{

		#region Fields 
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly ICountryService _countryService;
		private readonly IUserService _userService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthorizationService _authorizationService;
		#endregion

		#region Construtors
		public UserCommandHandler(UserManager<User> userManager,
						IMapper mapper,
						ICountryService countryService,
						IUserService userService,
						IStringLocalizer<SharedResources> stringLocalizer,
						IAuthorizationService authorizationService
			) : base(stringLocalizer)
		{
			_userManager = userManager;
			_mapper = mapper;
			_countryService = countryService;
			_userService = userService;
			_stringLocalizer = stringLocalizer;
			_authorizationService = authorizationService;
		}

		#endregion

		#region Handle Methods
		public async Task<Response<int>> Handle(AddUserCommand request, CancellationToken cancellationToken)
		{

			// verify the username and email to be unique
			var Exist = await _userManager.Users.AnyAsync(x => x.Email == request.Email);

			if (Exist) return BadRequest<int>(_stringLocalizer[SharedResourcesKeys.EmailExist]);

			Exist = await _userManager.Users.AnyAsync(x => x.UserName == request.UserName);

			if (Exist) return BadRequest<int>("Username Already Exits");

			// map request with user
			var user = _mapper.Map<User>(request);


			//var result = await _userManager.CreateAsync(user, request.Password);
			var result = await _userService.AddNewUserAsync(user, request.Password, request.Role);

			if (!(result == "Success")) return BadRequest<int>(result);


			//await _userManager.AddToRoleAsync(user, request.Role);

			return Created(user.Id);

		}
		public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var OldUser = await _userManager.FindByIdAsync(request.Id.ToString());

			var isAuthorized = await _authorizationService.AuthorizeAsync(new ClaimsPrincipal(), OldUser, new SameUserRequirement());

			if (!isAuthorized.Succeeded) return Forbidden<string>();

			if (OldUser == null) return NotFound("");

			var newUser = _mapper.Map(request, OldUser);
			var result = await _userManager.UpdateAsync(newUser);

			if (!result.Succeeded)
			{
				return BadRequest<string>("Cannot Update User :" + result?.Errors?
							.FirstOrDefault()?.Description);
			}


			return Success<string>();
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

		public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
		{

			var user = await _userManager.FindByIdAsync(request.Id.ToString());
			if (user == null) return NotFound("");

			var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
			if (!result.Succeeded)
			{
				return BadRequest<string>(result.Errors.FirstOrDefault().Description);

			}

			return Success<string>();
		}

		#endregion
	}
}
