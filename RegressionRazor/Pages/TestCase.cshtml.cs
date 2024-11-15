using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegressionTest.Models;
using RegressionTest.Services;

namespace RegressionTest.Pages
{
    public class TestCaseModel : PageModel
    {
        private readonly ProjectService _projectService;

        public Project? CurrentProject { get; set; }
        public TestCase? CurrentTestCase { get; set; }

        [BindProperty]
        public TestStep NewTestStep { get; set; } = new TestStep();

        public TestCaseModel(ProjectService projectService)
        {
            _projectService = projectService;
        }

        public IActionResult OnGet(int projectId, int testCaseId)
        {
            CurrentProject = _projectService.GetProjectById(projectId);
            if (CurrentProject == null)
            {
                return NotFound();
            }

            CurrentTestCase = CurrentProject.TestCases.FirstOrDefault(tc => tc.Id == testCaseId);
            if (CurrentTestCase == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostAddStep(int projectId, int testCaseId)
        {
            if (!ModelState.IsValid)
            {
                return OnGet(projectId, testCaseId);
            }

            _projectService.AddTestStep(projectId, testCaseId, NewTestStep);
            return RedirectToPage(new { projectId, testCaseId });
        }

        public IActionResult OnPostUpdateStepStatus(int projectId, int testCaseId, int stepId, StepStatus status)
        {
            _projectService.UpdateStepStatus(projectId, testCaseId, stepId, status);
            return RedirectToPage(new { projectId, testCaseId });
        }

        public IActionResult OnPostResetTestCase(int projectId, int testCaseId)
        {
            _projectService.ResetTestCase(projectId, testCaseId);
            return RedirectToPage(new { projectId, testCaseId });
        }
    }
}