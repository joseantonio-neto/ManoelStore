using System.Text.Json.Serialization;

namespace ManoelStore.Models
{
	public class PostPackingRequest
	{
		public PostPackingRequest()
		{
			Orders = [];
		}

		[JsonPropertyName("pedidos")]
		public Order[] Orders { get; set; }
	}
}
