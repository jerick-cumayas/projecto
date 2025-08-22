using Microsoft.EntityFrameworkCore;
using Projecto.Models;

namespace Projecto.Data.Service
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectoAppContext _context;

        public ProjectService(ProjectoAppContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Project>> GetAll()
        {
            var projects = await _context.Projects.ToListAsync();
            return projects;
        }
        public async Task<Project?> GetById(int id)
        {
            // return await _context.Projects.Include(project => project.Sprints.Where(sprint => sprint.Status != SprintStatus.Archived)).FirstOrDefaultAsync(p => p.Id == id);
            return await _context.Projects.Include(project => project.Sprints).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task Add(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
