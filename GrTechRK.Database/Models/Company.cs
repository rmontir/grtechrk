using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrTechRK.Database.Models
{
    [Table("Companies")]
    public class Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        public byte[] Logo { get; set; }

        [Column(TypeName = "text")]
        public Uri Website { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
