$(document).ready(function () {
    $('.autoPostBack').change(function () {
        $('.autoPostBack').val
        $(this).closest('form').submit();
    });
});