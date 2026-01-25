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
                    button.find("i").removeClass("text-secondary").addClass("text-danger");
                } else {
                    button.find("i").removeClass("text-danger").addClass("text-secondary");
                }
            },
            error: function () {
                alert("Error communicating with the server.");
            }
        });
    });
});
function addToCart(productId, callBackUrl) {
    fetch('/Cart/AddToCart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value // ضد CSRF
        },
        body: `productId=${productId}&callBackUrl=${encodeURIComponent(callBackUrl)}`
    })
        .then(response => {
            if (response.ok) {
                window.location.href = callBackUrl;
            } else {
                alert("Something went wrong, please try again.");
            }
        })
        .catch(error => console.error('Error:', error));
}
function updateCart(productId, quantity, $input) {
    $.ajax({
        url: '/Cart/UpdateQuantity',
        type: 'POST',
        data: { productId: productId, quantity: quantity },
        success: function (response) {
            // response = { productTotal, total, cartQuantity }

            // cart page: update product total
            let row = $input.closest("tr");
            if (row.length) {
                row.find(".product-total").text("$" + response.productTotal);
            }

            // shared: update totals + badge
            $("#total").text(response.total);
            $("#cartQuantityBadge").text(response.cartQuantity);
        },
        error: function () {
            alert("Failed to update quantity.");
        }
    });
}

// input change (typing or manual change)
$(document).on("change", ".quantity-input", function () {
    let $input = $(this);
    let productId = $input.data("product-id") || $input.closest("[data-product-id]").data("product-id");
    let quantity = parseInt($input.val()) || 1;

    updateCart(productId, quantity, $input);
});

// plus/minus buttons
$(document).on("click", ".number-increment, .inumber-decrement", function () {
    let $input = $(this).siblings(".quantity-input");
    let productId = $input.data("product-id") || $input.closest("[data-product-id]").data("product-id");
    let quantity = parseInt($input.val()) || 1;

    updateCart(productId, quantity, $input);
});
