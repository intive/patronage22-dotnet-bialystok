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
        void Create(ProjectDto projectDto);
        void Update(int id, ProjectDto projectDto);
        void Delete(int id);
    }
}
