using GallaryStore.Models;

namespace GallaryStore.DTOs.user
{
    public class updateUserDTO
    {
        public updateUserDTO()
        {

        }
        public updateUserDTO(string Id, string UserName, string Email, string PhoneNumber, List<Order> orders, List<Favourite> favourites)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;

        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}

