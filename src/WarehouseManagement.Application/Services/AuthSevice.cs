using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Repositories;
using WarehouseManagement.Application.Interfaces.Services;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Exceptions;

namespace WarehouseManagement.Application.Services;

public class AuthService(
    IConfiguration config,
    IGenericRepository<Staff> staffRepo) : IAuthService
{
    private readonly IConfiguration _config = config;
    private readonly IGenericRepository<Staff> _staffRepo = staffRepo;

    public async Task<string> LoginAsync(string email, string password)
    {
        var allStaff = await _staffRepo.ListAllAsync();
        var staff = allStaff.FirstOrDefault(s => s.Email == email);

        if (staff == null || !BCrypt.Net.BCrypt.Verify(password, staff.Password))
            throw new Exception("Username or Password wrong!");

        var key = _config["Jwt:Key"] ?? throw new Exception("JWT Key not setted in appsettings!");
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, staff.Id.ToString()),
            new Claim(ClaimTypes.Email, staff.Email),
        };

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task RegisterAsync(RequestRegister request) 
    {
        var allStaff = await _staffRepo.ListAllAsync();
        if (allStaff.Any(s => s.Email == request.Email)) throw new Exception("Staff already registered");

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var staff = new Staff
        {
            Name = request.Name,
            Email = request.Email,
            Password = hashedPassword,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber
        };

        await _staffRepo.AddAsync(staff);
        await _staffRepo.SaveChangesAsync();
    }
}