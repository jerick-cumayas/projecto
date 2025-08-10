using Microsoft.EntityFrameworkCore;
using Projecto.Models;

namespace Projecto.Data
{
    public class ProjectoAppContext : DbContext
    {
        public ProjectoAppContext(DbContextOptions<ProjectoAppContext> options) : base(options) { }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
