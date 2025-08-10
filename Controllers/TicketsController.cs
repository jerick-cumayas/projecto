using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecto.Data.Service;
using Projecto.Models;
using Projecto.Models.ViewModels;

namespace Projecto.Controllers
{
  public class TicketsController : Controller
  {
    private readonly ITicketService _ticketService;
    private readonly IProjectService _projectService;

    public TicketsController(ITicketService ticketService, IProjectService projectService)
    {
      _ticketService = ticketService;
      _projectService = projectService;
    }

    public async Task<IActionResult> Index()
    {
      var tickets = await _ticketService.GetAll();
      return View(tickets);
    }

    public async Task<IActionResult> Details(int id)
    {
      var ticket = await _ticketService.GetById(id);
      return View(ticket);
    }

    public async Task<IActionResult> Create()
    {
      var projects = await _projectService.GetAll();
      var vm = new TicketCreateViewModel
      {
        Projects = projects.Select(p => new SelectListItem
        {
          Value = p.Id.ToString(),
          Text = p.Name
        }),
        StatusOptions = Enum.GetValues(typeof(Models.TaskStatus))
                            .Cast<Models.TaskStatus>()
                            .Select(s => new SelectListItem
                            {
                                Value = ((int)s).ToString(),
                                Text = s.ToString()
                            }),

        PriorityOptions = Enum.GetValues(typeof(Priority))
                              .Cast<Priority>()
                              .Select(p => new SelectListItem
                              {
                                  Value = ((int)p).ToString(),
                                  Text = p.ToString()
                              })
      };
      return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Ticket ticket)
    {
      if (ModelState.IsValid)
      {
        await _ticketService.Add(ticket);
        return RedirectToAction("Index");
      }
      return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
      var ticket = await _ticketService.GetById(id);
      if (ticket == null) return NotFound();
      return View(ticket);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Ticket ticket)
    {
      if (id != ticket.Id) return BadRequest();

      if (ModelState.IsValid)
      {
        await _ticketService.Update(ticket);
        return RedirectToAction("Index");
      }
      return View(ticket);
    }

    public async Task<IActionResult> Delete(int id)
    {
      var ticket = await _ticketService.GetById(id);
      if (ticket == null) return NotFound();
      return View(ticket);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var ticket = await _ticketService.GetById(id);
      if (ticket == null) return NotFound();
      await _ticketService.Delete(ticket);
      return RedirectToAction("Index");
    }
  }
}