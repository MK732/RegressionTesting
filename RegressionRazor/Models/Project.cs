using System.ComponentModel.DataAnnotations;

namespace RegressionTest.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public List<TestCase> TestCases { get; set; } = new List<TestCase>();

        public int TotalTestCases => TestCases.Count;

        public int PassedTestCases => TestCases.Count(tc => tc.Status == TestStatus.Passed);

        public int FailedTestCases => TestCases.Count(tc => tc.Status == TestStatus.Failed);

        public int InProgressTestCases => TestCases.Count(tc => tc.Status == TestStatus.InProgress);

        public int NotStartedTestCases => TestCases.Count(tc => tc.Status == TestStatus.NotStarted);

        public double PassRate => TotalTestCases > 0 ? (double)PassedTestCases / TotalTestCases * 100 : 0;
    }
}