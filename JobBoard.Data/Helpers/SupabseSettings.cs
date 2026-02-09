namespace JobBoard.Data.Helpers
{

	public class SupabaseSettings
	{
		public string Url { get; set; } = null!;
		public string ServiceKey { get; set; } = null!;
		public string PrivateBucket { get; set; } = null!;
		public string PublicBucket { get; set; } = null!;
		public int SignedUrlExpirySeconds { get; set; }
		public long MaxFileSizeBytes { get; set; }
		public string[] AllowedExtensions { get; set; } = Array.Empty<string>();
		public string[] AllowedContentTypes { get; set; } = Array.Empty<string>();

	}



}
