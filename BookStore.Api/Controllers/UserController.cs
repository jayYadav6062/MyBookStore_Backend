using BookStore.Models.ViewModels;
using BookStore.Models.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository _repository = new UserRepository();
        RoleRepository _rolerepository = new RoleRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            ListResponse<User> response = _repository.GetUsers(pageIndex, pageSize, keyword);
            ListResponse<UserModel> users = new ListResponse<UserModel>()
            {
                records = response.records.Select(u => new UserModel(u)),
                TotalRecords = response.TotalRecords,
            };

            return Ok(users);
        }

        [HttpGet]
        [Route("Roles")]
        public IActionResult GetRoles(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            ListResponse<Role> response = _rolerepository.GetRoles(pageIndex, pageSize, keyword);
            ListResponse<RoleModel> users = new ListResponse<RoleModel>()
            {
                records = response.records.Select(u => new RoleModel(u)),
                TotalRecords = response.TotalRecords,
            };

            return Ok(users);
        }


        [HttpGet]
        [Route("Roless")]
        public IActionResult GetRole(int Roleid)
        {
            Role user = _rolerepository.GetRole(Roleid);
            if (user == null)
                return NotFound();

            RoleModel roleModel = new RoleModel(user);
            return Ok(roleModel);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
            User user = _repository.GetUser(id);
            if (user == null)
                return NotFound();

            UserModel userModel = new UserModel(user);
            return Ok(userModel);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateUser(ProfileUpdateModel model)
        {
            if (model != null)
            {
                var user = _repository.GetUser(model.Id);
                if (user == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                user.Firstname = model.Firstname;
                user.Lastname = model.Lastname;
                user.Email = model.Email;
                user.Password = model.NewPassword;

                User isSaved = _repository.UpdateUser(user);


                if (isSaved == null)
                {
                    return BadRequest("Not Updated");
                }
                UserModel updated = new UserModel(isSaved);
                return Ok(updated);
            }

            return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "No, Such User - Please provide correct information");

        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUser(int id)
        {
            bool isDeleted = _repository.DeleteUser(id);
            return Ok(isDeleted);
        }
    }
}
