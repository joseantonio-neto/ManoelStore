

namespace ManoelStore.Services
{
	public class AuthService : IAuthService
	{
		public bool Validate(string email, string password)
		{
			return !string.IsNullOrWhiteSpace(email)
				&& !string.IsNullOrWhiteSpace(password)
				&& email == "admin@admin.com"
				&& password == "12345";
		}

		public string GetUserName(string email)
		{
			return "admin";
		}
	}
}
