namespace JobBoard.Service.Abstractions
{
	public interface IFileUrlResolver
	{
		string? ResolveCompanyLogo(string? path);
	}
}
