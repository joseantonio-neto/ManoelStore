using ManoelStore.Models;

namespace ManoelStore.Services
{
	public interface IPackingService
	{
		PostPackingResponse Pack(PostPackingRequest packingRequest);
	}
}