using System.ComponentModel.DataAnnotations;

namespace RegressionTest.Models
{
    public class TestCase
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public List<TestStep> Steps { get; set; } = new List<TestStep>();

        public TestStatus Status { get; set; }
    }

    public class TestStep
    {
        public int Id { get; set; }

        public int StepNumber { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Outcome { get; set; } = string.Empty;

        public StepStatus Status { get; set; } = StepStatus.NotStarted;
    }

    public enum TestStatus
    {
        NotStarted,
        InProgress,
        Passed,
        Failed
    }

    public enum StepStatus
    {
        NotStarted,
        Passed,
        Failed
    }
}