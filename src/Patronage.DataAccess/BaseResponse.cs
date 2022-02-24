using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.DataAccess
{
    public class BaseResponse<T>
    {
        public int ResponseCode { get; set; }
        public List<BaseResponseError>? BaseResponseError { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

       
    }
}
