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
        IEnumerable<ProjectDto> GetAll();
        ProjectDto GetById(int id);
        int Create(ProjectDto projectDto);
        bool Update(int id, ProjectDto projectDto);
        bool Delete(int id);
    }
}
