using Microsoft.AspNetCore.Mvc;
using PassGuardianWS.Interfaces;
using PassGuardianWS.Models;
using PassGuardianWS.Service;

namespace PassGuardianWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user = new UserService();

        [HttpPost("save")]
        public ActionResult Save(User user)
        {
            if (_user.Save(user))
            {
                return Ok();

            }
            else
            {
                 ModelState.AddModelError("1", "Error");
                return BadRequest(ModelState);
            }
        }

        [HttpGet("getbyid")]
        public ActionResult GetById(int id)
        {
            var user = _user.GetById(id);
            if(user != null)
            {
                return Ok(user);
            }
            else
            {
                ModelState.AddModelError("2", "Not Found");
                return Ok(ModelState);
            }
        }

        [HttpPost("delete")]
        public ActionResult Delete(User user)
        {
            if (_user.Delete(user))
            {
                return Ok();
            }
            else
            {
                ModelState.AddModelError("3", "Error");
                return Ok(ModelState);
            }
        }
    }
}
