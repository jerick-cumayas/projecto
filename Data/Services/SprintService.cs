using Microsoft.EntityFrameworkCore;
using Projecto.Models;

namespace Projecto.Data.Service
{
    public class SprintService : ISprintService
    {
        private readonly ProjectoAppContext _context;

        public SprintService(ProjectoAppContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Sprint>> GetAll()
        {
            return await _context.Sprints.ToListAsync();
        }

        public async Task<Sprint?> GetById(int id)
        {
            return await _context.Sprints.FirstOrDefaultAsync(sprint => sprint.Id == id);
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