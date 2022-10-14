using SecondProject.ModelViews.ModelView;
using SecondProject.ModelViews.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondProject.Core.Managers.Interfaces
{
    public interface IItemManager : IManager
    {
        ItemResponse GetBlogs(int page = 1, int pageSize = 10, string sortColumn = "", string sortDirection = "ascending", string searchText = "");

        ItemModelView GetBlog(int id);

        ItemModelView PutBlog(UserModel currentUser, ItemRequest blogRequest);

        void ArchiveBlog(UserModel currentUser, int id);
        void IsRead(UserModel currentUser, int id);
    }
}
