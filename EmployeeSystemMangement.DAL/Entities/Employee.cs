using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.DAL.Entities
{
    public enum Gender
    {
        [EnumMember(Value ="Male")]
        Male=1,
        [EnumMember(Value ="Female")]

        Female = 2
    }
    public enum EmployeeType
    {
        [EnumMember(Value = " FullTime")]
        FullTime=1,
        [EnumMember(Value = " PartTime")]

        PartTime = 2
    }
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        [Range(22,35)]
        public int? Age { get; set; }

        [RegularExpression(@"^\d+-[a-zA-Z]+-[a-zA-Z]+-[a-zA-Z]+$", ErrorMessage = "Address must match the pattern '123-street-city-country'.")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [Phone]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public DateTime CreationDate { get; set; }= DateTime.Now;
        public bool IsDeleted { get; set; } = false;

    }
}
