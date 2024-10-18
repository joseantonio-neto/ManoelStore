using System.Text.Json.Serialization;

namespace ManoelStore.Models
{
	public class Product
	{
		[JsonPropertyName("produto_id")]
		public required string ProductId { get; set; }

		[JsonPropertyName("dimensoes")]
		public required Dimension Dimensions { get; set; }
	}
}
