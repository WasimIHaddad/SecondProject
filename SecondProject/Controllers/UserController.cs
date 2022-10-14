using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SecondProject.Common.Helper;
using SecondProject.Core.Managers.Interfaces;
using SecondProject.DbModel.Models;
using SecondProject.ModelViews.ModelView;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Tazeez.Common.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecondProject.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ApiBaseController
    {
       
        private IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        // GET: api/<UserController>
        [Route("api/user/all")]
        [HttpGet]
        
        
        
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/<UserController>/5
        [Route("api/user/get")]
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [Route("api/user/Register")]
        [HttpPost]
        public IActionResult SignUp([FromBody] UserRegistrationViewModel userReg)
        {
            var res = _userManager.SignUp(userReg);
            return Ok(res);
            
        }
        [Route("api/user/login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModelView userReg)
        {
            var res = _userManager.Login(userReg);
            return Ok(res);
        }

        // PUT api/<UserController>/5
        [Route("api/user/Put")]
        [HttpPut]
        public IActionResult UpdateProfile( UserModel request)
        {
            var user = _userManager.UpdateProfile(LoggedInUser,request);
            return  Ok(user);
        }

        // DELETE api/<UserController>/5
        [Route("api/user/Delete")]
        [HttpDelete]
        public IActionResult Delete( int id)
        {
            _userManager.DeleteUser(LoggedInUser, id);
                return Ok();
            
        }

        
    }
}
