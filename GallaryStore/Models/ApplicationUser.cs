using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GallaryStore.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string address { get; set; }
        public List<Order> orders { get; set; }
        public List<Favourite> favourites { get; set; }
    }
}
