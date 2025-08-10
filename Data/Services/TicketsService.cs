using Microsoft.EntityFrameworkCore;
using Projecto.Models;

namespace Projecto.Data.Service
{
  public class TicketService : ITicketService
  {
    private readonly ProjectoAppContext _context;
    public TicketService(ProjectoAppContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Ticket>> GetAll()
    {
      var tickets = await _context.Tickets.ToListAsync();
      return tickets;
    }

    public async Task<Ticket?> GetById(int id)
    {
      var ticket = await _context.Tickets.FirstOrDefaultAsync(p => p.Id == id);
      return ticket;
    }

    public async Task Add(Ticket ticket)
    {
      _context.Tickets.Add(ticket);
      await _context.SaveChangesAsync();
    }

    public async Task Update(Ticket ticket)
    {
      _context.Tickets.Update(ticket);
      await _context.SaveChangesAsync();
    }

    public async Task Delete(Ticket ticket)
    {
      _context.Tickets.Remove(ticket);
      await _context.SaveChangesAsync();
    }
  }
}