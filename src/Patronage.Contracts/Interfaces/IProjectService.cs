using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Contracts.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAll(string searchedPhrase);
        Task<ProjectDto> GetById(int id);
        Task<int> Create(CreateProjectDto projectDto);
        Task<bool> Update(int id, UpdateProjectDto projectDto);
        Task<bool> LightUpdate(int id, PartialProjectDto projectDto);
        Task<bool> Delete(int id);
    }
}
