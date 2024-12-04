using AutoMapper;
using EmployeeManagement_1.Entities;
using EmployeeManagement_1.Models;
namespace EmployeeManagement_1.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LeadEntity, Lead>().ReverseMap();
            CreateMap<MemberEntity, Member>().ReverseMap();

        }
    }
}
