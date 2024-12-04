using EmployeeManagement_1.Interface;
using EmployeeManagement_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _leadService;
        public LeadController(ILeadService leadService)
        {
            _leadService = leadService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Lead leadModel)
        {
            var response = await _leadService.AddEmployee(leadModel);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var response = await _leadService.GetAllEmployee();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeByUId(string UId)
        {
            var response = await _leadService.GetEmployeeByUId(UId);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyTeamByUId(string UId)
        {
            var response = await _leadService.GetMyTeamByUId(UId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployeeByUId(Lead leadModel)
        {
            var response = await _leadService.UpdateEmployeeByUId(leadModel);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveEmployeeByUId(string UId)
        {
            var response = await _leadService.RemoveEmployeeByUId(UId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetTeamTaskSheetByUId(string UId)
        {
            var response = await _leadService.GetTeamTaskSheetByUId(UId);
            return Ok(response);
        }
    }
}
