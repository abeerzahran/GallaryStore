using GallaryStore.Models;
using System.ComponentModel.DataAnnotations;

namespace GallaryStore.DTOs.user
{
    public class AddUserDTO
    {
        public AddUserDTO(string UserName, string Email, string Password, string PhoneNumber,string address)
        {
            
            this.UserName = UserName;
            this.Email = Email;
            this.Password = Password;
            this.PhoneNumber = PhoneNumber;
            this.Address=address;

        }

        [MinLength(8,ErrorMessage ="the user Name length must be > 8 ")]
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="The comfirmed password must be identical to password")]
        [Required]
        public string ConfirmedPassword {  get; set; }

        [Required]
        [MinLength(3)]
        public string Address { get; set; }

    }
}
