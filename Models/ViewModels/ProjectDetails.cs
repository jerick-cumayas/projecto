namespace Projecto.Models.ViewModels
{
    public class ProjectDetails
    {
        public required Project Project { get; set; }
        public required IEnumerable<Ticket> Tickets { get; set; }
    }
}
