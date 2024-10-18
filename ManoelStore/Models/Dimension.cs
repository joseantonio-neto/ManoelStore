using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManoelStore.Models
{
	public class Dimension
	{
		public Dimension(int height, int width, int lenght)
		{
			Height = height;
			Width = width;
			Lenght = lenght;
			Volume = height * width * lenght;
		}

		[JsonPropertyName("altura")]
		public int Height { get; internal set; }

		[JsonPropertyName("largura")]
		public int Width { get; internal set; }

		[JsonPropertyName("comprimento")]
		public int Lenght { get; internal set; }

		[NotMapped]
		public int Volume { get; internal set; }
	}
}