namespace kolokwium_s30708_grC.Models;

public class DeliveryReturnDTO
{
    public DateTime date{ get; set; }
    public customerDTO customer { get; set; }
    public DriverDTO driver{ get; set; }
    public List<ProductDTO> ProductsList{ get; set; }
}