using Microsoft.AspNetCore.Mvc.Rendering;

namespace Projecto.Models.ViewModels
{
  public class TicketIndex
  {
    public required IEnumerable<Ticket> Tickets;
    public required IEnumerable<SelectListItem> StatusOptions;
    public required IEnumerable<SelectListItem> PriorityOptions;
  }
}