using System.Text.Json.Serialization;

namespace ManoelStore.Models
{
	public class OrderResponse
	{
		public OrderResponse()
		{
			Boxes = [];
		}

		[JsonPropertyName("pedido_id")]
		public int OrderId { get; set; }

		[JsonPropertyName("caixas")]
		public Box[]? Boxes { get; set; }
	}
}
