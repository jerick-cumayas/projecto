using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Projecto.Models.ViewModels
{
    public class TicketUpdateFormModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime? CreatedAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
        // for dropdowns
        public IEnumerable<SelectListItem> StatusOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> PriorityOptions { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}