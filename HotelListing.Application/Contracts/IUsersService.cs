using HotelListing.App.Application.DTOs.Auth;
using HotelListing.App.Common.Results;

namespace HotelListing.App.Application.Contracts;

public interface IUsersService
{
    string UserId { get; }
    Task<Result<string>> LoginAsync(LoginUserDto dto);
    Task<Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto);
}