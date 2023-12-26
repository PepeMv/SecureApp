using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Clases
{
    public class EntradasInvalidasException : Exception
    {
        public EntradasInvalidasException() : base("Entradas inválidas para el procesamiento.")
        {
        }
    }
}
