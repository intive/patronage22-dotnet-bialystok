using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.DataAccess
{
    public abstract class BaseResponse<T>
    {
        public T ResponseCode { get; set; }
        public List<T> Errors { get; set; }
        public T Message { get; set; }
        public T Data { get; set; }

       
    }
}
