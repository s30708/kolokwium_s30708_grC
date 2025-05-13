using kolokwium_s30708_grC.Models;
using Microsoft.Data.SqlClient;

namespace kolokwium_s30708_grC.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IConfiguration _configuration;

    public DeliveryService(IConfiguration configuration)
    {
        _configuration = configuration;
    }




    public async Task<DeliveryReturnDTO> GetDelivery(int id)
    {
        DeliveryReturnDTO deliveryReturnDto = new DeliveryReturnDTO();

        int customerId = 0;
        int driverId = 0;

        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        using (SqlCommand command =
               new SqlCommand(@"SELECT customer_id, driver_id, date FROM Delivery WHERE delivery_id = @DeliveryId ",
                   connection))
        {
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@DeliveryId", id);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    customerId = reader.GetInt32(0);
                    driverId = reader.GetInt32(1);
                    deliveryReturnDto.date = reader.GetDateTime(2);
                }
            }
        }

        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        using (SqlCommand command2 =
               new SqlCommand(
                   @"SELECT first_name, last_name, date_of_birth FROM Customer WHERE customer_id = @CustomerId ",
                   connection))
        {
            await connection.OpenAsync();
            command2.Parameters.AddWithValue("@CustomerId", customerId);
            using (var reader = await command2.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    deliveryReturnDto.customer = new customerDTO()
                    {
                        firstName = reader.GetString(0),
                        lastName = reader.GetString(1),
                        dateOfBirth = reader.GetDateTime(2)
                    };
                }
            }
        }

        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        using (SqlCommand command3 =
               new SqlCommand(
                   @"SELECT first_name, last_name, license_number FROM Driver WHERE driver_id = @DriverId ",
                   connection))
        {
            await connection.OpenAsync();
            command3.Parameters.AddWithValue("@DriverId", driverId);
            using (var reader = await command3.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    deliveryReturnDto.driver = new DriverDTO()
                    {
                        firstName = reader.GetString(0),
                        lastName = reader.GetString(1),
                        licenseNumber = reader.GetString(2)
                    };
                }
            }
        }


        List<ProductDTO> products = new List<ProductDTO>();
        
        
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        using (SqlCommand command4 =
               new SqlCommand(
                   @"SELECT 
                    p.name AS product_name,
                    p.price,
                    pd.amount
                    FROM 
                    Product_Delivery pd
                    JOIN 
                    Product p ON pd.product_id = p.product_id
                    WHERE 
                    pd.delivery_id = @DeliveryID;
",
                   connection))
        {
            await connection.OpenAsync();
            command4.Parameters.AddWithValue("@DeliverID", id);
            using (var reader = await command4.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    products.Add(new ProductDTO()
                    {
                        name = reader.GetString(0),
                        amount = reader.GetInt32(1),
                        price = reader.GetDecimal(2),
                    });
                }
            }
        }
        
        deliveryReturnDto.ProductsList = products;
        
        
        return deliveryReturnDto;
    }
}








