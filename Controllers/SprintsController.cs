using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public async Task<IActionResult> Details(int sprintId)
    {
      var sprint = await _sprintService.GetById(sprintId);
      return View(sprint);
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
      Console.WriteLine($"PROJECT ID: {form.ProjectId}");
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
  }
}