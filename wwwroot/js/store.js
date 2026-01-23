$(document).ready(function () {
    $(".favorite-btn").click(function (e) {
        e.preventDefault();

        var button = $(this);
        var productId = button.data("product-id");

        $.ajax({
            url: '/UserPanel/Favorites/ToggleFavorite',
            type: 'POST',
            data: { productId: productId },
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                if (response.added) {
                    // اگه اضافه شد، قلب قرمز بشه
                    button.find("i").removeClass("text-secondary").addClass("text-danger");
                } else {
                    // اگه حذف شد، قلب خاکستری بشه
                    button.find("i").removeClass("text-danger").addClass("text-secondary");
                }
            },
            error: function () {
                alert("Error communicating with the server.");
            }
        });
    });
});