using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Projecto.Models.ViewModels
{
  public class SprintForm
  {
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public string Title { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
    [Required]
    public SprintStatus Status { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    public IEnumerable<SelectListItem> SprintStatusOptions { get; set; } = [];
   }
}