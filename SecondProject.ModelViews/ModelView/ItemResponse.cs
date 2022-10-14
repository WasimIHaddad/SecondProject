
using SecondProject.Common.Extinsions;
using System.Collections.Generic;

namespace SecondProject.ModelViews.ModelView
{
    public class ItemResponse
    {
        public PagedResult<ItemModelView> Blog { get; set; }

        public  Dictionary<int, UserResult> User { get; set; }
    }
}
