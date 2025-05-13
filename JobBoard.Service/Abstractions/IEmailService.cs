namespace JobBoard.Service.Abstractions
{
	public interface IEmailService
	{
		Task<string> SendEmail(string recipientEmail, string recipientName, string htmlMessage, string subject);


	}
}
