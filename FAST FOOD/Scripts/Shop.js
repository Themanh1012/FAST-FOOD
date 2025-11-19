
    $(document).ready(function () {
        updateCartCount();

    // Khi click vào icon giỏ hàng, chuyển tới trang Cart
    $('#cartIcon').click(function () {
        window.location.href = '@Url.Action("Index", "Carts", new { area = "Customer" })';
        });
    });

    function updateCartCount() {
        $.get('@Url.Action("GetCartCount", "Carts", new { area = "Customer" })', function (res) {
            $('#cartCount').text(res.count);
        });
    }


function updateCartCount(count) {
    const badge = document.getElementById("cartCount");
    if (badge) badge.textContent = count > 0 ? count : "";
}
document.addEventListener("DOMContentLoaded", function () {
    // Lấy dữ liệu từ TempData qua Razor
    var success = '@TempData["Success"]';
    var error = '@TempData["Error"]';
    var info = '@TempData["Info"]';

    // Nếu có thông báo, hiển thị toast
    if (success && success.trim() !== "") {
        Swal.fire({
            toast: true,
            icon: 'success',
            title: success,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true
        });
    } else if (error && error.trim() !== "") {
        Swal.fire({
            toast: true,
            icon: 'error',
            title: error,
            position: 'top-end',
            showConfirmButton: false,
            timer: 4000,
            timerProgressBar: true
        });
    } else if (info && info.trim() !== "") {
        Swal.fire({
            toast: true,
            icon: 'info',
            title: info,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true
        });
    }
});


var success = '@TempData["Success"]';
var error = '@TempData["Error"]';
var info = '@TempData["Info"]';

if (success && success.trim() !== "") {
    Swal.fire({
        toast: true,
        icon: 'success',
        title: success,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });
}
      
    // 🔥 AJAX thêm vào giỏ hàng
    function addToCart(monId) {

        $.ajax({
            url: '/Customer/Carts/AddToCartAjax',
            type: 'POST',
            data: { id: monId },

            success: function (res) {

                if (res.status === "notlogin") {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Bạn chưa đăng nhập',
                        text: 'Đăng nhập để thêm món vào giỏ!',
                        confirmButtonText: 'Đăng nhập ngay'
                    }).then(() => {
                        window.location.href = '/Account/DangNhap';
                    });
                }
                else if (res.status === "success") {
                    Swal.fire({
                        toast: true,
                        icon: 'success',
                        title: 'Đã thêm vào giỏ!',
                        position: 'top-end',
                        timer: 1500,
                        showConfirmButton: false
                    });

                    // cập nhật số lượng giỏ hàng
                    document.getElementById("cartCount").innerText = res.count;
                }
            }
        });
    }



//left _right menu 2


    const recList = document.getElementById("recList");

    function slideLeft() {
        recList.scrollBy({ left: -300, behavior: "smooth" });
    }

    function slideRight() {
        recList.scrollBy({ left: 300, behavior: "smooth" });
    }

