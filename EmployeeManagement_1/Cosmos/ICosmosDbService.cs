using EmployeeManagement_1.Entities;

namespace EmployeeManagement_1.Cosmos
{
    public interface ICosmosDbService
    {
        Task<E> AddItemAsync<E>(E model);
        Task<List<LeadEntity>> GetAllEmployee();
        Task<LeadEntity> GetEmployeeByUId(string UId);
        Task<List<LeadEntity>> GetMyTeamByUId(string domain);
        Task ReplaceAsync(dynamic entity);
        Task<MemberEntity> GetTaskSheetByUId(string UId);
    }
}
