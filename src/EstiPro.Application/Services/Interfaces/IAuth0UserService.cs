using Auth0.ManagementApi.Models;
using EstiPro.Application.DTOs.Auth0;
using FluentResults;

namespace EstiPro.Application.Services.Interfaces;

public interface IAuth0UserService
{
    public Task<Result<User>> CreateUserAsync(UserRegistrationDataDto userRegistrationData, CancellationToken cancellationToken = default);
}