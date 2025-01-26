using AutoMapper;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.Mapping
{
    public class EmployeeMap : Profile
    {
        public EmployeeMap()
        {
            CreateMap<Employee, EmployeeDTO>()
              .ForMember(dest => dest.DeptName, opt => opt.MapFrom(src => src.dept.name)); // Map department name

            // If you also want to map the other way around, you can use:
            CreateMap<EmployeeDTO, Employee>().ReverseMap();
        }
    }
}
