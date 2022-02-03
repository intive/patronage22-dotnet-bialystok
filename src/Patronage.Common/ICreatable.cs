using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Common
{
    public interface ICreatable
    {
        public DateTime CreatedOn { get; set; }
        void OnCreate();
    }
}
