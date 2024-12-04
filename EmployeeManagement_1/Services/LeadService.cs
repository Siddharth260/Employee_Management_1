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
        public LeadService (ICosmosDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<Lead> AddEmployee(Lead leadModel)
        {
            var employee = new LeadEntity();
            employee.Name = leadModel.Name;
            employee.Designation = leadModel.Designation;
            employee.Domain = leadModel.Domain;
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

            var responseModel = new Lead()
            {
                UId = employee.UId,
                Name = employee.Name,
                Designation = employee.Designation,
                Domain = employee.Domain,
                DateOfJoining = employee.DateOfJoining
            };

            return responseModel;
            
        }

        public async Task<List<Lead>> GetAllEmployee()
        {
            var response = await _dbService.GetAllEmployee();
            var responseList = new List<Lead>();
            
            foreach (var item in response)
            {
                var model = new Lead()
                {
                    UId = item.UId,
                    Name = item.Name,
                    DateOfJoining = item.DateOfJoining,
                    Designation = item.Designation,
                    Domain = item.Domain
                };
                responseList.Add(model);
            }
            return responseList;
        }

        public async Task<Lead> GetEmployeeByUId(string UId)
        {
            var response = await _dbService.GetEmployeeByUId(UId);
            var model = new Lead()
            {
                UId = response.UId,
                Name = response.Name,
                DateOfJoining = response.DateOfJoining,
                Designation = response.Designation,
                Domain = response.Domain
            };
            return model;
        }
        public async Task<List<Lead>> GetMyTeamByUId(string UId)
        {
            var lead = await _dbService.GetEmployeeByUId(UId);
            var response = await _dbService.GetMyTeamByUId(lead.Domain);
            var responseList = new List<Lead>();

            foreach (var item in response)
            {
                var model = new Lead()
                {
                    UId = item.UId,
                    Name = item.Name,
                    DateOfJoining = item.DateOfJoining,
                    Designation = item.Designation,
                    Domain = item.Domain
                };
                responseList.Add(model);
            }
            return responseList;
        }

        public async Task<Lead> UpdateEmployeeByUId(Lead leadModel)
        {
            var existingEmployee = await _dbService.GetEmployeeByUId(leadModel.UId);
            existingEmployee.Active = false;
            existingEmployee.Archived = true;
            //existingEmployee.dType = leadModel.Designation;

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

            var responseModel = new Lead()
            {
                Name = existingEmployee.Name,
                UId = existingEmployee.UId,
                Designation = existingEmployee.Designation,
                Domain = existingEmployee.Domain,
                DateOfJoining = existingEmployee.DateOfJoining
            };
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
                var model = new Member()
                {
                    UId = response.UId,
                    Name = response.Name,
                    Tasksheet = response.Tasksheet
                };
                taskList.Add(model);
            }
            return taskList;
        }
    }
}
