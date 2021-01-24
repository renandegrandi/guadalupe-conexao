using System;

namespace Guadalupe.Conexao.App
{
    public class Initialize
    {
        public Pages Page { get; private set; }
        public Guid IdRegister { get; private set; }

        public Initialize(Pages page, Guid id)
        {
            Page = page;
            IdRegister = id;
        }

        public enum Pages 
        {
            Notice = 0,
            Project = 1
        }
    }
}
