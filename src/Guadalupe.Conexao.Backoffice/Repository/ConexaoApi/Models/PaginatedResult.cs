using System.Collections.Generic;

namespace Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models
{
    public class PaginatedResult<T>
    {
        public int TotalRegisters { get; set; }
        public List<T> Registers { get; set; }
    }
}
