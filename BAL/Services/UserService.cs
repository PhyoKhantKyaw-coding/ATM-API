using BAL.IServices;
using BAL.Shared;
using MODEL.ApplicationConfig;
using MODEL.DTOs;
using MODEL.Entities;
using REPOSITORY;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services;


internal class UserService:IUserService 
{
    private readonly IUnitOfWork _unitOfWork;
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<bool> UpdateUserIsLocked(Guid userId)
    {
        try
        {
            var user = await _unitOfWork.User.GetById(userId);
            if (user != null)
            {
                user.IsLocked = "N";
                _unitOfWork.User.Update(user);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            throw;
        }

    }
    public async Task<bool> UserIsLocked(Guid userId)
    {
        try
        {
            var user = await _unitOfWork.User.GetById(userId);
            if (user != null)
            {
                user.IsLocked = "Y";
                _unitOfWork.User.Update(user);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            throw;
        }

    }
    public async Task<UserResponseDTO> Withdraw(UserRequestDTO user)
    {
        var item = await _unitOfWork.User.GetById(user.Id);
        if (item == null)
        {
            return new UserResponseDTO
            {
                Message = "User not found.",
                Data = null
            };
        }
        if(item.IsLocked == "Y")
        {
            return new UserResponseDTO
            {
                Message = "User is inactive.",
                Data = null
            };
        }
        if (user.Pin != item.Pin)
            return new UserResponseDTO
            {
                Message = "Invalid PIN.",
                Data = null
            };

        if (user.Amount <= 0)
            return new UserResponseDTO
            {
                Message = "Amount must be greater than zero.",
                Data = null
            };

        if (user.Amount > item.Wallet)
            return new UserResponseDTO
            {
                Message = "Insufficient funds.",
                Data = null
            };

        item.Wallet -= user.Amount;

        var transaction = new Transaction
        {
            UserID = user.Id,
            Amount = user.Amount,
            TransactionDate = DateTime.Now,
            TransactionType = "Withdraw"
        };

        await _unitOfWork.Transaction.Add(transaction);
        _unitOfWork.User.Update(item);
        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0
            ? new UserResponseDTO
            {
                Message = $"Withdrawal successful. New balance: ${item.Wallet}",
                Data = item
            }
            : new UserResponseDTO
            {
                Message = "Transaction failed to save.",
                Data = null
            };
    }

    public async Task<UserResponseDTO> Deposit(UserRequestDTO user)
    {
        var item = await _unitOfWork.User.GetById(user.Id);
        if (item == null)
        {
            return new UserResponseDTO
            {
                Message = "User not found.",
                Data = null
            };
        }
        if (item.IsLocked == "Y")
        {
            return new UserResponseDTO
            {
                Message = "User is inactive.",
                Data = null
            };
        }
        if (user.Pin != item.Pin)
            return new UserResponseDTO
            {
                Message = "Invalid PIN.",
                Data = null
            };
        if (user.Amount <= 0)
            return new UserResponseDTO
            {
                Message = "Amount must be greater than zero.",
                Data = null
            };
        item.Wallet += user.Amount;

        var transaction = new Transaction
        {
            UserID = user.Id,
            Amount = user.Amount,
            TransactionDate = DateTime.Now,
            TransactionType = "Deposit"
        };

        await _unitOfWork.Transaction.Add(transaction);
        _unitOfWork.User.Update(item);
        var result = await _unitOfWork.SaveChangesAsync();

        return result > 0
            ? new UserResponseDTO
            {
                Message = $"Deposit successful. New balance: ${item.Wallet}",
                Data = item
            }
            : new UserResponseDTO
            {
                Message = "Transaction failed to save.",
                Data = null
            };
    }

    public async Task<decimal?> CheckBalance(Guid userId)
    {
        try
        {
            var user = await _unitOfWork.User.GetById(userId);
            return user?.Wallet;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Transaction>> GetTransactionByUserId(Guid userId)
    {
        try
        {
            var transactions = await _unitOfWork.Transaction.GetByCondition(x => x.UserID == userId);

            if (transactions == null || !transactions.Any())
            {
                throw new Exception("No transactions found for this user.");
            }

            return transactions.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<UserResponseDTO> CreateTranfer(TranferRequestDTO tranferRequestDTO)
    {
        var sender = await _unitOfWork.User.GetById(tranferRequestDTO.FromId);
        var receiver = (await _unitOfWork.User.GetByCondition(User => User.AcountNumber == tranferRequestDTO.ToAcountNumber)).FirstOrDefault();

        if ( receiver == null)
        {
            return new UserResponseDTO
            {
                Message = "Receiver not found.",
                Data = null
            };
        }
        if (sender == null)
        {
            return new UserResponseDTO
            {
                Message = "Sender not found.",
                Data = null
            };
        }
        if (sender.IsLocked == "Y" && receiver.IsLocked=="Y")
        {
            return new UserResponseDTO
            {
                Message = "User is inactive.",
                Data = null
            };
        }
        if(tranferRequestDTO.Pin != sender.Pin)
            return new UserResponseDTO
            {
                Message = "Invalid PIN.",
                Data = null
            };
        if (tranferRequestDTO.Amount > sender.Wallet)
            return new UserResponseDTO
            {
                Message = "Insufficient funds.",
                Data = null
            };
        sender.Wallet -= tranferRequestDTO.Amount;
        receiver.Wallet += tranferRequestDTO.Amount;
        var transfer = new Transfer
        {
            SenderID = sender.UserID,
            ReceiverID = receiver.UserID,
            Amount = tranferRequestDTO.Amount,
            TransferDate = DateTime.Now,
            TransferType = "Transfer",
            Description = tranferRequestDTO.Description

        };
        await _unitOfWork.Transfer.Add(transfer);
        _unitOfWork.User.Update(sender);
        _unitOfWork.User.Update(receiver);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0
            ? new UserResponseDTO
            {
                Message = $"Transfer successful. New balance: ${sender.Wallet}",
                Data = new TransferDTO
                {
                    senderName = sender.UserName,
                    receiverName = receiver.UserName,
                    Amount = tranferRequestDTO.Amount,
                    TransferDate = transfer.TransferDate,
                    Description = tranferRequestDTO.Description
                }
            }
            : new UserResponseDTO
            {
                Message = "Transaction failed to save.",
                Data = null
            };
    }
    public async Task<bool> CheckPin(ChangePinDTO changePin)
    {
        var user = await _unitOfWork.User.GetById(changePin.Id);
        if (user == null)
        {
            return false;
        }
        if(user.IsLocked == "Y")
        {
            return false;
        }
        if (changePin.OldPin != user.Pin)
        {
            return false;
        }
        if (changePin.NewPin != changePin.ConfirmPin)
        {
            return false;
        }
        user.Pin = changePin.NewPin;
        _unitOfWork.User.Update(user);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0;
    }
    public async Task<UserResponseDTO> CreateUser(UserCreateDTO user)
    {
        // Generate Account Number (Assuming starting from 1000)
        int accountNumber = await GenerateAccountNumber();

        // Create password hash
        CommonAuthentication.CreatePasswordHash(user.Password, out byte[] passwordhash);

        // Create new user
        var newUser = new User
        {
            UserName = user.UserName,
            AcountNumber = accountNumber, // Use generated account number
            PasswordHash = passwordhash,
            Wallet = user.Wallet,
            Pin = user.Pin
        };
        // Add user to the database
        var result =  _unitOfWork.User.Add(newUser);
        await _unitOfWork.SaveChangesAsync();
        if (result != null)
        {
            await _unitOfWork.SaveChangesAsync();
            return new UserResponseDTO
            {
                Message = "User created successfully.",
                Data = newUser
            };
        }
        else
        {
            return new UserResponseDTO
            {
                Message = "Failed to create user.",
                Data = null
            };
        }
    }
    private async Task<int> GenerateAccountNumber()
    {
        var users = await _unitOfWork.User.GetByCondition(u => u.AcountNumber > 0 && u.Wallet > 100);

        var lastAccountNumber = users.Any() ? users.Max(u => u.AcountNumber) : 1000;

        return lastAccountNumber + 1;  
    }
    public async Task<List<TransferDTO>> GetTransferbyUserId(Guid id)
    {
        var user = await _unitOfWork.User.GetById(id);
        if (user == null)
        {
            throw new Exception("User not found.");
        }
        var transfers = await _unitOfWork.Transfer.GetByCondition(x => x.SenderID == user.UserID || x.ReceiverID == user.UserID);
        List<TransferDTO> transferDTOs = new List<TransferDTO>();
        foreach (var transfer in transfers)
        {
            var transferDTO = new TransferDTO
            {
                senderName = (await _unitOfWork.User.GetById(transfer.SenderID)).UserName,
                receiverName = (await _unitOfWork.User.GetById(transfer.ReceiverID)).UserName,
                Amount = transfer.Amount,
                TransferDate = transfer.TransferDate,
                Description = transfer.Description
            };
            transferDTOs.Add(transferDTO);
        }
        if (transfers == null || !transfers.Any())
        {
            throw new Exception("No transfers found for this user.");

        }
        return transferDTOs;
    }

   

}
