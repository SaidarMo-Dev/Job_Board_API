namespace JobBoard.Service.Abstractions
{
	public interface IEmailService
	{
		Task<string> SendEmail(string email, string message);
		Task<string> SendEmail(string email, string message, string subject);
	}
}
