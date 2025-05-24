using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Applications.Queries.Models;
using JobBoard.Core.Feutures.Applications.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Applications.Queries.Handler
{
	public class ApplicationQueryHandler : ResponseHandler,
				IRequestHandler<GetSingleApplicationQuery, Response<GetSingleApplictionQueryResponse>>,
				IRequestHandler<GetApplicationsByJobIdQuery, Response<GetApplicationByJobIdQueryResponse>>
	{
		private readonly IApplicationService _applicationService;
		private readonly IMapper _mapper;
		private readonly IJobService _jobService;

		#region Fields 
		#endregion

		#region Constructors 
		public ApplicationQueryHandler(IApplicationService applicationService,
										IMapper mapper,
										IStringLocalizer<SharedResources> stringLocalizer,
										IJobService jobService
										) : base(stringLocalizer)
		{
			_applicationService = applicationService;
			_mapper = mapper;
			_jobService = jobService;
		}
		#endregion

		#region Handle Methods
		public async Task<Response<GetSingleApplictionQueryResponse>> Handle(GetSingleApplicationQuery request, CancellationToken cancellationToken)
		{
			var application = await _applicationService.GetByIdWithIncludeAsync(request.Id);

			if (application is null) return NotFound<GetSingleApplictionQueryResponse>();

			return Success(_mapper.Map<GetSingleApplictionQueryResponse>(application));

		}

		public async Task<Response<GetApplicationByJobIdQueryResponse>> Handle(GetApplicationsByJobIdQuery request, CancellationToken cancellationToken)
		{
			// check the existense of the job

			var exist = await _jobService.IsExistByIdAsync(request.JobId);
			if (!exist) return NotFound<GetApplicationByJobIdQueryResponse>();

			throw new NotImplementedException();
		}

		#endregion

	}
}
