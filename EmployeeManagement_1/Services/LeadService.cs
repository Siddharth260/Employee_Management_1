using AutoMapper;
using EmployeeManagement_1.Common;
using EmployeeManagement_1.Cosmos;
using EmployeeManagement_1.Entities;
using EmployeeManagement_1.Interface;
using EmployeeManagement_1.Models;

namespace EmployeeManagement_1.Services
{
    public class LeadService : ILeadService
    {
        private readonly ICosmosDbService _dbService;
        private readonly IMapper _mapper;
        public LeadService(ICosmosDbService dbService, IMapper mapper)
        {
            _dbService = dbService;
            _mapper = mapper;
        }

        public async Task<Lead> AddEmployee(Lead leadModel)
        {
            var employee = _mapper.Map<LeadEntity>(leadModel);
            employee.DateOfJoining = DateTime.Today;
            if (employee.Designation == "Lead")
            {
                employee.initialize(true, Credentials.leadDocumentType, Credentials.createdBy, Credentials.createdByName);
            }
            else if (employee.Designation == "Member")
            {
                employee.initialize(true, Credentials.memberDocumentType, Credentials.createdBy, Credentials.createdByName);
            }
            var response = await _dbService.AddItemAsync(employee);
            var responseModel = _mapper.Map<Lead>(response);
            return responseModel;
        }

        public async Task<List<Lead>> GetAllEmployee()
        {
            var response = await _dbService.GetAllEmployee();
            var responseList = new List<Lead>();
            foreach (var item in response)
            {
                var model = _mapper.Map<Lead>(item);
                responseList.Add(model);
            }
            return responseList;
        }

        public async Task<Lead> GetEmployeeByUId(string UId)
        {
            var response = await _dbService.GetEmployeeByUId(UId);
            var model = _mapper.Map<Lead>(response);
            return model;
        }

        public async Task<List<Lead>> GetMyTeamByUId(string UId)
        {
            var lead = await _dbService.GetEmployeeByUId(UId);
            var response = await _dbService.GetMyTeamByUId(lead.Domain);
            var responseList = new List<Lead>();

            foreach (var item in response)
            {
                var model = _mapper.Map<Lead>(item);
                responseList.Add(model);
            }
            return responseList;
        }

        public async Task<Lead> UpdateEmployeeByUId(Lead leadModel)
        {
            var existingEmployee = await _dbService.GetEmployeeByUId(leadModel.UId);
            existingEmployee.Active = false;
            existingEmployee.Archived = true;

            await _dbService.ReplaceAsync(existingEmployee);

            if (existingEmployee.Designation == "Lead")
            {
                existingEmployee.initialize(false, Credentials.leadDocumentType, Credentials.createdBy, Credentials.createdByName);
            }
            else if (existingEmployee.Designation == "Member")
            {
                existingEmployee.initialize(false, Credentials.memberDocumentType, Credentials.createdBy, Credentials.createdByName);
            }

            existingEmployee.Name = leadModel.Name;
            existingEmployee.Designation = leadModel.Designation;
            existingEmployee.Domain = leadModel.Domain;
            existingEmployee.DateOfJoining = DateTime.Today;

            var response = await _dbService.AddItemAsync(existingEmployee);
            var responseModel = _mapper.Map<Lead>(existingEmployee);
            return responseModel;
        }

        public async Task<string> RemoveEmployeeByUId(string UId)
        {
            var existingEmployee = await _dbService.GetEmployeeByUId(UId);
            existingEmployee.Active = false;
            existingEmployee.Archived = true;

            await _dbService.ReplaceAsync(existingEmployee);
            return "Employee Removed From The Company";
        }

        public async Task<List<Member>> GetTeamTaskSheetByUId(string UId)
        {
            var lead = await _dbService.GetEmployeeByUId(UId);
            var teamList = await _dbService.GetMyTeamByUId(lead.Domain);
            var taskList = new List<Member>();

            foreach (var member in teamList)
            {
                var response = await _dbService.GetTaskSheetByUId(member.UId);
                var model = _mapper.Map<Member>(response);
                taskList.Add(model);
            }
            return taskList;
        }
    }
}
