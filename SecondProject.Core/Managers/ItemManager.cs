using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondProject.Common.Extinsions;
using SecondProject.Core.Managers.Interfaces;
using SecondProject.DbModel.Models;
using SecondProject.ModelViews.ModelView;
using SecondProject.ModelViews.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazeez.Common.Extensions;

namespace SecondProject.Core.Managers
{
    public class ItemManager :IItemManager
    {
        private secdbContext _secdbContext;
        private IMapper _mapper;

        public ItemManager(secdbContext secdbContext, IMapper mapper)
        {
            _secdbContext = secdbContext;
            _mapper = mapper;
        }
        public void ArchiveBlog(UserModel currentUser, int id)
        {
            if (!currentUser.IsRead)
            {
                throw new ServiceValidationException("You don't have permission to archive blog");
            }

            var data = _secdbContext.Items
                                    .FirstOrDefault(a => a.Id == id)
                                    ?? throw new ServiceValidationException("Invalid blog id received");
            data.Archived = true;
            _secdbContext.SaveChanges();
        }
        public void IsRead(UserModel currentUser, int id)
        {
            if (!currentUser.IsRead)
            {
                throw new ServiceValidationException("You don't have permission to archive blog");
            }

            var data = _secdbContext.Items
                                    .FirstOrDefault(a => a.Id == id)
                                    ?? throw new ServiceValidationException("Invalid blog id received");
            data.IsRead = true;
            _secdbContext.SaveChanges();
        }

        public ItemModelView GetBlog(int id)
        {
            var res = _secdbContext.Items
                                   .Include("Creator")
                                   .FirstOrDefault(a => a.Id == id)
                                   ?? throw new ServiceValidationException("Invalid blog id received");

            return _mapper.Map<ItemModelView>(res);
        }

        public ItemResponse GetBlogs(int page = 1, int pageSize = 10, string sortColumn = "", string sortDirection = "ascending", string searchText = "")
        {
            var queryRes = _secdbContext.Items
                                        .Where(a => string.IsNullOrWhiteSpace(searchText)
                                                    || (a.Title.Contains(searchText)
                                                        || a.Content.Contains(searchText)));

            if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("ascending", StringComparison.InvariantCultureIgnoreCase))
            {
                queryRes = queryRes.OrderBy(sortColumn);
            }
            else if (!string.IsNullOrWhiteSpace(sortColumn) && sortDirection.Equals("descending", StringComparison.InvariantCultureIgnoreCase))
            {

                queryRes = queryRes.OrderByDescending(sortColumn);
            }

            var res = queryRes.GetPaged(page, pageSize);

            var userIds = res.Data
                             .Select(a => a.CreatorId)
                             .Distinct()
                             .ToList();

            var users = _secdbContext.Users
                                     .Where(a => userIds.Contains(a.Id))
                                     .ToDictionary(a => a.Id, x => _mapper.Map<UserResult>(x));

            var data = new ItemResponse
            {
                Blog = _mapper.Map<PagedResult<ItemModelView>>(res),
                User = users
            };

            data.Blog.Sortable.Add("Title", "Title");
            data.Blog.Sortable.Add("CreatedDate", "Created Date");

            return data;
        }

        public ItemModelView PutBlog(UserModel currentUser, ItemRequest itemRequest)
        {
            Item item = null;

            if (!currentUser.IsRead)
            {
                throw new ServiceValidationException("You don't have permission to add or update blog");
            }

            if (itemRequest.Id > 0)
            {
                item = _secdbContext.Items
                                    .FirstOrDefault(a => a.Id == itemRequest.Id)
                                    ?? throw new ServiceValidationException("Invalid blog id received");

                item.Title = itemRequest.Title;
                item.Content = itemRequest.Content;
            }
            else
            {
                item = _secdbContext.Items.Add(new Item
                {
                    Title = itemRequest.Title,
                    Content = itemRequest.Content,
                    CreatorId = currentUser.Id
                }).Entity;
            }

            _secdbContext.SaveChanges();
            return _mapper.Map<ItemModelView>(item);
        }
    }
}

