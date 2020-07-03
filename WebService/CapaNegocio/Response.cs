using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class Response
    {
        private int code;
        private string message;
        private Object data;
        

        public int Code { get => code; set => code = value; }
        public string Message { get => message; set => message = value; }
        public Object Data { get => data; set => data = value; }
       
    }
}
