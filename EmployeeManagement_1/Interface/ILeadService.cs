using EmployeeManagement_1.Models;

namespace EmployeeManagement_1.Interface
{
    public interface ILeadService
    {
        Task<Lead> AddEmployee(Lead leadModel);
        Task<List<Lead>> GetAllEmployee();
        Task<Lead> GetEmployeeByUId(string UId);
        Task<List<Lead>> GetMyTeamByUId(string UId);
        Task <Lead> UpdateEmployeeByUId(Lead leadModel);
        Task <string> RemoveEmployeeByUId(string UId);
        Task <List<Member>> GetTeamTaskSheetByUId(string UId);
    }
}
