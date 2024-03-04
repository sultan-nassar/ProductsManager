// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


    document.addEventListener('DOMContentLoaded', function () {
        var rows = document.querySelectorAll('.product-row');

        rows.forEach(function (row) {
            row.addEventListener('click', function () {
                var productId = this.getAttribute('data-product-id');
                window.location.href = '/ProductPage/' + productId;
            });
        });
    });



    document.addEventListener('DOMContentLoaded', function () {
        var buttons = document.querySelectorAll('.btn-primary');

        buttons.forEach(function (button) {
            button.addEventListener('click', function () {
                var productId = this.getAttribute('data-product-id');
                var checkbox = document.getElementById('cartCheck-' + productId);
                var label = document.querySelector('.cart-label[for="cartCheck-' + productId + '"]');

                if (checkbox && label) {
                    checkbox.checked = true;
                    label.classList.add('checked');
                    
                }
            });
        });
    });
