using MODEL.ApplicationConfig;
using MODEL.DTOs;
using MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices;

public interface IUserService
{
    Task<bool> UpdateUserIsLocked(Guid userId);
    Task<bool> UserIsLocked(Guid userId);
    Task<UserResponseDTO> CreateUser(UserCreateDTO userCreateDTO);
    Task<UserResponseDTO> Withdraw(UserRequestDTO user);
    Task<UserResponseDTO> Deposit(UserRequestDTO user);
    Task<UserResponseDTO> CreateTranfer(TranferRequestDTO tranferRequestDTO);
    Task<List<Transaction>> GetTransactionByUserId(Guid userId);
    Task<decimal?> CheckBalance(Guid userId);
    Task<bool> CheckPin(ChangePinDTO changePin);
    Task<List<TransferDTO>> GetTransferbyUserId(Guid userId);


}
