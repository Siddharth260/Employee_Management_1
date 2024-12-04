using EmployeeManagement_1.Common;
using EmployeeManagement_1.Entities;
using Microsoft.Azure.Cosmos;
namespace EmployeeManagement_1.Cosmos
{
    public class CosmosDbService : ICosmosDbService
    {
        public readonly CosmosClient _client;
        public readonly Container _container;
        public CosmosDbService ()
        {
            _client = new CosmosClient(Credentials.cosmosUrl, Credentials.authToken);
            _container = _client.GetContainer(Credentials.databaseName, Credentials.containerName);
        }

        public async Task<E> AddItemAsync<E>(E model)
        {
            try
            {
                var response = await _container.CreateItemAsync(model);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LeadEntity>> GetAllEmployee()
        {
            try
            {
                var response = _container.GetItemLinqQueryable<LeadEntity>(true).Where(e => e.dType != Credentials.taskDocumentType && e.Active == true && e.Archived == false).AsEnumerable().ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LeadEntity> GetEmployeeByUId(string UId)
        {
            try
            {
                var response = _container.GetItemLinqQueryable<LeadEntity>(true).Where(e => e.UId == UId && e.Active == true && e.Archived == false).AsEnumerable().FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MemberEntity> GetTaskSheetByUId(string UId)
        {
            try
            {
                var response = _container.GetItemLinqQueryable<MemberEntity>(true).Where(e => e.UId == UId && e.dType == Credentials.taskDocumentType && e.Active == true && e.Archived == false).AsEnumerable().FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LeadEntity>> GetMyTeamByUId(string domian)
        {
            try
            {
                var response = _container.GetItemLinqQueryable<LeadEntity>(true).Where(e => e.Domain == domian && e.Designation == "Member" &&  e.Active == true && e.Archived == false).AsEnumerable().ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ReplaceAsync(dynamic entity)
        {
            var response = await _container.ReplaceItemAsync(entity, entity.Id);
        }
    }
}
