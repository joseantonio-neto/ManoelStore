using System.ComponentModel.DataAnnotations;

namespace ManoelStore.Models
{
	public class PostSignRequest
	{
		[Required]
		public required string Email { get; set; }

		[Required]
		public required string Password { get; set; }
	}
}
