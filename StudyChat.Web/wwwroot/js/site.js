// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    if ($('div.alert.notification').length > 0)
        setTimeout(() => $('div.alert.notification').fadeOut('slow'), 3000);
});