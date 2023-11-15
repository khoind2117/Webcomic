using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using Webcomic.Models;
using Webcomic.Models.DTOs;
using Webcomic.Models.Entities;
using Webcomic.Services.Interfaces;

namespace Webcomic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(login.UserName);

                // Kiểm tra user và password
                if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    // Tạo Access Token
                    var accessToken = await _tokenService.GenerateAccessToken(user, userRoles);

                    // Tạo Refresh Token
                    var refreshToken = await _tokenService.GenerateRefreshToken();

                    // Cập nhật Refresh Token và thời gian hết hạn vào cơ sở dữ liệu
                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                    await _userManager.UpdateAsync(user);

                    // Tạo AuthResult
                    var authResult = new AuthResult
                    {
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                        RefreshToken = refreshToken,
                        Expiration = accessToken.ValidTo
                    };
                    // Trả về kết quả
                    return Ok(authResult);
                }
                else
                {
                    // Xử lý lỗi khi người dùng đăng nhập không thành công
                    return BadRequest("Invalid Login Attempt");
                }
            }

            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = register.UserName,
                    Email = register.Email,
                };

                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    // Kiểm tra và tạo vai trò nếu chưa tồn tại
                    if (!await _roleManager.RoleExistsAsync("Admin") || !await _roleManager.RoleExistsAsync("User"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    }

                    // Đảm bảo người dùng đăng nhập thành công trước khi gán vai trò
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Gán vai trò cho người dùng
                    await _userManager.AddToRoleAsync(user, "User");

                    var userRoles = await _userManager.GetRolesAsync(user);
                    var accessToken = await _tokenService.GenerateAccessToken(user, userRoles);
                    var refreshToken = await _tokenService.GenerateRefreshToken();

                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                    await _userManager.UpdateAsync(user);

                    // Tạo AuthResult
                    var authResult = new AuthResult
                    {
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                        RefreshToken = refreshToken,
                        Expiration = accessToken.ValidTo
                    };
                    // Trả về kết quả
                    return Ok(authResult);
                }
                else
                {
                    // Xử lý lỗi khi tạo người dùng không thành công
                    return BadRequest(result.Errors);
                }
            }

            return BadRequest(ModelState);
        }
    }
}
