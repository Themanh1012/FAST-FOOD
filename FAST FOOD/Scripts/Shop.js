// =============================
// 🔥 CART BADGE FIX
// =============================

// Luôn giữ badge hiển thị đúng số lượng
function updateCartCount(count) {
    const badge = document.getElementById("cartCount");
    if (badge) badge.textContent = count;
}


// =============================
// 🔥 HIỂN THỊ THÔNG BÁO (TempData)
// =============================
document.addEventListener("DOMContentLoaded", function () {

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


// =============================
// 🔥 AJAX thêm giỏ hàng
// =============================
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

                // cập nhật số lượng hiển thị
                updateCartCount(res.count);
            }
        }
    });
}
