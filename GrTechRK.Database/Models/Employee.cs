using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GrTechRK.Database.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required, StringLength(200)]
        public string FirstName { get; set; }

        [Required, StringLength(200)]
        public string LastName { get; set; }

        [ForeignKey(nameof(Company))]
        public int? CompanyId { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Phone { get; set; }

        [AllowNull, MaybeNull]
        public virtual Company Company { get; set; }
    }
}
