using Microsoft.AspNetCore.Mvc.Rendering;

namespace Projecto.Models.ViewModels
{
  public class TicketCreateViewModel
  {
    public int ProjectId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public TaskStatus Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime DueDate { get; set; }

    public required IEnumerable<SelectListItem> Projects { get; set; }
    public required IEnumerable<SelectListItem> StatusOptions { get; set; }
    public required IEnumerable<SelectListItem> PriorityOptions { get; set; }
  }
}