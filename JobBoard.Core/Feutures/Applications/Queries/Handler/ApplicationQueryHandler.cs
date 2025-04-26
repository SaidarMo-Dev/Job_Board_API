using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Applications.Queries.Models;
using JobBoard.Core.Feutures.Applications.Queries.Responses;
using JobBoard.Service.Abstractions;
using MediatR;

namespace JobBoard.Core.Feutures.Applications.Queries.Handler
{
	public class ApplicationQueryHandler : ResponseHandler,
				IRequestHandler<GetSingleApplicationQuery, Response<GetSingleApplictionQueryResponse>>
	{
		private readonly IApplicationService _applicationService;
		private readonly IMapper _mapper;
		#region Fields 
		#endregion

		#region Constructors 
		public ApplicationQueryHandler(IApplicationService applicationService, IMapper mapper)
		{
			_applicationService = applicationService;
			_mapper = mapper;
		}
		#endregion

		#region Handle Methods
		public async Task<Response<GetSingleApplictionQueryResponse>> Handle(GetSingleApplicationQuery request, CancellationToken cancellationToken)
		{
			var application = await _applicationService.GetByIdWithIncludeAsync(request.Id);

			if (application is null) return NotFound<GetSingleApplictionQueryResponse>();

			return Success(_mapper.Map<GetSingleApplictionQueryResponse>(application));


		}
		#endregion

	}
}
