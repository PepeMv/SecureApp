using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Clases
{
    public class LoginResponse
    {
        public LoginResponse(string token, string refresh)
        {
            Token = token;
            Refresh = refresh;
        }

        public string Token { get; set; }
        public string Refresh { get; set; }
    }
}
