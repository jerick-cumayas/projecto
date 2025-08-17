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

    [HttpGet]
    public async Task<IActionResult> Create(int? projectId)
    {
      var projectOptions = await _projectService.GetAll();

      var model = new TicketFormModel
      {
        ProjectId = projectId ?? 0,
        DueDate = DateTime.Today.AddDays(7),
        ProjectOptions = projectId.HasValue
              ? null
              : projectOptions.Select(p => new SelectListItem
              {
                Value = p.Id.ToString(),
                Text = p.Name
              }),
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
        var vm = await BuildTicketFormModel(form.ProjectId);
        vm.Title = form.Title;
        vm.Description = form.Description;
        vm.Status = form.Status;
        vm.Priority = form.Priority;
        vm.DueDate = form.DueDate;
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

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      var ticket = await _ticketService.GetById(id);
      if (ticket == null) return NotFound();

      var vm = new TicketFormModel
      {
        Id = ticket.Id,
        ProjectId = ticket.ProjectId,
        Title = ticket.Title,
        Description = ticket.Description,
        Status = ticket.Status,
        Priority = ticket.Priority,
        CreatedAt = ticket.CreatedAt,
        DueDate = ticket.DueDate,
        StatusOptions = EnumHelper.GetEnumSelectList<Models.TaskStatus>(),
        PriorityOptions = EnumHelper.GetEnumSelectList<Priority>()
      };
      return View(vm);
    }

    // POST: Tickets/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TicketFormModel form)
    {
      var ticket = await _ticketService.GetById(form.Id!.Value);
      if (ticket == null) return NotFound();

      ticket.Title = form.Title;
      ticket.Description = form.Description;
      ticket.Status = form.Status;
      ticket.Priority = form.Priority;
      ticket.DueDate = form.DueDate;

      await _ticketService.Update(ticket);
      return RedirectToAction("Details", "Projects", new { id = form.ProjectId });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int id, Models.TaskStatus status)
    {
      var ticket = await _ticketService.GetById(id);
      if (ticket == null) return NotFound();

      ticket.Status = status;
      await _ticketService.Update(ticket);

      return RedirectToAction("Details", "Projects", new { id = ticket.ProjectId });
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePriority(int id, Priority priority)
    {
      var ticket = await _ticketService.GetById(id);
      if (ticket == null) return NotFound();

      ticket.Priority = priority;
      await _ticketService.Update(ticket);

      return RedirectToAction("Details", "Projects", new { id = ticket.ProjectId });
    }

    [HttpGet]
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
      int projectId = ticket.ProjectId;
      await _ticketService.Delete(ticket);
      return RedirectToAction("Details", "Projects", new { id = projectId});
    }

    private async Task<TicketFormModel> BuildTicketFormModel(int? projectId = null)
    {
      var model = new TicketFormModel
      {
        ProjectId = projectId ?? 0,
        DueDate = DateTime.Today.AddDays(7),
        StatusOptions = EnumHelper.GetEnumSelectList<Models.TaskStatus>(),
        PriorityOptions = EnumHelper.GetEnumSelectList<Priority>()
      };

      if (!projectId.HasValue)
      {
        var projectOptions = await _projectService.GetAll();
        model.ProjectOptions = projectOptions.Select(p => new SelectListItem
        {
          Value = p.Id.ToString(),
          Text = p.Name
        });
      }

      return model;
    }
  }
}