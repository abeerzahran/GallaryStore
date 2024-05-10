using GallaryStore.Models;

namespace GallaryStore.DTOs
{
    public class UserDTO
    {
        public UserDTO()
        {
            
        }
        public UserDTO(string Id, string UserName, string Email, string PasswordHash, string PhoneNumber, List<Order> orders, List<Favourite> favourites)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.Email = Email;
            this.PasswordHash = PasswordHash;
            this.PhoneNumber = PhoneNumber;
            
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}
