using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SecondProject.DbModel.Models
{
    public partial class Item
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
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        public bool Archived { get; set; }
        public bool IsRead { get; set; }

        public virtual User User { get; set; }

        public virtual User Creator { get; set; }
    }
}
