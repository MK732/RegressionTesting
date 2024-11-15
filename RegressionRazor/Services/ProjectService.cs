using RegressionTest.Models;

namespace RegressionTest.Services
{
    public class ProjectService
    {
        private List<Project> _projects = new List<Project>();
        private int _nextProjectId = 1;
        private int _nextTestCaseId = 1;
        private int _nextTestStepId = 1;

        public ProjectService()
        {
            InitializeSampleData();
        }

        public List<Project> GetAllProjects() => _projects;

        public Project? GetProjectById(int id) => _projects.FirstOrDefault(p => p.Id == id);

        public void AddTestCase(int projectId, TestCase newTestCase)
        {
            var project = GetProjectById(projectId);
            if (project != null)
            {
                newTestCase.Id = _nextTestCaseId++;
                newTestCase.Status = TestStatus.NotStarted;
                project.TestCases.Add(newTestCase);
            }
        }

        public void AddTestStep(int projectId, int testCaseId, TestStep newTestStep)
        {
            var project = GetProjectById(projectId);
            var testCase = project?.TestCases.FirstOrDefault(tc => tc.Id == testCaseId);
            if (testCase != null)
            {
                newTestStep.Id = _nextTestStepId++;
                newTestStep.StepNumber = testCase.Steps.Count + 1;
                testCase.Steps.Add(newTestStep);
                UpdateTestCaseStatus(projectId, testCaseId);
            }
        }

        public void UpdateStepStatus(int projectId, int testCaseId, int stepId, StepStatus status)
        {
            var project = GetProjectById(projectId);
            var testCase = project?.TestCases.FirstOrDefault(tc => tc.Id == testCaseId);
            var step = testCase?.Steps.FirstOrDefault(s => s.Id == stepId);

            if (step != null)
            {
                step.Status = status;
                UpdateTestCaseStatus(projectId, testCaseId);
            }
        }

        public void UpdateTestCaseStatus(int projectId, int testCaseId)
        {
            var project = GetProjectById(projectId);
            var testCase = project?.TestCases.FirstOrDefault(tc => tc.Id == testCaseId);

            if (testCase != null && testCase.Steps.Any())
            {
                if (testCase.Steps.All(s => s.Status == StepStatus.Passed))
                {
                    testCase.Status = TestStatus.Passed;
                }
                else if (testCase.Steps.Any(s => s.Status == StepStatus.Failed))
                {
                    testCase.Status = TestStatus.Failed;
                }
                else if (testCase.Steps.Any(s => s.Status != StepStatus.NotStarted))
                {
                    testCase.Status = TestStatus.InProgress;
                }
                else
                {
                    testCase.Status = TestStatus.NotStarted;
                }
            }
            else
            {
                testCase.Status = TestStatus.NotStarted;
            }
        }

        public void ResetTestCase(int projectId, int testCaseId)
        {
            var project = GetProjectById(projectId);
            var testCase = project?.TestCases.FirstOrDefault(tc => tc.Id == testCaseId);

            if (testCase != null)
            {
                foreach (var step in testCase.Steps)
                {
                    step.Status = StepStatus.NotStarted;
                }
                testCase.Status = TestStatus.NotStarted;
            }
        }

        public bool AddProject(string projectName)
        {
            if (_projects.Any(p => p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            var newProject = new Project
            {
                Id = _nextProjectId++,
                Name = projectName,
                TestCases = new List<TestCase>()
            };

            _projects.Add(newProject);
            return true;
        }

        private void InitializeSampleData()
        {
        }
    }
}