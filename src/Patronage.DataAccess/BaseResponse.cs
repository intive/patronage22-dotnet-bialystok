using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.DataAccess
{
    public abstract class BaseResponse<T>
    {
        public T responceCode { get; set; }
        public List<T> errors { get; set; }
        public T message { get; set; }
        public T data { get; set; }

       
    }
}
