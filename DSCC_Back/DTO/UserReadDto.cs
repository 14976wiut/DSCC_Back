namespace DSCC_Back.DTO
{
    public class UserReadDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<OrderReadDto> Orders { get; set; }
    }
}
