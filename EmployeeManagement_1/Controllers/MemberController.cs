using EmployeeManagement_1.Interface;
using EmployeeManagement_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        //private readonly ILeadService _leadService;

        public MemberController(IMemberService memberService, ILeadService leadService)
        {
            _memberService = memberService;
            //_leadService = leadService;
        }

        //public async Task<IActionResult> UpdateMyProfile (Lead leadModel)
        //{

        //}

        [HttpPost]
        public async Task<IActionResult> AddTaskSheetByUId(Member memberModel)
        {
            var response = await _memberService.AddTaskSheetByUId(memberModel);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskSheetUId(string UId)
        {
            var response = await _memberService.GetTaskSheetByUId(UId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ResignByUId(string UId)
        {
            var response = await _memberService.ResignByUId(UId);
            return Ok(response);
        }
    }
}
