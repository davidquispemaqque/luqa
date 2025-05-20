using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using luqa_backend.Models;

namespace luqa_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly LuqaContext _context;

    public AuthController(IConfiguration configuration, LuqaContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        var user = _context.Usuarios.SingleOrDefault(u => u.CorreoElectronico == login.CorreoElectronico);

        if (user == null || !BCrypt.Net.BCrypt.Verify(login.Contraseña, user.Contraseña))
            return Unauthorized(new { message = "Correo o contraseña incorrectos" });

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.NombreCompleto),
            new Claim(ClaimTypes.Email, user.CorreoElectronico)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(90),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = tokenString });
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterModel model)
    {
        if (_context.Usuarios.Any(u => u.CorreoElectronico == model.CorreoElectronico))
            return BadRequest("El correo ya está registrado");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Contraseña);

        var usuario = new Usuario
        {
            NombreCompleto = model.NombreCompleto,
            CorreoElectronico = model.CorreoElectronico,
            Contraseña = hashedPassword,
            FechaRegistro = DateTime.Now
        };

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        return Ok(new { message = "Usuario registrado correctamente" });
    }
}