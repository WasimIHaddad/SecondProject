using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondProject.ModelViews.ModelView
{
    public class SecModelView
    {
        public int ItemId { get; set; }
        public string ItemTitle { get; set; }
        public string ItemContent { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }




    }
}
