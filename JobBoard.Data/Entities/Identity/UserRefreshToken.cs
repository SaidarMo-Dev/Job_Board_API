namespace JobBoard.Data.Entities.Identity
{
	public class UserRefreshToken
	{

		public int Id { get; set; }
		public int UserId { get; set; }
		public string RefreshToken { get; set; }
		public string AccessToken { get; set; }
		public string? JwtId { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime ExpiresOn { get; set; }
		public DateTime? RevokedOn { get; set; }

		public bool IsActive => ExpiresOn >= DateTime.UtcNow && RevokedOn == null;

		public virtual User User { get; set; }
	}
}
