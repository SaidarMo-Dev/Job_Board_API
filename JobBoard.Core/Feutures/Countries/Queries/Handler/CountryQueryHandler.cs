using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Countries.Queries.Models;
using JobBoard.Core.Feutures.Countries.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Countries.Queries.Handler
{
	public class CountryQueryHandler : ResponseHandler,
						IRequestHandler<GetCountryByIdQuery, Response<GetCountryByIdQueryResponse>>,
						IRequestHandler<GetListCountriesQuery, Response<List<ListCountriesQueryResponse>>>
	{
		#region Fields
		private readonly ICountryService _countryService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion

		#region Constructors
		public CountryQueryHandler(ICountryService countryService,
								IMapper mapper,
								IStringLocalizer<SharedResources> stringLocalizer)
									: base(stringLocalizer)
		{
			_countryService = countryService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}


		#endregion

		#region Handlers
		public async Task<Response<GetCountryByIdQueryResponse>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
		{
			var country = await _countryService.GetCountryByIdAsync(request.Id);

			if (country == null) return NotFound<GetCountryByIdQueryResponse>();

			return Success(new GetCountryByIdQueryResponse { Id = country.CountryId, CountryName = country.CountryName });
		}

		public async Task<Response<List<ListCountriesQueryResponse>>> Handle(GetListCountriesQuery request, CancellationToken cancellationToken)
		{
			var countries = await _countryService.GetAllAsync();

			var countriesDto = _mapper.Map<List<ListCountriesQueryResponse>>(countries);

			return Success(countriesDto);
		}

		#endregion

	}
}
