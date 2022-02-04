using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Common
{
    public interface IModifable
    {        
        public DateTime? ModifiedOn { get; set; }
        void OnModify();
    }
}
