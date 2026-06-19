# TÀI LIỆU DỰ ÁN .NET MVC

## 1. Cấu trúc thư mục của dự án .NET MVC

Dự án `FirstWebMVC` có cấu trúc thư mục chuẩn của ASP.NET Core MVC:

- `Controllers/`
  - Chứa các lớp controller xử lý yêu cầu HTTP, điều phối luồng chương trình và trả về `View` hoặc dữ liệu.
- `Models/`
  - Chứa các lớp dữ liệu, các model dùng để định nghĩa cấu trúc dữ liệu và tương tác với cơ sở dữ liệu.
- `Views/`
  - Chứa các file giao diện Razor (`.cshtml`) dùng để hiển thị HTML cho người dùng.
  - Mỗi controller có thể có một thư mục con tương ứng, ví dụ `Views/Demo/Index.cshtml`.
- `wwwroot/`
  - Chứa tài nguyên tĩnh như CSS, JavaScript, hình ảnh.
- `Program.cs`
  - Cấu hình ứng dụng, đăng ký dịch vụ và thiết lập route mặc định.
- `appsettings.json`, `appsettings.Development.json`
  - Chứa cấu hình ứng dụng và chuỗi kết nối.

### Vai trò của MVC

- `Model` xử lý dữ liệu và logic nghiệp vụ.
- `View` hiển thị giao diện người dùng.
- `Controller` nhận yêu cầu từ trình duyệt, gọi model và trả về view.

> Lưu ý: `Model` và `View` không giao tiếp trực tiếp với nhau; mọi liên kết đều qua `Controller`.

## 2. Định tuyến (Route) trong .NET MVC

Định tuyến là cơ chế ánh xạ URL đến controller và action tương ứng.

### Route mặc định

Trong `Program.cs`, ứng dụng sử dụng route mặc định:

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
```

Ý nghĩa:

- `controller=Home`: controller mặc định là `HomeController` nếu không truyền controller trong URL.
- `action=Index`: action mặc định là `Index` nếu không truyền action.
- `id?`: tham số tùy chọn.

Ví dụ URL:

- `/` => `HomeController.Index()`
- `/Demo` => `DemoController.Index()`
- `/Demo/Index` => `DemoController.Index()`
- `/Demo/Index/123` => `DemoController.Index(int id)` nếu action nhận tham số.

### Route theo attribute

Có thể gắn route trực tiếp trên controller hoặc action bằng attribute:

```csharp
[Route("orders")]
public class OrderController : Controller
{
    [HttpGet("")]
    public IActionResult Index() { }

    [HttpGet("create")]
    public IActionResult Create() { }

    [HttpGet("edit/{id}")]
    public IActionResult Edit(int id) { }
}
```

## 3. Namespace trong C#

`namespace` là cách tổ chức mã nguồn thành các nhóm logic và tránh xung đột tên.

- `namespace` định nghĩa một phạm vi chứa các lớp, interface và enum.
- Giúp tách biệt các thành phần cùng tên trong các phần khác nhau của ứng dụng.
- Cú pháp:

```csharp
namespace FirstWebMVC.Controllers
{
    public class DemoController : Controller
    {
        // ...
    }
}
```

Trong dự án này, `namespace FirstWebMVC.Controllers` cho biết controller này thuộc nhóm controller của ứng dụng `FirstWebMVC`.

## 4. Controller và View trong .NET MVC

### Controller

- Controller là lớp chịu trách nhiệm xử lý yêu cầu HTTP.
- Mỗi phương thức public trong controller được gọi là một action.
- Action trả về `IActionResult`, ví dụ `View()`, `RedirectToAction()`, `Json()`, `Content()`.
- Controller lấy dữ liệu từ model và kiểm soát luồng thực thi.

### View

- View là file Razor (`.cshtml`) chứa markup HTML và mã C# nhẹ.
- View nhận dữ liệu từ controller và render ra HTML trả về cho trình duyệt.
- View thường nằm trong thư mục có tên trùng với controller.

### Ví dụ

- `DemoController` có action `Index()` trả về view `Views/Demo/Index.cshtml`.
- View hiển thị thông báo `Hello + Họ tên và mã sinh viên`.

## 5. Demo thực hành

- Controller: `FirstWebMVC/Controllers/DemoController.cs`
- View: `FirstWebMVC/Views/Demo/Index.cshtml`
- URL truy cập: `/Demo/Index`

Nội dung trả về: `Hello Nguyễn Văn A - 2221050692`.
