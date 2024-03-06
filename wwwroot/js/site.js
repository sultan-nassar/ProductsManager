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






