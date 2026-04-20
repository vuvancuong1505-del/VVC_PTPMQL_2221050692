using FirstWebMVC.Models.ViewModels;

namespace FirstWebMVC.Models.Student;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}