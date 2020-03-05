using NanoCell.Application.Common.Models;
using NanoCell.Application.Common.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NanoCell.Application
{
    public interface IIdentityService
    {
        Task<List<ApplicationUserOutputDto>> GetAllUserAsync();
        Task<string> GetUserNameAsync(string userId);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
