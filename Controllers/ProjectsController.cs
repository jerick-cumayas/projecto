using Microsoft.AspNetCore.Mvc;
using Projecto.Data;
using Projecto.Data.Service;
using Projecto.Models;

namespace Projecto.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAll();
            return View(projects);
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _projectService.GetById(id);
            return View(project);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.Add(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetById(id);
            if (project == null) return NotFound();
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(int id, Project project)
        {
            if (id != project.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                await _projectService.Update(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectService.GetById(id);
            if (project == null) return NotFound();
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _projectService.GetById(id);
            if (project == null) return NotFound();
            await _projectService.Delete(project);
            return RedirectToAction("Index");
        }
    }
}
