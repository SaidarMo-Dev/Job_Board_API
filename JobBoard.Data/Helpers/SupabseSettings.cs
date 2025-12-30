namespace JobBoard.Data.Helpers
{

	public class SupabaseSettings
	{
		public string Url { get; set; } = null!;
		public string ServiceKey { get; set; } = null!;
		public string BucketName { get; set; } = null!;
		public int SignedUrlExpirySeconds { get; set; }
		public int MaxFileSizeBytes { get; set; }
	}


}
