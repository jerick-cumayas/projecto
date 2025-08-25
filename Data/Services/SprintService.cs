using Microsoft.EntityFrameworkCore;
using Projecto.Models;

namespace Projecto.Data.Service
{
  public class SprintService : ISprintService
  {
    private ProjectoAppContext _context;

    public SprintService(ProjectoAppContext context)
    {
      _context = context;
    }
    public async Task<IEnumerable<Sprint>> GetAll() => await _context.Sprints.Include(s => s.Project).ToListAsync();
    public async Task<IEnumerable<Sprint>> GetAllByProjectId(int projectId) => await _context.Sprints.Include(s => s.Project).Where(s => s.ProjectId == projectId).ToListAsync();
    public async Task<Sprint?> GetById(int id)
    {
      return await _context.Sprints
       .Include(s => s.Project).Include(s => s.Tickets)
       .FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task Add(Sprint sprint)
    {
      _context.Sprints.Add(sprint);
      await _context.SaveChangesAsync();
    }
    public async Task Update(Sprint sprint)
    {
      _context.Sprints.Update(sprint);
      await _context.SaveChangesAsync();
    }
    public async Task Delete(Sprint sprint)
    {
      _context.Sprints.Remove(sprint);
      await _context.SaveChangesAsync();
    }
  }
}