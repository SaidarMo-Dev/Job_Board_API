namespace JobBoard.Service.Abstractions
{
	public interface IEmailService
	{
		Task<string> SendEmailAsync(string recipientEmail, string recipientName, string htmlMessage, string subject);


	}
}
