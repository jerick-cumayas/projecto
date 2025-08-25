using Projecto.Models;

namespace Projecto.Data.Service
{
  public interface ISprintService
  {
    Task<IEnumerable<Sprint>> GetAll();
    Task<IEnumerable<Sprint>> GetAllByProjectId(int projectId);
    Task<Sprint?> GetById(int id);
    Task Add(Sprint sprint);
    Task Update(Sprint sprint);
    Task Delete(Sprint sprint);
  }
}