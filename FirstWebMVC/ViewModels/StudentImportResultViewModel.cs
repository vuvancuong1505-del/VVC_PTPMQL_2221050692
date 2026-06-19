namespace FirstWebMVC.ViewModels
{
    public class StudentImportResultViewModel
    {
        public int TotalRows { get; set; }
        public int CreatedCount { get; set; }
        public int SkippedCount { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
