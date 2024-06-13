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
    public class Employee:BaseEntity
    {

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        [Range(22,35)]
        public int? Age { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]{5,}@(gmail\.com|yahoo\.com|hotmail\.com|outlook\.com|icloud\.com|protonmail\.com)$", ErrorMessage = "The email must have at least 5 characters before the '@' symbol and must be a valid domain (gmail.com, yahoo.com, hotmail.com, outlook.com, icloud.com, protonmail.com).")]
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [Phone]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public DateTime CreationDate { get; set; }= DateTime.Now;
        public bool IsDeleted { get; set; } = false; ///Soft Delete
         
    }
}
