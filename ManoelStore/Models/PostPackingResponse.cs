using System.Text.Json.Serialization;

namespace ManoelStore.Models
{
	public class PostPackingResponse
	{
		public PostPackingResponse()
		{
			Orders = [];
		}

		[JsonPropertyName("pedidos")]
		public required OrderResponse[] Orders { get; set; }
	}
}
