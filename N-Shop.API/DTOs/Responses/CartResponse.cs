namespace N_Shop.API.DTOs.Responses;

public class CartResponse
{
    public int ProductId { get; set; }
    public string ApplicationUserId { get; set; }
    public int Count { get; set; }
}