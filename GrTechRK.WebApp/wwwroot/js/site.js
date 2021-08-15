// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function getValue(id) {
    return $(id).val();
}

function setValue(id, value) {
    $(id).val(value);
}

function setHtml(id, htmlText) {
    $(id).html(htmlText);
}

function openStaticModal(id) {
    $(id).modal({ backdrop: 'static' });
}

function closeModal(id) {
    $(id).modal('hide');
}

function disableElement(id) {
    $(id).prop("disabled", true);
}

function enableElement(id) {
    $(id).prop("disabled", false);
}