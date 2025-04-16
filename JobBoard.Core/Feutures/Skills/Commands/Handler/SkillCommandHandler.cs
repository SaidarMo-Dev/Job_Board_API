using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Skills.Commands.Models;
using JobBoard.Data.Entities;
using JobBoard.Service.Abstractions;
using MediatR;

namespace JobBoard.Core.Feutures.Skills.Commands.Handler
{
	public class SkillCommandHandler : ResponseHandler,
									   IRequestHandler<AddSkillCommand, Response<int>>,
									   IRequestHandler<UpdateSkillCommand, Response<string>>,
									   IRequestHandler<DeleteSkillCommand, Response<string>>
	{

		#region Fiedls
		private readonly ISkillService _skillService;
		private readonly IMapper _mapper;
		#endregion

		#region Constructors
		public SkillCommandHandler(ISkillService skillService, IMapper mapper)
		{
			_skillService = skillService;
			_mapper = mapper;
		}

		#endregion

		#region Handels
		public async Task<Response<int>> Handle(AddSkillCommand request, CancellationToken cancellationToken)
		{
			var skill = _mapper.Map<Skill>(request);

			skill = await _skillService.AddNewSkillAsync(skill);

			return Created(skill.SkillId);
		}

		public async Task<Response<string>> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
		{
			//bool Exist = await _skillService.IsExistByIdAsync(request.SkillId);

			var skill = await _skillService.GetSkillByIdAsync(request.SkillId);

			if (skill == null) return NotFound<string>("Not Found");

			skill = _mapper.Map(request, skill);

			await _skillService.UpdateAsnyc(skill);

			return Success("");
		}

		public async Task<Response<string>> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
		{
			var skill = await _skillService.GetSkillByIdAsync(request.Id);
			if (skill == null) return NotFound<string>("NotFound");

			await _skillService.DeleteAsync(skill);

			return Deleted<string>();
		}

		#endregion
	}
}
