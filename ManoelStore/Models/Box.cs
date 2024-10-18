using System.Text.Json.Serialization;

namespace ManoelStore.Models
{
	public class Box
	{
		public Box()
		{
			Products = [];
		}

		[JsonPropertyName("caixa_id")]
		public string? BoxId { get; set; }

		[JsonPropertyName("produtos")]
		public IList<string> Products { get; set; }

		[JsonPropertyName("observacao")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? Comment { get; set; }
	}
}
