using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Commands.Models;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Core.Feutures.ApplicationUser.Commands.Handler
{
	public class UserCommandHandler : ResponseHandler,
			IRequestHandler<AddUserCommand, Response<int>>,
			IRequestHandler<UpdateUserCommand, Response<string>>
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
						IUserService userService)
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

			var Exist = await _userManager.Users.AnyAsync(x => x.Email == request.Email);

			if (Exist) return BadRequest<int>("Email Already exist!");

			//var UserByUsername = await _userManager.FindByNameAsync(request.UserName);
			Exist = await _userManager.Users.AnyAsync(x => x.UserName == request.UserName);

			if (Exist) return BadRequest<int>("Username Already Exits");

			var TargetUser = _mapper.Map<User>(request);

			// get user country Id 
			TargetUser.CountryId = await _countryService.GetCountryIdAsync(request.CountryName);

			var result = await _userManager.CreateAsync(TargetUser, request.Password);

			if (!result.Succeeded)
			{
				return BadRequest<int>(result?.Errors?.FirstOrDefault()?.Description);
			}

			return Created(TargetUser.Id);

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

		#endregion
	}
}
