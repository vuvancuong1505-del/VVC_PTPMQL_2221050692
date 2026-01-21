Tìm hiểu cấu trúc thư mục của dự án .Net MVC 

Cấu trúc thư mục dự án .NET MVC
ProjectName
│
├── Controllers/   // chứa Controller
├── Models/        // chứa Model (dữ liệu)
├── Views/         // chứa View (hiển thị)
│   └── ControllerName/
│       └── Index.cs
│
├── Program.cs     // điểm bắt đầu chương trình
└── ProjectName.csproj

Tìm hiểu về định tuyến (Route) trong .Net MVC

class Program
{
    static void Main()
    {
        DemoController demo = new DemoController();
        demo.Index();
    }
}

Tìm hiểu về namespace trong C#

namespace TenProject.Controllers
{
    class DemoController
    {
    }
}


Tìm hiểu về Controller, View trong .Net MVC

Controller trong .NET MVC
Controller là class điều khiển luồng chương trình
Nhận yêu cầu từ Program
Xử lý logic
Gọi Model
Trả dữ liệu cho View


