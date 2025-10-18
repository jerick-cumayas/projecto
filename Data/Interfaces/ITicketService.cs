using Projecto.Models;

namespace Projecto.Data.Service
{
  public interface ITicketService
  {
    Task<IEnumerable<Ticket>> GetAll();
    Task<Ticket?> GetById(int id);
    Task<IEnumerable<Ticket>> GetAllByProjectId(int projectId);
    Task<IEnumerable<Ticket>> GetAllBySprintId(int sprintId);
    Task Add(Ticket ticket);
    Task Update(Ticket ticket);
    Task Delete(Ticket ticket);
  }
}