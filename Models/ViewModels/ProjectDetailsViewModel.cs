using Microsoft.AspNetCore.Mvc.Rendering;

namespace Projecto.Models.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public required Project Project { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; } = [];
        public IEnumerable<SelectListItem> StatusOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> PriorityOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public int OpenTicketsCount { get; set; }
    }
}
