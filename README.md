# CẤU TRÚC THƯ MỤC CỦA DỰ ÁN .NET MVC
###
    PHÂN TÍCH:
        - cấu trúc theo thứ tự Models -> Controllers -> Views
        - Models sẽ chứa các lớp csdl (làm việc với Database)
        - Controller sẽ chứa các mã nguồn, logic chính của chương trình và kết nối với Models để lấy dữ liệu (làm việc với Models) và đẩy dữ liệu qua cho Views (làm việc với Views)
        - Views sẽ chứa các mã nguồn giao diện nhận dữ liệu từ Controllers gửi đến và hiển thị ra cho người dùng, nhận tương tác của người dùng chuyển đến cho Controllers (làm việc với Controllers) 
    KẾT LUẬN: 
        - Views chỉ hiển thị giao diện nhận dữ liệu từ Controllers
        - Models xử lý dữ liệu 
        - Models và Views không làm việc trực tiếp với nhau mà thông qua Controllers
###

# ROUTE trong .NET MVC
###
    - Route không phải Url
    - Route là cơ chế ánh xạ Url hoặc có thể nói Route chịu trách nhiệm điều hướng đến trang tương ứng với Url được nhập
    - cấu tạo của Route trong .NET MVC "{controller=Home}/{action=Index}/{id?}" trong đó:
        + Home là giá trị nhập vào tương ứng với tên của controller bỏ hậu tố controller
        + Index là tên method public
        + id là tham số
    - có 2 cách để cấu tạo Route trong .NET MVC
        + cách 1: dùng Route gắn trên controller/action (Convention-based Routing)
        ```
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
        + cách 2: dùng Convention-based Routing (mặc định trong Program.cs)
        ```
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        ```      
###