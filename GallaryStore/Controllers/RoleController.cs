using GallaryStore.DTOs.role;
using GallaryStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GallaryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        [HttpPost]
        [Route("addRole")]
        public async Task <ActionResult> addRole(string roleName)
        {
            if(roleManager == null)
            {
                return BadRequest("role name is null");
            }
            IdentityRole role = new IdentityRole()
            {
                Name = roleName
            };
            try
            {
                await roleManager.CreateAsync(role);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut]
        [Route("editRole")]
        public async Task<ActionResult> editRole(roleDTO role)
        {
            if(ModelState.IsValid)
            {
                IdentityRole? identityRole = await roleManager.FindByIdAsync(role.roleId);
                if (identityRole == null)
                {
                    return NotFound();
                }
                try
                {
                    identityRole.Name = role.roleName;
                    await roleManager.UpdateAsync(identityRole);
                    return Ok(identityRole);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                
            }
            return BadRequest(ModelState);

            
        }
        [HttpPut]
        [Route("updateRoles")]
        public async Task<ActionResult> updateRoles(string userId, List<string>roles)
        {
            
            if (userId == null || roles == null)
            {
                return BadRequest("roles or userId is null");
            }
            ApplicationUser? user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            try
            {
                await userManager.RemoveFromRolesAsync(user, await userManager.GetRolesAsync(user));
                await userManager.AddToRolesAsync(user, roles);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("AllRole")]
        public ActionResult getAllRoles()
        {
            if (roleManager.Roles.ToList() == null) return NotFound();
            else return Ok(roleManager.Roles.ToList());
        }

        [HttpDelete]
        [Route("DeleteRole")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            if (await roleManager.FindByIdAsync(id) == null) return NotFound();
            else
            {
                try
                {
                    await roleManager.DeleteAsync(await roleManager.FindByIdAsync(id));
                    return Ok();
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                
            }
        }


        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (await userManager.FindByIdAsync(id) == null) return NotFound();
            else
            {
                try
                {
                    await userManager.DeleteAsync(await userManager.FindByIdAsync(id));
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet]
        [Route("getAllUsers")]
        public async Task<ActionResult> getusers()
        {
            if (userManager.Users.ToList() == null) return NotFound();
            else return Ok(userManager.Users.ToList());
        }


        [HttpGet]
        [Route("getUser")]
        public async Task<ActionResult> getUserById(string userId)
        {
            if (userId == null) return BadRequest("userId is null");
            if (await userManager.FindByIdAsync(userId) == null) return NotFound();
            else return Ok(await userManager.FindByIdAsync(userId));
        }

        [HttpGet]
        [Route("getRole")]
        public async Task<ActionResult> getRoleById(string roleId)
        {
            if (roleId == null) return BadRequest("roleId is null");
            if (await roleManager.FindByIdAsync(roleId) == null) 
                return NotFound();
            else return Ok(await roleManager.FindByIdAsync(roleId));
        }

        [HttpGet]
        [Route("getRoleUsers")]
        public async Task<ActionResult> getRoleUsers(string role)
        {
            if (role == null) return BadRequest("role is null");
            if (await userManager.GetUsersInRoleAsync(role) == null)
                return NotFound();
            else return Ok(await userManager.GetUsersInRoleAsync(role));
        }
    }
}
