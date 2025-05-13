using kolokwium_s30708_grC.Models;

namespace kolokwium_s30708_grC.Services;

public interface IDeliveryService
{
    public Task<DeliveryReturnDTO> GetDelivery(int id);
}