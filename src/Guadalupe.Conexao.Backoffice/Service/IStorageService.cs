using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Backoffice.Service
{
    public interface IStorageService
    {
        Task UploadFileToBlobAsync(string name, byte[] file, string mimetype);
    }
}
