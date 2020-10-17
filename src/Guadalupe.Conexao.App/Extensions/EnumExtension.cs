using System;
using System.ComponentModel;
using System.Linq;

namespace Guadalupe.Conexao.App.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Any())
                return attributes.First().Description;

            throw new NotImplementedException("Não foi implementado a annotation (DescriptionAttribute)!");
        }
    }
}
