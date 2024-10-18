namespace ManoelStore.Services
{
	public interface IAuthService
	{
		string GetUserName(string email);
		bool Validate(string email, string password);
	}
}