using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondProject.ModelViews.ModelView
{
    public class ItemModelView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [DefaultValue("")]
        public string Image { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }

        public virtual UserResult User { get; set; }
        public virtual UserResult Creator { get; set; }
    }
}
