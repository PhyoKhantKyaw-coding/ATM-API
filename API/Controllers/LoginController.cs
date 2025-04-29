using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTOs;
using REPOSITORY.UnitOfWork;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase

{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoginService _loginService;
    public LoginController(IUnitOfWork unitOfWork, ILoginService loginService)
    {
        _unitOfWork = unitOfWork;
        _loginService = loginService;
    }
    [HttpPost("LoginWeb")]
    public async Task<IActionResult> LoginWeb([FromBody] LoginRequestDTO loginDTO)
    {
        try
        {
            var result = _loginService.LoginWeb(loginDTO);
            return Ok(new ResponseModel { Message = result.Result.Message, Status = APIStatus.Successful, Data = result.Result });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }

}
