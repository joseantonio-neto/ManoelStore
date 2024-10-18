using System.ComponentModel.DataAnnotations;

namespace ManoelStore.Models
{
	public class PostSignResponse
	{
		[Required]
		public required string Token { get; set; }

		[Required]
		public DateTime Expiration { get; set; }
	}
}
