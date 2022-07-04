$(document).ready(function () {
    $("#btnSubmit").mouseenter(GetCost);
    $("#btnSubmit").focus(GetCost);
});

function GetCost() {
    var quantity = parseInt($('#Stock_Quantity').val());
    var price = parseInt($('#Stock_RetailPrice').val());
    $('#Stock_TotalPrice').val(quantity * price);
}