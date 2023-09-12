using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECOMMERSE.Models
{
    public class ResponseDataDTO
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResponseDataDTO()
        {


        }
        public ResponseDataDTO(bool result, string message, object data)
        {
            Result = result;
            Message = message;
            Data = data;
        }
    }
}