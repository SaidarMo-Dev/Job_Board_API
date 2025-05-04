using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Abstractions;
using MediatR;
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
		#endregion

		#region Construtors
		public UserCommandHandler(UserManager<User> userManager,
		IMapper mapper,
		ICountryService countryService,
						IUserService userService,
						IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_userManager = userManager;
			_mapper = mapper;
			_countryService = countryService;
			_userService = userService;
		}

		#endregion

		#region Handle Methods
		public async Task<Response<int>> Handle(AddUserCommand request, CancellationToken cancellationToken)
		{

			// verify the username and email to be unique
			var Exist = await _userManager.Users.AnyAsync(x => x.Email == request.Email);

			if (Exist) return BadRequest<int>("Email Already exist!");

			Exist = await _userManager.Users.AnyAsync(x => x.UserName == request.UserName);

			if (Exist) return BadRequest<int>("Username Already Exits");

			// map request with user
			var user = _mapper.Map<User>(request);

			// get user country Id 
			user.CountryId = await _countryService.GetCountryIdAsync(request.CountryName);

			var result = await _userManager.CreateAsync(user, request.Password);

			if (!result.Succeeded)
				return BadRequest<int>(result?.Errors?.FirstOrDefault()?.Description);

			await _userManager.AddToRoleAsync(user, "User");

			return Created(user.Id);

		}

		public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var OldUser = await _userManager.FindByIdAsync(request.Id.ToString());
			if (OldUser == null) return NotFound("");

			var newUser = _mapper.Map(request, OldUser);
			var result = await _userManager.UpdateAsync(newUser);

			if (!result.Succeeded)
				return BadRequest<string>("Cannot Update User :" + result?.Errors?.FirstOrDefault()?.Description);

			return Success("");
		}

		public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.Id.ToString());
			if (user == null) return NotFound("");

			//var result = await _userManager.DeleteAsync(user);

			//if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);

			var IsDeleted = await _userService.DeleteUsersAsync(user);
			if (!IsDeleted) return BadRequest<string>("Cannot delete this User");

			return Success("");
		}

		public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
		{

			var user = await _userManager.FindByIdAsync(request.Id.ToString());
			if (user == null) return NotFound("");

			//var passwordCorrect = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
			//if (!passwordCorrect) return BadRequest<string>("Wrong Id Or Password!");

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
