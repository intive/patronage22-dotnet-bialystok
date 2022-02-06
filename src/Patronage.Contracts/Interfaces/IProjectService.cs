using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetAll(string searchedProject);
        ProjectDto GetById(int id);
        int Create(CreateOrUpdateProjectDto projectDto);
        bool Update(int id, CreateOrUpdateProjectDto projectDto);
        bool Delete(int id);
    }
}
