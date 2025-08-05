namespace JobBoard.Core.Feutures.Skills.Queries.Results
{
	public class GetListSkillsQueryResponse
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }
		public string CreateDate { get; set; }
	}
}
