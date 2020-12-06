using Guadalupe.Conexao.App.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Repository
{
    public interface INoticeRepository
    {
        Task<List<Notice>> GetAsync();
        Task<List<Notice>> GetAsync(Guid[] ids);
        Task RemoveAsync(Guid[] ids);
        Task InsertAsync(List<Notice> notices);
        Task UpdateAsync(List<Notice> notices);
    }
}
