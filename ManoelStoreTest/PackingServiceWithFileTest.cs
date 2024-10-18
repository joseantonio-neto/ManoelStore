using ManoelStore.Models;
using ManoelStore.Services;
using System.Text.Json;

namespace ManoelStoreTest
{
	public class PackingServiceWithFileTest
	{
		private PostPackingRequest _request;
		private PostPackingResponse _response;
		private PostPackingResponse _expetedResponse;

		[SetUp]
		public void Setup()
		{
			var jsonContentRequest = File.ReadAllText("./Files/entrada.json");
			_request = JsonSerializer.Deserialize<PostPackingRequest>(jsonContentRequest);

			var jsonContentResponse = File.ReadAllText("./Files/saida.json");
			_expetedResponse = JsonSerializer.Deserialize<PostPackingResponse>(jsonContentResponse);

			var service = new PackingService();
			_response = service.Pack(_request);
		}

		[Test]
		public void Test_File([Range(0, 9, 1)] int index)
		{

			Assert.That(_response, Is.Not.Null);
			Assert.That(_response.Orders, Is.Not.Empty);
			Assert.Multiple(() =>
			{
				Assert.That(_response.Orders[index].OrderId, Is.EqualTo(_expetedResponse.Orders[index].OrderId));
				Assert.That(_response.Orders[index].Boxes?[0].BoxId, Is.EqualTo(_expetedResponse.Orders[index].Boxes?[0].BoxId));
				Assert.That(_response.Orders[index].Boxes?.Length, Is.EqualTo(_expetedResponse.Orders[index].Boxes?.Length));
				Assert.That(_response.Orders[index].Boxes?[0].Products, Is.EquivalentTo(_expetedResponse.Orders[index].Boxes?[0].Products));
			});
		}
	}
}