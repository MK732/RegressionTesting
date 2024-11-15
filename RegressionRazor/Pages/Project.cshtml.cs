using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegressionTest.Models;
using RegressionTest.Services;

namespace RegressionTest.Pages
{
    public class ProjectModel : PageModel
    {
        private readonly ProjectService _projectService;

        public Project? CurrentProject { get; set; }

        [BindProperty]
        public TestCase NewTestCase { get; set; } = new TestCase();

        public ProjectModel(ProjectService projectService)
        {
            _projectService = projectService;
        }

        public IActionResult OnGet(int id)
        {
            CurrentProject = _projectService.GetProjectById(id);
            if (CurrentProject == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostAddTestCase(int projectId)
        {
            if (!ModelState.IsValid)
            {
                return OnGet(projectId);
            }

            _projectService.AddTestCase(projectId, NewTestCase);
            return RedirectToPage(new { id = projectId });
        }

        public IActionResult OnPostResetTestCase(int projectId, int testCaseId) // Updated method signature
        {
            _projectService.ResetTestCase(projectId, testCaseId); // Updated method call
            return RedirectToPage(new { id = projectId });
        }
    }
}