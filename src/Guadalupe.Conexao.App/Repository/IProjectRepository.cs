using Guadalupe.Conexao.App.Model;
using System;
using System.Collections.Generic;

namespace Guadalupe.Conexao.App.Repository
{
    public interface IProjectRepository
    {
        Project Get(Guid id);
        List<Project> Get();
    }
}
