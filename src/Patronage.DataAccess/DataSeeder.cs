using Microsoft.EntityFrameworkCore;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.DataAccess
{
    public class DataSeeder
    {
        private readonly TableContext _dbContext;

        public DataSeeder(TableContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {

        }
    }
}
