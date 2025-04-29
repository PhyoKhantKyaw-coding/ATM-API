using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTOs;
using REPOSITORY.UnitOfWork;

namespace API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public UserController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                var user = await _unitOfWork.User.GetById(userId);
                if (user == null)
                {
                    return NotFound(new ResponseModel { Message = Messages.NoData, Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Messages.Successfully, Status = APIStatus.Successful, Data = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
        [Authorize]
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var users = await _unitOfWork.User.GetAll();
                return Ok(new ResponseModel { Message = Messages.Successfully, Status = APIStatus.Successful, Data = users });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
        [Authorize]
        [HttpPatch("Withdraw")]
        public async Task<IActionResult> Withdraw(UserRequestDTO user)
        {
            try
            {
                var result = await _userService.Withdraw(user);
                return Ok(new ResponseModel { Message = result.Message, Status = APIStatus.Successful, Data = result.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
        [Authorize]
        [HttpPatch("Deposit")]
        public async Task<IActionResult> Deposit(UserRequestDTO user)
        {
            try
            {
                var result = await _userService.Deposit(user);
                return Ok(new ResponseModel { Message = result.Message, Status = APIStatus.Successful, Data = result.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
        [Authorize]
        [HttpGet("CheckBalance")]
        public async Task<IActionResult> CheckBalance(Guid userId)
        {
            try
            {
                var result = await _userService.CheckBalance(userId);
                return Ok(new ResponseModel { Message = Messages.Result, Status = APIStatus.Successful, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
        [HttpPatch("UpdateUserIsLocked")]
        public async Task<IActionResult> UpdateUserIsLocked(Guid userId)
        {
            try
            {
                var result = await _userService.UpdateUserIsLocked(userId);
                return Ok(new ResponseModel { Message = Messages.UpdateSucess, Status = APIStatus.Successful, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
        [Authorize]
        [HttpGet("GetTransactionByUserId")]
        public async Task<IActionResult> GetTransactionByUserId(Guid userId)
        {
            try
            {
                var result = await _userService.GetTransactionByUserId(userId);
                return Ok(new ResponseModel { Message = Messages.Result, Status = APIStatus.Successful, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
        [HttpPost("CreateTranfer")]
        public async Task<IActionResult> CreateTranfer(TranferRequestDTO tranferRequestDTO)
        {
            try
            {
                var result = await _userService.CreateTranfer(tranferRequestDTO);
                return Ok(new ResponseModel { Message = result.Message, Status = APIStatus.Successful, Data = result.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
        
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserCreateDTO userCreateDTO)
        {
            try
            {
                var result= await _userService.CreateUser(userCreateDTO);
                return Ok(new ResponseModel { Message = Messages.Successfully, Status = APIStatus.Successful,Data= result.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
        [Authorize]
        [HttpPatch("ChangePin")]
        public async Task<IActionResult> ChangePin(ChangePinDTO changePin)
        {
            try
            {
                var result = await _userService.CheckPin(changePin);
                return Ok(new ResponseModel { Message = Messages.Result, Status = APIStatus.Successful, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
        [HttpGet("UserIsLocked")]
        public async Task<IActionResult> UserIsLocked(Guid userId)
        {
            try
            {
                var result = await _userService.UserIsLocked(userId);
                return Ok(new ResponseModel { Message = Messages.UpdateSucess, Status = APIStatus.Successful, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
        [Authorize]
        [HttpGet("GetTransferbyUserId")]
        public async Task<IActionResult> GetTransferbyUserId(Guid userId)
        {
            try
            {
                var result = await _userService.GetTransferbyUserId(userId);
                return Ok(new ResponseModel { Message = Messages.Result, Status = APIStatus.Successful, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
    }
}
