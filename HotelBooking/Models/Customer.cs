
namespace HotelBooking.Models
{
    public class CustomerModel : Customer
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int LoyaltyTierId { get; set; }
        public int TotalPoints { get; set; } = 0;   
    }
       
}