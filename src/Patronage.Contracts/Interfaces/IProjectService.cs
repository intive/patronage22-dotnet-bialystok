using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Contracts.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetAll(string searchedProject);
        ProjectDto GetById(int id);
        int Create(CreateOrUpdateProjectDto projectDto);
        bool Update(int id, CreateOrUpdateProjectDto projectDto);
        bool LightUpdate(int id, PartialProjectDto projectDto);
        bool Delete(int id);
    }
}
