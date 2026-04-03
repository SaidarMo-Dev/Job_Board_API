using JobBoard.Core.Common.DTOs;

namespace JobBoard.Core.Feutures.ApplicationUser.Queries.Responses
{
	public class GetCurrentUserProfileResponse
	{
		public BasicUserInfoDto BasicInfo { get; set; } = null!;
		public List<string> Roles { get; set; } = new();

		public List<SkillDto> Skills { get; set; } = new();
		public List<ExperienceDto> Experiences { get; set; } = new();
		public List<CertificationDto> Certifications { get; set; } = new();

		public List<EducationDto> Educations { get; set; } = new();
		public List<LanguageDto> Languages { get; set; } = new();

		public JobPreferenceDto? JobPreferences { get; set; }
		public ProfileStatsDto? Stats { get; set; }
	}
}
