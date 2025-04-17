// TRODHA.Core/Interfaces/IImportanceLevelRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using TRODHA.Core.Models;

namespace TRODHA.Core.Interfaces
{
    public interface IImportanceLevelRepository : IRepository<ImportanceLevel>
    {
        Task<ImportanceLevel?> GetByNameAsync(string levelName);
    }
}
