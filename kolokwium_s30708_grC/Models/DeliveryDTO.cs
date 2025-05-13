namespace kolokwium_s30708_grC.Models;

public class DeliveryDTO
{
    public int delivery_id { get; set; }
    public int customer_id { get; set; }
    public int driver_id { get; set; }
    public DateTime date { get; set; }
}