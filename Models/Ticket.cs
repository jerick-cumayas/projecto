using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projecto.Models
{
  public class Ticket
  {
    [Key]
    public int Id { get; set; }
    public required int SprintId { get; set; }

    [ForeignKey("SprintId")]
    public Sprint? Sprint { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required TicketStatus Status { get; set; } = TicketStatus.ToDo;
    public required Priority Priority { get; set; } = Priority.Medium;
    public required DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
  }

  public enum TicketStatus {
    ToDo,
    InProgress,
    Done
  }

  public enum Priority {
    Low,
    Medium, 
    High
  }
}