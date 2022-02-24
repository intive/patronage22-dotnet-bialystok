using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.DataAccess.Services
{
    public class StatusService : IStatusService
    {
        private readonly TableContext _dbContext;

        public StatusService(TableContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<StatusDto> GetAll()
        {
            //var statuses = from s in _dbContext.Statuses
            //               select new StatusDto()
            //               {
            //                   Id = s.Id,
            //                   Code = s.Code
            //               };



        }
        public StatusDto GetById(int id)
        {
            var status = _dbContext
                    .Statuses
                    .Select(b => new StatusDto()
                    {
                        Id = b.Id,
                        Code = b.Code,
                    }).SingleOrDefault(b =>b.Id == id);
            return status;
        }
        public bool Create(string statusCode)
        {
            try
            {
                var status = new Status();
                status.Code = statusCode;
            
                _dbContext.Statuses.Add(status);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex) when (ex is Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return false;
            }
        }
        public bool Delete(int statusId)
        {
            var status = _dbContext.Statuses
                        .FirstOrDefault(b => b.Id == statusId);
            _dbContext.Statuses.Remove(status);
            _dbContext.SaveChanges();
            return true;
        }
        public bool Update(int statusId, string statusCode)
        {
            var status = _dbContext
                        .Statuses
                        .FirstOrDefault(s => s.Id == statusId);
            if (status is null)
            {
                return false;
            }
            else
            {
                status.Code = statusCode;
                _dbContext.SaveChanges();
                return true;
            }
        }
    }
}
