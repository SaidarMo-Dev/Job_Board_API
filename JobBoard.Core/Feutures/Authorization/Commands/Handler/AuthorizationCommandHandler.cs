using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Authorization.Commands.Models;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Core.Feutures.Authorization.Commands.Handler
{
	public class AuthorizationCommandHandler : ResponseHandler,
					IRequestHandler<AddRoleCommand, Response<int>>,
					IRequestHandler<UpdateRoleCommand, Response<string>>,
					IRequestHandler<DeleteRoleCommand, Response<string>>
	{
		#region Fields 
		private readonly IAuthorizationService _authorizationService;
		private readonly RoleManager<Role> _roleManager;

		#endregion



		#region Constructors
		public AuthorizationCommandHandler(IAuthorizationService authorizationService,
									RoleManager<Role> roleManager
									)
		{
			_authorizationService = authorizationService;
			_roleManager = roleManager;
		}
		#endregion


		#region Handle Methods
		public async Task<Response<int>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.AddRoleAsync(request.RoleName);
			if (result == -1) return BadRequest<int>();

			return Success(result);
		}

		public async Task<Response<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
		{
			// check the role if exists
			var role = await _roleManager.FindByIdAsync(request.Id.ToString());
			if (role is null) return NotFound<string>();

			// update roleName
			role.Name = request.RoleName;

			var result = await _roleManager.UpdateAsync(role);
			if (!result.Succeeded)
				return BadRequest<string>(result.Errors.FirstOrDefault().Description);

			return Success<string>();


		}

		public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
		{
			var role = await _roleManager.FindByIdAsync(request.Id.ToString());
			if (role is null) return NotFound<string>();

			var result = await _roleManager.DeleteAsync(role);
			if (!result.Succeeded)
				return BadRequest<string>(result.Errors.FirstOrDefault().Description);

			return Deleted<string>();

		}

		#endregion

	}
}
