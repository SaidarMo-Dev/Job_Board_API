using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Admin.Command.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Admin.Command.Handlers
{
	public class AdminCommandHandler : ResponseHandler,
					IRequestHandler<AdminAddUserCommand, Response<int>>,
					IRequestHandler<AdminUpdateUserCommand, Response<string>>
	{
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#region Fields

		#endregion

		#region Constructors
		public AdminCommandHandler(UserManager<User> userManager, IMapper mapper, IUserService userService, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_userManager = userManager;
			_mapper = mapper;
			_userService = userService;
			_stringLocalizer = stringLocalizer;
		}

		#endregion

		#region Methods
		public async Task<Response<int>> Handle(AdminAddUserCommand request, CancellationToken cancellationToken)
		{
			// verify the username and email to be unique
			var Exist = await _userManager.Users.AnyAsync(x => x.Email == request.Email);

			if (Exist) return BadRequest<int>(_stringLocalizer[SharedResourcesKeys.EmailExist]);

			// map request with user
			var user = _mapper.Map<User>(request);

			user.UserName = $"{user.FirstName}{Guid.NewGuid().ToString("N").Substring(0, 8)}";

			var result = await _userService.AddNewUserAsync(user, request.Password, request.Role);

			if (!(result == "Success")) return BadRequest<int>(result);


			return Created(user.Id);
		}

		public async Task<Response<string>> Handle(AdminUpdateUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.Id.ToString());

			if (user is null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);

			var userToUpdate = _mapper.Map(request, user);

			var updateResult = await _userService.AdminUpdateUserAsync(userToUpdate, request.Role);

			if (!updateResult.Succeeded) return BadRequest<string>(updateResult.Errors.FirstOrDefault()?.Description ?? "Failed to updae user");

			return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
		}

		#endregion
	}
}
