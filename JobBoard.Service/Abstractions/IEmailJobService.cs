namespace JobBoard.Service.Abstractions
{
	public interface IEmailJobService
	{
		void EnqueueVerificationEmail(string toEmail, string fullName, string code);
		void EnqueuePasswordResetEmail(string toEmail, string fullName, string resetLink);

	}
}
