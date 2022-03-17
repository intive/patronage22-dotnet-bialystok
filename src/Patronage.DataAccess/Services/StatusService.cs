using Microsoft.EntityFrameworkCore;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;

namespace Patronage.DataAccess.Services
{
    public class StatusService : IStatusService
    {
        private readonly TableContext _dbContext;

        public StatusService(TableContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<StatusDto>> GetAll()
        {
            var statuses = await _dbContext.Statuses
                          .Select(status => new StatusDto
                          {
                              Id = status.Id,
                              Code = status.Code
                          })
                          .ToListAsync();
            return statuses;
        }

        public async Task<StatusDto?> GetById(int id)
        {
            var status = await _dbContext
                    .Statuses
                    .FirstOrDefaultAsync(b => b.Id == id);
            if (status is not null)
            {
                var statusDto = new StatusDto
                {
                    Id = status.Id,
                    Code = status.Code
                };
                return statusDto;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> Create(string statusCode)
        {
            var status = new Status
            {
                Code = statusCode
            };
            await _dbContext.Statuses.AddAsync(status);
            await _dbContext.SaveChangesAsync();
            return status.Id;
        }

        public async Task<bool> Delete(int statusId)
        {
            var status = await _dbContext.Statuses
                        .FirstOrDefaultAsync(b => b.Id == statusId);
            if (status is not null)
            {
                _dbContext.Statuses.Remove(status);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Update(int statusId, string statusCode)
        {
            var status = await _dbContext
                        .Statuses
                        .FirstOrDefaultAsync(s => s.Id == statusId);
            if (status is not null)
            {
                status.Code = statusCode;
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}