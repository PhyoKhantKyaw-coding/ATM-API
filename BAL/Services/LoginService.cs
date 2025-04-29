using BAL.IServices;
using BAL.Shared;
using MODEL.DTOs;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services;

internal class LoginService : ILoginService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CommonTokenGenerator _commonTokenGenerator;
    public LoginService(IUnitOfWork unitOfWork, CommonTokenGenerator commonTokenGenerator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _commonTokenGenerator = commonTokenGenerator;
    }
    public async Task<LoginResponseDTO> LoginWeb(LoginRequestDTO loginDTO)
    {
        try
        {
            var returndata = new LoginResponseDTO();
            var emailAttribute = new EmailAddressAttribute();
            var userdata = (await _unitOfWork.User.GetByCondition(x => x.UserName == loginDTO.UserName && x.AcountNumber == loginDTO.AccountNumber)).FirstOrDefault();
            if(userdata == null)
            {
                returndata.AccountStatus = false;
                returndata.Message = "Invalid username or password.";
                return returndata;
            }
            else if (userdata.IsLocked == "Y")
            {
                returndata.AccountStatus = true;
                returndata.PasswordStatus = false;
                returndata.Message = "User is inactive.";
                return returndata;
            }
            else
            {
                var checkpassword = CommonAuthentication.VerifyPasswordHash(loginDTO.Password, userdata.PasswordHash);
                if (checkpassword )
                {
                    returndata.PasswordStatus = true;
                    returndata.AccountStatus = true;
                    returndata.Message = "Login successful.";
                    returndata.Token = _commonTokenGenerator.Create(userdata);
                    returndata.Data = userdata;
                    returndata.Issuccess = true;
                    return returndata;
                }
                else
                {
                    returndata.AccountStatus = true;
                    returndata.PasswordStatus = false;
                    returndata.Issuccess = false;
                    returndata.Message = "Incorrect password.";
                    returndata.Data = userdata.UserID;
                    return returndata;
                }

            }
        }
        catch (Exception)
        {

            throw;
        }
    }

}
