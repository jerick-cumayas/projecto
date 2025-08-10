using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projecto.Models
{
  public class Ticket
  {
    [Key]
    public int Id { get; set; }
    public required int ProjectId { get; set; }

    [ForeignKey("ProjectId")]
    public Project? Project { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required TaskStatus Status { get; set; } = TaskStatus.ToDo;
    public required Priority Priority { get; set; } = Priority.Medium;
    public required DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
  }

  public enum TaskStatus {
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