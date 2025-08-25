using Microsoft.AspNetCore.Mvc;
using Projecto.Data.Service;
using Projecto.Models;
using Projecto.Models.ViewModels;

namespace Projecto.Controllers
{
  public class SprintsController : Controller
  {
    private readonly ISprintService _sprintService;

    public SprintsController(ISprintService sprintService)
    {
      _sprintService = sprintService;
    }

    public async Task<IActionResult> Index()
    {
      var sprints = await _sprintService.GetAll();
      return View(sprints);
    }

    public async Task<IActionResult> ListByProject(int projectId)
    {
      var sprints = await _sprintService.GetAllByProjectId(projectId);
      return PartialView("_SprintList", sprints);
    }
    public async Task<IActionResult> Details(int id)
    {
      var sprint = await _sprintService.GetById(id);
      if (sprint == null) return NotFound();
      var vm = new SprintDetails
      {
        Sprint = sprint,
        Tickets = sprint.Tickets
      };
      return View(vm);
    }
    [HttpGet]
    public IActionResult Create(int projectId)
    {
      var vm = new SprintForm
      {
        ProjectId = projectId,
        SprintStatusOptions = EnumHelper.GetEnumSelectList<SprintStatus>()
      };
      return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Create(SprintForm form)
    {
      if (!ModelState.IsValid)
      {
        return View(form);
      }
      var sprint = new Sprint
      {
        ProjectId = form.ProjectId,
        Title = form.Title,
        Description = form.Description,
        DueDate = form.DueDate
      };
      await _sprintService.Add(sprint);
      return RedirectToAction("Details", "Projects", new { id = form.ProjectId });
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      var sprint = await _sprintService.GetById(id);
      return View(sprint);
    }

    [HttpPost]
    public async Task<IActionResult> EditConfirmed(int id, Sprint sprint)
    {
      if (id != sprint.Id) return BadRequest();

      if (!ModelState.IsValid)
      {
        return RedirectToAction("Edit", "Sprints", new { id = sprint.Id });
      }

      await _sprintService.Update(sprint);
      return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      var sprint = await _sprintService.GetById(id);
      if (sprint == null) return NotFound();

      int projectId = sprint.ProjectId;

      await _sprintService.Delete(sprint);
      return RedirectToAction("Details", "Projects", new { id = projectId });
    }
  }
}