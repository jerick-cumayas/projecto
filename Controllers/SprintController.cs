using Projecto.Data.Service;
using Projecto.Models;

namespace Projecto.Controllers
{
    public class SprintController
    {
        private readonly ISprintService _sprintService;

        public SprintController(ISprintService sprintService)
        {
            _sprintService = sprintService;
        }
    }
}