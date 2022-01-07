using Microsoft.AspNetCore.Mvc;
using PassGuardianWS.Interfaces;
using PassGuardianWS.Models;
using PassGuardianWS.Service;
using PassGuardianWS.Utils;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace PassGuardianWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IPassword _password = new PasswordService();

        [HttpPost("save")]
        public ActionResult Save(Password password)
        {
            ModelState.Clear();
            if (_password.Save(password))
            {
                return Ok();
            }
            else
            {
                ModelState.AddModelError("1", "Error");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("delete")]
        public ActionResult Delete(Password password)
        {
            ModelState.Clear();
           if (_password.Delete(password))
            {
                return Ok();
            }
            else
            {
                ModelState.AddModelError("2", "Error");
                return Ok(ModelState);
            }
        }

        [HttpGet("listbyuser")]
        public ActionResult ListByUser(int id)
        {
            ModelState.Clear();
            List<Password> passwords = _password.ListById(id);

            foreach (var item in passwords)
            {
                Decrypt(item);
            }

            return Ok(passwords);
        }

        [HttpGet("generatepasswordtouser")]
        public ActionResult GeneratePasswordToUser(int userId)
        {
            ModelState.Clear();
            Password password = new Password();
            password.ApplicationPassword = _password.GeneratePassword(userId);
            password.LastChange = DateTime.Now;
            return Ok(password);
        }

        private void Decrypt(Password password)
        {
            var keys = password.KeyPassword.Split("#&");
            password.ApplicationPassword = Encryption.Decrypt(Convert.FromBase64String(password.ApplicationPassword),
                Convert.FromBase64String(keys[0]), Convert.FromBase64String(keys[1]));

            password.KeyPassword = "";
        }
    }
}
