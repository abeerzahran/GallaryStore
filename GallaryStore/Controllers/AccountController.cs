using GallaryStore.DTOs;
using GallaryStore.DTOs.user;
using GallaryStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Text;

namespace GallaryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(RoleManager<IdentityRole> _roleManager ,UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser>_signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.roleManager = _roleManager;
        }
        [HttpPost]
        [Route("/api/register")]
        public async Task<ActionResult> Registration(AddUserDTO addUserDTO)
        {
            if(ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = addUserDTO.UserName,
                    Email = addUserDTO.Email,
                    PasswordHash = addUserDTO.Password,
                    PhoneNumber = addUserDTO.PhoneNumber,
                    address = addUserDTO.Address,
                    

                };
                
                IdentityResult result = await userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    IdentityResult identityResult= await userManager.AddToRoleAsync(user, "user");
                    if(!identityResult.Succeeded) {
                        if (await roleManager.FindByNameAsync("user") == null)
                        {
                            await roleManager.CreateAsync(new IdentityRole() { Name = "user" });
                        }
                        else
                            BadRequest();
                    }
                    //create cookies and login
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Created();
                }

                var errors = result.Errors.Select(e=> e.Description).ToList();
                string error = string.Join(", \n", errors);
                return BadRequest(error);
                

            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("/api/login")]
        public async Task<ActionResult> Login(loginDTO login)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser? user = await userManager.FindByEmailAsync(login.Email);
                if (user == null)
                {
                    return NotFound();
                }

                if (user != null)
                {
                    if(await userManager.CheckPasswordAsync(user, login.Password))
                    {
                        List<Claim> userclaims = new List<Claim>()
                        {
                            new Claim("userName",user.UserName),
                            new Claim("phone",user.PhoneNumber),
                            new Claim ("email",user.Email),
                            new Claim("address",user.address)
                        };
                        string key = "welcome to my secret world abeer adel zahran";

                        var secretkey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                        var sigingCred= new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                                claims: userclaims,
                                signingCredentials: sigingCred,
                                expires:DateTime.Now.AddDays(3)
                            );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                        await signInManager.SignInAsync(user, login.rememberMe);
                        var role = await userManager.GetRolesAsync(user);
                        return Ok(new {role= role, token= tokenString, user=user});
                    }
                    return BadRequest("Incorrect password");
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("/api/logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
            
        }

        [HttpPut]
        [Route("updateUser")]
        public async Task<ActionResult> updateUser(string userId, updateUserDTO user )
        {
            if (ModelState.IsValid)
            {
                if (userId != null && user != null && user.Id == userId)
                {
                    ApplicationUser? applicationUser = await userManager.FindByIdAsync(userId);
                    if (applicationUser == null)
                    {
                        return NotFound();
                    }

                    applicationUser.PhoneNumber = user.PhoneNumber;
                    applicationUser.UserName = user.UserName;
                    applicationUser.Email = user.Email;

                    await userManager.UpdateAsync(applicationUser);
                    return Ok(applicationUser);
                }
                return BadRequest();
            }
            
            return BadRequest(ModelState);
        }
        [HttpPut]
        [Route("updatePassword")]
        public async Task<ActionResult> updatePassword(string userId, UpdatePasswordDTO user)
        {
            if (ModelState.IsValid)
            {
                if (userId!=null && user != null)
                {
                    ApplicationUser? applicationUser = await userManager.FindByIdAsync(userId);
                    if (applicationUser == null)
                    {
                        return NotFound();
                    }

                    IdentityResult result = await userManager.ChangePasswordAsync(applicationUser, user.currentPassword, user.newPassword);
                    if (result.Succeeded)
                    {
                        return Ok(applicationUser);
                    }

                    return BadRequest(result.Errors);

                }
                return BadRequest("user or userId is required");
            }
            
            return BadRequest(ModelState);
        }


    }
}
