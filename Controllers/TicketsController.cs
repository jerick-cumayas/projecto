using System.Runtime.InteropServices;
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

    public IActionResult Create(int projectId)
    {
      var model = new TicketFormModel
      {
        ProjectId = projectId,
        DueDate = DateTime.Today.AddDays(7),
        StatusOptions = EnumHelper.GetEnumSelectList<Models.TaskStatus>(),
        PriorityOptions = EnumHelper.GetEnumSelectList<Priority>(),
      };
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TicketFormModel form)
    {
      if (!ModelState.IsValid)
      {
        var vm = new TicketFormModel
        {
          ProjectId = form.ProjectId,
          DueDate = DateTime.Today.AddDays(7),
          StatusOptions = EnumHelper.GetEnumSelectList<Models.TaskStatus>(),
          PriorityOptions = EnumHelper.GetEnumSelectList<Priority>(),
        };
        return View("Create", vm);
      }

      var ticket = new Ticket
      {
        ProjectId = form.ProjectId,
        Title = form.Title,
        Description = form.Description,
        Status = form.Status,
        Priority = form.Priority,
        DueDate = form.DueDate,
      };

      await _ticketService.Add(ticket);
      return RedirectToAction("Details", "Projects", new { id = form.ProjectId });
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