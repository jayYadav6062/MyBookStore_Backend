using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.Api.Controllers
{

    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        RoleRepository _rolerepository = new RoleRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string keyword = "")
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
        [Route("Roles")]
        public IActionResult GetRole(int Roleid)
        {
            Role user = _rolerepository.GetRole(Roleid);
            if (user == null)
                return NotFound();

            RoleModel roleModel = new RoleModel(user);
            return Ok(roleModel);
        }
    }

}
