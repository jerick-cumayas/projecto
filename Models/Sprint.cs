using System.ComponentModel.DataAnnotations.Schema;

namespace Projecto.Models
{
    public class Sprint
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public SprintStatus Status { get; set; } = SprintStatus.Planned;
        public ICollection<Ticket> Tickets { get; set; } = [];
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public enum SprintStatus
    {
        Planned,
        InProgress,
        Completed,
    }
}