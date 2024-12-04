using AutoMapper;
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
        private readonly IMapper _mapper;
        public MemberService(ICosmosDbService dbService, IMapper mapper)
        {
            _dbService = dbService;
            _mapper = mapper;
        }

        public async Task<Member> AddTaskSheetByUId(Member memberModel)
        {
            var task = _mapper.Map<MemberEntity>(memberModel);
            task.initialize(true, Credentials.taskDocumentType, Credentials.createdBy, Credentials.createdByName);
            task.UId = memberModel.UId;
            var response = await _dbService.AddItemAsync(task);
            var responseModel = _mapper.Map<Member>(memberModel);
            responseModel.UId = response.UId;
            return responseModel;
        }

        public async Task<Member> GetTaskSheetByUId(string UId)
        {
            var task = await _dbService.GetTaskSheetByUId(UId);
            var model = _mapper.Map<Member>(task);
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
