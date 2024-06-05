namespace DSCC_Back.DTO
{
    public class OrderReadDto
    {
        public int OrderId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
    }
}
