using Projecto.Models;

namespace Projecto.Data.Service
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAll();
        Task<Project?> GetById(int id);
        Task Add(Project project);
        Task Update(Project project);
        Task Delete(Project project);
    }
}
