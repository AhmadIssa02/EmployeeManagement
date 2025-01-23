using AutoMapper;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.Mapping
{
    public class EmployeeMap : Profile
    {
        public EmployeeMap()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();

        }
    }
}
