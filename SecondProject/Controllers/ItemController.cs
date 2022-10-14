using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondProject.Attributes;
using SecondProject.Core.Managers.Interfaces;
using SecondProject.DbModel.Models;
using SecondProject.ModelViews.ModelView;
using SecondProject.ModelViews.Request;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecondProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ApiBaseController
    {
        private IItemManager _itemManager;
        private readonly ILogger<UserController> _logger;
        public ItemController(IItemManager itemManager, ILogger<UserController> logger)
        {
            _itemManager = itemManager;
            _logger = logger;
        }

        [Route("api/blog")]
        [HttpGet]
        public IActionResult GetBlogs(int page = 1, int pageSize = 5, string sortColumn = "", string sortDirection = "ascending", string searchText = "")
        {
            var result = _itemManager.GetBlogs(page, pageSize, sortColumn, sortDirection, searchText);
            return Ok(result);
        }

        [Route("api/blog/{id}")]
        [HttpGet]
        public IActionResult GetBlog(int id)
        {
            var result = _itemManager.GetBlog(id);
            return Ok(result);
        }

        [Route("api/blog/{id}")]
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SecondProjectAuthorizeAttribute()]
        public IActionResult ArchiveBlog(int id)
        {
            _itemManager.ArchiveBlog(LoggedInUser, id);
            return Ok();
        }
        [Route("api/blog/{id}")]
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SecondProjectAuthorizeAttribute()]
        public IActionResult IsRead(UserModel currentUser, int id)
        {
            _itemManager.IsRead(LoggedInUser, id);
            return Ok();
        }

        [Route("api/blog")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SecondProjectAuthorizeAttribute()]
        public IActionResult PutBlog(ItemRequest blogRequest)
        {
            var result = _itemManager.PutBlog(LoggedInUser, blogRequest);
            return Ok(result);
        }

    }
}
