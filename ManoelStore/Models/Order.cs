using System.Text.Json.Serialization;

namespace ManoelStore.Models
{
	public class Order
	{
		public Order()
		{
			Products = [];
		}

		[JsonPropertyName("pedido_id")]
		public int OrderId { get; set; }
		
		[JsonPropertyName("produtos")]
		public Product[] Products { get; set; }

		public int OrderVolume
		{
			get
			{
				return Products.Sum(p => p.Dimensions.Volume);
			}
		}
	}
}
