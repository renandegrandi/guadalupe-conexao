using System.Collections.Generic;

namespace Guadalupe.Conexao.Api.Models.V1
{
    public class PaginatedResult<T>
    {
        public int TotalRegisters { get; set; }
        public List<T> Registers { get; set; }
    }
}
