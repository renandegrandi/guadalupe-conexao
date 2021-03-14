using System;
using System.Collections.Generic;
using System.Text;

namespace Guadalupe.Conexao.App.Model
{
    public class Liturgy
    {
        public Reading FirstReading { get; set; }
        public Reading SecondReading { get; set; }
        public Reading Psalm { get; set; }
        public Reading Gospel { get; set; }
    }
}
