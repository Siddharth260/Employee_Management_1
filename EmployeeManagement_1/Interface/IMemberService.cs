using EmployeeManagement_1.Models;

namespace EmployeeManagement_1.Interface
{
    public interface IMemberService
    {
        Task<Member> AddTaskSheetByUId(Member member);
        Task<Member> GetTaskSheetByUId(string UId);
        Task<string> ResignByUId (string uId);
    }
}
