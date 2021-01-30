﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
console.log("Initialization check for JS");

$("#ExpeditionSubmit").click(function () {
    alert("Expedition Submitted to Database");
});

$(document).ready(function () { window.setInterval(execute, 10000) });
function execute() {
    $.getJSON( '/api/count', (data) => {
        showCount(data)
    });
}
function showCount(data) {
    $('#stat').fadeOut();
    $('#stat').empty();
    setTimeout(1000);
    $('#stat').append(data.val);
    $('#stat').fadeIn();
}