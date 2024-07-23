using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Model
{
    [Table("Users")]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = ("Please provide name"))]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide email")]
        [EmailAddress(ErrorMessage = "Please provide valid email")]
        [MaxLength(200)]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Please provide gender")]
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; } 

        public DateTimeOffset CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
