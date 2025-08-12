using Microsoft.AspNetCore.Mvc.Rendering;

namespace Projecto.Models.ViewModels
{
    public class TicketUpdateFormModel
    {
        public required Ticket Ticket;
        public required IEnumerable<SelectListItem> StatusOptions;
        public required IEnumerable<SelectListItem> PriorityOptions;
    }
}