using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Applications.Queries.Models;
using JobBoard.Core.Feutures.Applications.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities.Identity;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using Serilog;

namespace JobBoard.Core.Feutures.Applications.Queries.Handler
{
	public class ApplicationQueryHandler : ResponseHandler,
				IRequestHandler<GetSingleApplicationQuery, Response<GetSingleApplictionQueryResponse>>
	{
		private readonly IApplicationService _applicationService;
		private readonly IMapper _mapper;

		private readonly ICurrentUserService _currentUserService;
		private readonly User _currentUser;

		#region Fields 
		#endregion

		#region Constructors 
		public ApplicationQueryHandler(IApplicationService applicationService,
										IMapper mapper,
										IStringLocalizer<SharedResources> stringLocalizer,

										ICurrentUserService currentUserService) : base(stringLocalizer)
		{
			_applicationService = applicationService;
			_mapper = mapper;

			this._currentUserService = currentUserService;
			_currentUser = currentUserService.GetCurrentUser();
		}
		#endregion

		#region Handle Methods
		public async Task<Response<GetSingleApplictionQueryResponse>> Handle(GetSingleApplicationQuery request, CancellationToken cancellationToken)
		{
			var application = await _applicationService.GetByIdWithIncludeAsync(request.Id);

			if (application is null) return NotFound<GetSingleApplictionQueryResponse>();
			Log.Information("Application Retrived By user :" + _currentUser.FullName);

			return Success(_mapper.Map<GetSingleApplictionQueryResponse>(application));

		}
		#endregion

	}
}
