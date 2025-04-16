namespace JobBoard.Core.Feutures.Jobs.Queries.Responses
{
	public class GetJobSkillsQueryResponse
	{
		public int SkillId { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }

	}
}
