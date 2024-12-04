using EmployeeManagement_1.Common;
using EmployeeManagement_1.Cosmos;
using EmployeeManagement_1.Entities;
using EmployeeManagement_1.Interface;
using EmployeeManagement_1.Models;
using System.Drawing;

namespace EmployeeManagement_1.Services
{
    public class MemberService : IMemberService
    {
        private readonly ICosmosDbService _dbService;
        public MemberService (ICosmosDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<Member> AddTaskSheetByUId(Member memberModel)
        {
            var task = new MemberEntity();
            task.Name = memberModel.Name;
            task.Tasksheet = memberModel.Tasksheet;
            task.initialize(true, Credentials.taskDocumentType, Credentials.createdBy, Credentials.createdByName);
            task.UId = memberModel.UId;

            var response = await _dbService.AddItemAsync(task);

            var responseModel = new Member
            {
                UId = response.UId,
                Name = memberModel.Name,
                Tasksheet = memberModel.Tasksheet
            };

            return responseModel;
        }

        public async Task<Member> GetTaskSheetByUId(string UId)
        {
            var task = await _dbService.GetTaskSheetByUId(UId);
            var model = new Member
            {
                UId = task.UId,
                Name = task.Name,
                Tasksheet = task.Tasksheet
            };
            return model;
        }
        public async Task<string> ResignByUId(string UId)
        {
            var existingEmployee = await _dbService.GetEmployeeByUId(UId);
            existingEmployee.Active = false;
            existingEmployee.Archived = true;
            await _dbService.ReplaceAsync(UId);
            return "You have resigned";
        }
    }
}
