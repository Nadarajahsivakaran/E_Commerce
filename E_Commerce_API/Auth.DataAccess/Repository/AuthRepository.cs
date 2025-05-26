using Auth.DataAccess.IRepository;
using Auth.Models;
using Auth.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.DataAccess.Repository
{
    public class AuthRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, ApplicationDbContext dbContext, IOptions<JwtOptions> jwtOption) : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly JwtOptions jwtOptions = jwtOption.Value;
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly ServiceResponse _serviceResponse = new();

        #region Login
        public async Task<ServiceResponse> Login(Login login)
        {
            try
            {
                ApplicationUser? user = await _userManager.FindByNameAsync(login.UserName);

                if (user == null)
                {
                    _serviceResponse.IsSuccess = false;
                    _serviceResponse.Res = "Invalid UserName or Password";
                    return _serviceResponse;
                }

                bool isPasswordOk = await _userManager.CheckPasswordAsync(user, login.Password);

                if (!isPasswordOk)
                {
                    _serviceResponse.IsSuccess = false;
                    _serviceResponse.Res = "Invalid UserName or Password";
                    return _serviceResponse;
                }

                IEnumerable<string> roles = await _userManager.GetRolesAsync(user);
                UserDTO logginUser = _mapper.Map<UserDTO>(user);
                logginUser.Role = string.Join(", ", roles);

                LoginDTO loginDTO = new()
                {
                    UserDTO = logginUser,
                    Token = GenerateToken(user, logginUser.Role)
                };

                _serviceResponse.Res = loginDTO;
                return _serviceResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Login: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region Register
        public async Task<ServiceResponse> Register(Register register)
        {
            try
            {
                ApplicationUser? isEmailExit = await _userManager.FindByEmailAsync(register.Email);
                if (isEmailExit != null)
                {
                    _serviceResponse.IsSuccess = false;
                    _serviceResponse.Res = "Email already exist!";
                    return _serviceResponse;
                }

                ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(register);
                IdentityResult result = await _userManager.CreateAsync(applicationUser, register.Password);

                if (result.Succeeded)
                {
                    //ApplicationUser? newUser = await _dbContext.ApplicationUser.FirstOrDefaultAsync(U => U.UserName == register.Email);
                    UserDTO user = _mapper.Map<UserDTO>(applicationUser);

                    string roleName = register.Roles.ToString().ToUpper();
                    bool roleExist = await _roleManager.RoleExistsAsync(roleName);

                    if (!roleExist)
                    {
                        IdentityRole role = new(roleName);
                        await _roleManager.CreateAsync(role);
                    }

                    await _userManager.AddToRoleAsync(applicationUser, roleName);
                    user.Role = roleName;

                    _serviceResponse.Res = user;
                    return _serviceResponse;
                }

                _serviceResponse.IsSuccess = false;
                return _serviceResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Register: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region GenerateToken
        private string? GenerateToken(ApplicationUser user,string roleName)
        {
            try
            {
                SymmetricSecurityKey securityKey = new(Encoding.ASCII.GetBytes(jwtOptions.Secrete));
                SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);
               
                List<Claim> claims =
                [
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Name,user.FirstName),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                    new Claim(ClaimTypes.Role,roleName)
                ];

                JwtSecurityToken token = new(
                   issuer: jwtOptions.Issuer,
                   audience: jwtOptions.Audience,
                   claims: claims,
                   expires: DateTime.UtcNow.AddDays(1),
                   signingCredentials: credentials
                );
                Console.WriteLine($"Token Expiry: {token.ValidTo}");
                Console.WriteLine($"now time: {DateTime.UtcNow}");

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GenerateToken: {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
