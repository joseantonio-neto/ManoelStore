using ManoelStore.Models;

namespace ManoelStore.Services
{
	public class PackingService : IPackingService
	{
		private const string NoFitBox = "Produto não cabe em nenhuma caixa disponível.";
		private readonly List<BoxType> BoxTypes =
		[
			new() { BoxId = "Caixa 1", Dimensions = new Dimension(30, 40, 80) },
			new() { BoxId = "Caixa 2", Dimensions = new Dimension(80, 50, 40) },
			new() { BoxId = "Caixa 3", Dimensions = new Dimension(50, 80, 60) },
		];

		public PostPackingResponse Pack(PostPackingRequest packingRequest)
		{
			var pacotes = new OrderResponse[packingRequest.Orders.Length];

			for (int i = 0; i < packingRequest.Orders.Length; i++)
			{
				var boxes = HandleProducts(packingRequest.Orders[i].Products);

				pacotes[i] = new OrderResponse
				{
					OrderId = packingRequest.Orders[i].OrderId,
					Boxes = boxes
				};
			}

			return new PostPackingResponse { Orders = pacotes };
		}

		private Box[] HandleProducts(Product[] products)
		{
			var orderingProducts = products.OrderByDescending(p => p.Dimensions.Width).ThenByDescending(p => p.Dimensions.Volume);
			var boxes = new List<Box>();

			var productsToPack = new List<Product>();
			foreach (var product in orderingProducts)
			{
				var bestBox = GetBestFitBox(product);

				if (bestBox is not null)
				{
					var box = boxes.FindLast(b => b.BoxId == bestBox.BoxId);
					if (box is null)
					{
						box = new Box { BoxId = bestBox.BoxId };
						boxes.Add(box);
					}

					box.Products.Add(product.ProductId);
				}
				else
				{
					var box = new Box { Products = [product.ProductId], Comment = NoFitBox };
					boxes.Add(box);
				}
			}

			return boxes.ToArray();
		}

		private BoxType? GetBestFitBox(Product product)
		{
			var fitSizeBoxes = BoxTypes
				.Where(t =>
							(t.Dimensions.Height >= product.Dimensions.Height && t.Dimensions.Width >= product.Dimensions.Width && t.Dimensions.Lenght >= product.Dimensions.Lenght) ||
							(t.Dimensions.Height >= product.Dimensions.Width && t.Dimensions.Width >= product.Dimensions.Height && t.Dimensions.Lenght >= product.Dimensions.Lenght) ||
							(t.Dimensions.Height >= product.Dimensions.Height && t.Dimensions.Width >= product.Dimensions.Lenght && t.Dimensions.Lenght >= product.Dimensions.Width) ||
							(t.Dimensions.Height >= product.Dimensions.Lenght && t.Dimensions.Width >= product.Dimensions.Width && t.Dimensions.Lenght >= product.Dimensions.Height) ||
							(t.Dimensions.Height >= product.Dimensions.Width && t.Dimensions.Width >= product.Dimensions.Lenght && t.Dimensions.Lenght >= product.Dimensions.Height) ||
							(t.Dimensions.Height >= product.Dimensions.Lenght && t.Dimensions.Width >= product.Dimensions.Height && t.Dimensions.Lenght >= product.Dimensions.Width)
				);

			var fitVolumeBoxes = BoxTypes
				.Where(t => t.Dimensions.Volume >= product.Dimensions.Volume);

			var fitBoxes = fitSizeBoxes.Intersect(fitVolumeBoxes);

			var bestBox = fitBoxes.FirstOrDefault();

			return bestBox;
		}
	}
}
