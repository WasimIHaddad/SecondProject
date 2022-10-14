using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SecondProject.DbModel.Models
{
    public partial class User
    {
        public User()
        {
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [DefaultValue("")]
        public string Image { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        public bool Archived { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
