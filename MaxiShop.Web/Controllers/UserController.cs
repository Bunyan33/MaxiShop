using Azure;
using MaxiShop.Application.ApplicationConstants;
using MaxiShop.Application.Common;
using MaxiShop.Application.InputModels;
using MaxiShop.Application.Services;
using MaxiShop.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MaxiShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly APIResponse _response;

        public UserController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<APIResponse>> Register(Register register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.AddError(ModelState.ToString());
                    _response.AddWarning(CommonMessage.RegistrationFailed);
                    return _response;
                }

                var result = await _authService.Register(register);

                

                _response.statusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.RegistrationSuccess;
                _response.Result = result;
            }
            catch (Exception)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }
            return Ok(_response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<APIResponse>> Login(Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.AddError(ModelState.ToString());
                    _response.AddWarning(CommonMessage.LoginFailed);
                    return _response;
                }

                var result = await _authService.Login(login);

                if (result is string)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;                    
                    _response.DisplayMessage = CommonMessage.LoginFailed;
                    _response.Result = result;
                    return _response;
                } 

                _response.statusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommonMessage.LoginSuccess;
                _response.Result = result;
            }
            catch (Exception)
            {
                _response.statusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessage.SystemError);
            }
            return Ok(_response);
        }
    }
}
