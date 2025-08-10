namespace Projecto.Models.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public required Project Project { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; } = [];
        public int OpenTicketsCount { get; set; }
    }
}
