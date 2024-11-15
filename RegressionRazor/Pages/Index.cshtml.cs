using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegressionTest.Models;
using RegressionTest.Services;

namespace RegressionTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProjectService _projectService;

        public List<Project> Projects { get; set; } = new List<Project>();

        [BindProperty]
        public string NewProjectName { get; set; } = string.Empty;

        public IndexModel(ProjectService projectService)
        {
            _projectService = projectService;
        }

        public void OnGet()
        {
            Projects = _projectService.GetAllProjects();
        }

        public IActionResult OnPostAddProject()
        {
            if (!ModelState.IsValid)
            {
                Projects = _projectService.GetAllProjects();
                return Page();
            }

            bool added = _projectService.AddProject(NewProjectName);
            if (!added)
            {
                ModelState.AddModelError("NewProjectName", "A project with this name already exists.");
                Projects = _projectService.GetAllProjects();
                return Page();
            }

            return RedirectToPage();
        }
    }
}