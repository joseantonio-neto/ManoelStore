using ManoelStore.Models;
using ManoelStore.Services;
using System.Linq;
using System.Text.Json;

namespace ManoelStoreTest
{
	public class PackingServiceTest
	{
		private readonly OrderResponse[] _expectedOrderResponses = new OrderResponse[10];

		[SetUp]
		public void Setup()
		{
			_expectedOrderResponses[0] = new OrderResponse
			{
				OrderId = 3,
				Boxes = [
					new Box { BoxId = "Caixa 1", Products = ["Headset"] }
				]
			};

			_expectedOrderResponses[5] = new OrderResponse
			{
				OrderId = 5,
				Boxes = [
					new Box { BoxId = null, Products = ["Cadeira Gamer"], Comment = "Produto não cabe em nenhuma caixa disponível." }
				]
			};

			_expectedOrderResponses[6] = new OrderResponse
			{
				OrderId = 6,
				Boxes = [
					new Box { BoxId = "Caixa 3", Products = ["Monitor", "Notebook"] },
					new Box { BoxId = "Caixa 1", Products = ["Webcam", "Microfone"] }
				]
			};
		}

		[Test]
		public void Test_Pedido3()
		{
			Order pedido = new()
			{
				OrderId = 3,
				Products = [
					new Product { ProductId = "Headset", Dimensions = new Dimension(25, 15, 20 ) }
				]
			};
			var request = new PostPackingRequest { Orders = [pedido] };

			var service = new PackingService();

			var response = service.Pack(request);

			Assert.That(response, Is.Not.Null);
			Assert.That(response.Orders, Is.Not.Empty);
			Assert.Multiple(() =>
			{
				Assert.That(response.Orders[0].OrderId, Is.EqualTo(_expectedOrderResponses[0].OrderId));
				Assert.That(response.Orders[0].Boxes?[0].BoxId, Is.EqualTo(_expectedOrderResponses[0].Boxes?[0].BoxId));
			});
		}

		[Test]
		public void Test_Pedido5()
		{
			Order pedido = new()
			{
				OrderId = 5,
				Products = [
					new Product { ProductId = "Cadeira Gamer", Dimensions = new Dimension(120, 60, 70 ) }
				]
			};
			var request = new PostPackingRequest { Orders = [pedido] };

			var service = new PackingService();

			var response = service.Pack(request);

			Assert.That(response, Is.Not.Null);
			Assert.That(response.Orders, Is.Not.Empty);
			Assert.Multiple(() =>
			{
				Assert.That(response.Orders[0].OrderId, Is.EqualTo(_expectedOrderResponses[5].OrderId));
				Assert.That(response.Orders[0].Boxes?[0].BoxId, Is.EqualTo(_expectedOrderResponses[5].Boxes?[0].BoxId));
				Assert.That(response.Orders[0].Boxes?[0].Comment, Is.EqualTo(_expectedOrderResponses[5].Boxes?[0].Comment));
			});
		}

		[Test]
		public void Test_Pedido6()
		{
			Order pedido = new()
			{
				OrderId = 6,
				Products = [
					new Product { ProductId = "Webcam", Dimensions = new Dimension(7, 10, 5 ) },
					new Product { ProductId = "Microfone", Dimensions = new Dimension(25, 10, 10) },
					new Product { ProductId = "Monitor", Dimensions = new Dimension(50, 60, 20) },
					new Product { ProductId = "Notebook", Dimensions = new Dimension(2, 35, 25) }
				]
			};
			var request = new PostPackingRequest { Orders = [pedido] };

			var service = new PackingService();

			var response = service.Pack(request);

			Assert.That(response, Is.Not.Null);
			Assert.That(response.Orders, Is.Not.Empty);
			Assert.Multiple(() =>
			{
				Assert.That(response.Orders[0].OrderId, Is.EqualTo(_expectedOrderResponses[6].OrderId));
				Assert.That(response.Orders[0].Boxes?[0].BoxId, Is.EqualTo(_expectedOrderResponses[6].Boxes?[0].BoxId));
				Assert.That(response.Orders[0].Boxes?[0].Products, Is.EqualTo(_expectedOrderResponses[6].Boxes?[0].Products));
				Assert.That(response.Orders[0].Boxes?[1].BoxId, Is.EqualTo(_expectedOrderResponses[6].Boxes?[1].BoxId));
				CollectionAssert.AreEquivalent(response.Orders[0].Boxes?[1].Products, _expectedOrderResponses[6].Boxes?[1].Products);
			});
		}
	}
}