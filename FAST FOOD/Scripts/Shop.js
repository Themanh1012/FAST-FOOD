
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