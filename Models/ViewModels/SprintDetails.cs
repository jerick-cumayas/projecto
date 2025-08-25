namespace Projecto.Models.ViewModels
{
  public class SprintDetails
  {
    public required Sprint Sprint;
    public required IEnumerable<Ticket> Tickets;
  }
}