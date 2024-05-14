using GallaryStore.DTOs;
using GallaryStore.Models;
using GallaryStore.UnitOfWork;

namespace GallaryStore.Services
{
    public class UserService
    {
        public unitOfWork<ApplicationUser> unit;
        public UserService(unitOfWork<ApplicationUser> unit)
        {
            this.unit = unit;
        }

        public List<UserDTO> GetAll()
        {
            return unit.Repository.GetAll().Select(p => new UserDTO(p.Id, p.UserName, p.Email, p.PasswordHash, p.PhoneNumber, p.orders, p.favourites)).ToList();
        }
        public UserDTO GetById(string id)
        {

            ApplicationUser p = unit.Repository.getElement(p=> p.Id==id,null);
            return new UserDTO(p.Id, p.UserName, p.Email, p.PasswordHash, p.PhoneNumber, p.orders, p.favourites);
        }

        public void Update(UserDTO User)
        {
            ApplicationUser p = new ApplicationUser()
            {
                Id = User.Id,
                UserName = User.UserName,
                Email = User.Email,
                PasswordHash = User.PasswordHash,
                PhoneNumber = User.PhoneNumber,
                
                

            };
            unit.Repository.update(p);
            unit.savechanges();
        }
        public void Delete(string id)
        {
            ApplicationUser User = unit.Repository.getElement(p => p.Id == id, null);
            unit.Repository.delete(User);
            unit.savechanges();
        }
        public void Add(UserDTO User)
        {
            ApplicationUser p = new ApplicationUser()
            {
               
                UserName = User.UserName,
                Email = User.Email,
                PasswordHash = User.PasswordHash,
                PhoneNumber = User.PhoneNumber,
               
            };
            unit.Repository.add(p);
            unit.savechanges();
        }

       
    }
}
