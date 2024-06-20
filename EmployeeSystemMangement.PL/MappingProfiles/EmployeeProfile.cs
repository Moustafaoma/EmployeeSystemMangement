using AutoMapper;
using EmployeeSystemMangement.DAL.Entities;
using EmployeeSystemMangement.PL.ViewModels;

namespace EmployeeSystemMangement.PL.MappingProfiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
