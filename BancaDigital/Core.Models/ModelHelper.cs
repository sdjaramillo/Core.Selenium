using Core.Models.Entidad.Transferencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public static class ModelHelper
    {
        public static Type GetObject(string type)
        {
            Type tipo = Type.GetType(type);

            return tipo;
        }
    }
}
