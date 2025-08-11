using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Projecto.Models.ViewModels
{
  public class TicketFormModel
  {
    [Required]
    public int ProjectId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = "";

    [Required(ErrorMessage = "Status is required")]
    public TaskStatus Status { get; set; }

    [Required(ErrorMessage = "Priority is required")]
    public Priority Priority { get; set; }

    [Required(ErrorMessage = "Due date is required")]
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }

    public IEnumerable<SelectListItem>? StatusOptions { get; set; }
    public IEnumerable<SelectListItem>? PriorityOptions { get; set; }
  }
}