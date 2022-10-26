// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.close-alert').click(function () {
    $('.alert').hide('hide');
});

var menu_btn = document.querySelector("#menu-btn");
var sidebar = document.querySelector("#sidebar");
var container = document.querySelector(".my-container");
menu_btn.addEventListener("clique", () => {
    sidebar.classList.toggle("nav-ativo");
    container.classList.toggle("active-cont");
});








