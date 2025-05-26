using Auth.DataAccess.IRepository;
using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Product.Models.DTO;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepository authRepository) : ControllerBase
    {
        private readonly List<string> errors = [];
        private readonly Response _response = new();
        private readonly IAuthRepository _authRepository = authRepository;


        #region Register
        [HttpPost("Register")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(Register register)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ServiceResponse serviceResponse = await _authRepository.Register(register);

                    if (serviceResponse.IsSuccess)
                    {
                        _response.StatusCode = StatusCodes.Status201Created;
                        _response.Res = serviceResponse.Res;
                        return Ok(_response);
                    }
                    _response.StatusCode = StatusCodes.Status400BadRequest;
                    _response.Message = serviceResponse.Res;
                    return BadRequest(_response);
                }
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }
        #endregion

        #region Login
        [HttpPost("Login")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                ServiceResponse serviceResponse = await _authRepository.Login(login);

                if (serviceResponse.IsSuccess)
                {
                    _response.StatusCode = StatusCodes.Status200OK;
                    _response.Res = serviceResponse.Res;
                    return Ok(_response);
                }

                return HandleException(null, StatusCodes.Status400BadRequest, serviceResponse.Res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        #endregion


        #region HandleException
        private IActionResult HandleException(Exception? ex, int statusCode = StatusCodes.Status400BadRequest, object? customMessage = null)
        {
            try
            {
                if (ex != null)
                    errors.Add(ex.Message.ToString());

                _response.IsSuccess = false;
                _response.StatusCode = statusCode;
                _response.Message = customMessage ?? errors;

                return statusCode switch
                {
                    StatusCodes.Status401Unauthorized => Unauthorized(_response),
                    StatusCodes.Status403Forbidden => Forbid(),
                    StatusCodes.Status404NotFound => NotFound(_response),
                    StatusCodes.Status409Conflict => Conflict(_response),
                    _ => BadRequest(_response)
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
