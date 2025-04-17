using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRODHA.Core.Models;

namespace TRODHA.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
    }
}
