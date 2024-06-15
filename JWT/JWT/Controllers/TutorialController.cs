/*using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;


 * TODO w pracy domowej
 * Koncowki do logowania, rejestracji oraz refreshowania sesji umiescic w kontrolerze AuthController
 * 
 * 1. Logowanie api/auth/login
 * Input: username (email), password
 * - Sprawdzenie poprawnosci danych uzytkownika
 * - if(true) => generujemy token z krotkim czasem zycia + refresh token z dlugim czasem zycia => 200
 * - if(false) => 401 niepoprny login lub haslo
 * Output: tokeny
 *
 * 2. Refreshowanie sesji api/auth/refresh
 * Input: refresh token
 * - Sprawdzenie czy refresh token czy jest poprawny
 * - if(true) -> generujemy token z krotkim czasem zycia + refresh token z dlugim czasem zycia => 200
 * - if(false) => 401 Invalid token
 * Output: tokeny
 *
 * 3. Rejestacja uzytkownika api/auth/register
 * - Input: username, password
 * - Sprawdzamy czy nazwa uzytkownika jest unikalna
 * - Walidujemy zapytanie
 * - Hashujemy haslo
 * - Tworzymy nowy rekord dla uzytkownika w bazie ktory bedzie zawieral jego username oraz hash ktory wygenerowalismy w ramach hasla
 *
 * 4. Zabezpiecznie jednej koncowki
 

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorialController(IConfiguration config) : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(LoginRequestModel model) 
        {

            if(!(model.UserName.ToLower() == "kacper" && model.Password == "hello-world"))
            {
                return Unauthorized("Wrong username or password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = config["JWT:Issuer"],
                Audience = config["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!)),
                        SecurityAlgorithms.HmacSha256
                )
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            var stringToken = tokenHandler.WriteToken(token);

            var refTokenDescription = new SecurityTokenDescriptor
            {
                Issuer = config["JWT:RefIssuer"],
                Audience = config["JWT:RefAudience"],
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:RefKey"]!)),
                        SecurityAlgorithms.HmacSha256
                )
            };
            var refToken = tokenHandler.CreateToken(refTokenDescription);
            var stringRefToken = tokenHandler.WriteToken(refToken);
            
            return Ok(new LoginResponseModel
            {
                Token = stringToken,
                RefreshToken = stringRefToken
            });
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken(RefreshTokenRequestModel requestModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(requestModel.RefreshToken, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["JWT:RefIssuer"],
                    ValidAudience = config["JWT:RefAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:RefKey"]!))
                }, out SecurityToken validatedToken);
                // return Ok(true + " " + validatedToken);
                return Ok();
                // generowanie kolejnego tokenu oraz refresh tokenu
            }
            catch
            {
                return Unauthorized();
            }
        }

        //Generated password does not work with /verify-password endpoint!
        [HttpGet("hash-password/{password}")]
        public IActionResult HashPassword(string password)
        {
            
            Console.WriteLine("hash-password");

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                new byte[] {0},
                10,
                HashAlgorithmName.SHA512,
                1
            );

            return Ok(Convert.ToHexString(hash));
        }

        [HttpGet("hash-password-with-salt/{password}")]
        public IActionResult HashPasswordWithSalt(string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return Ok(passwordHasher.HashPassword(new User(), password));
        }

        [HttpPost("verify-password")]
        public IActionResult VerifyPassword(VerifyPasswordRequestModel requestModel)
        {
            var passwordHasher = new PasswordHasher<User>();
            return Ok(passwordHasher.VerifyHashedPassword(new User(), requestModel.Hash, requestModel.Password) == PasswordVerificationResult.Success);
        }
    }

    public class RefreshTokenRequestModel
    {
        public string RefreshToken { get; set; } = null!;
    }

    public class VerifyPasswordRequestModel
    {
        public string Password { get; set; } = null!;
        public string Hash { get; set; } = null!;
    }
    
    public class LoginRequestModel
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }

    public class LoginResponseModel
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }

    public class User
    {
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
*/