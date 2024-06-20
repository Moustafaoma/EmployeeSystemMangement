using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Name { get; set; }

        public int? Age { get; set; }

        public string Address { get; set; }
        public decimal Salary { get; set; }

        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [Phone]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public DateTime CreationDate { get; set; }= DateTime.Now;
        public bool IsDeleted { get; set; } = false; //soft delete
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        [InverseProperty("Employees")]
        public Department Department { get; set; }


    }
}
