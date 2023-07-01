using AutoMapper;
using Demo.DAL.Entities;
using Demo.PL.Models;

namespace Demo.PL.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Employee, EmployeeViewModel>();
            //CreateMap<EmployeeViewModel, Employee>();
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();

        }
    }
}
