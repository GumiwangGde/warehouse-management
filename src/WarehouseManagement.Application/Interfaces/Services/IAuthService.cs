using Microsoft.AspNetCore.Identity.Data;
using WarehouseManagement.Application.DTOs;

namespace WarehouseManagement.Application.Interfaces.Services;

public interface IAuthService
{
    Task<string> LoginAsync(string username, string password);
    Task RegisterAsync(RequestRegister request);
}