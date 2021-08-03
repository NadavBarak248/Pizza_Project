var order = [];
var row_counter = 0;
function AddPizza(id) {
    row_counter += 1;
    order.push(id);

    document.getElementById("orderready").value = "Send Order (" + order.length + ")";

    var ordertbl = document.getElementById("myOrder");
    var pizzarow = ordertbl.insertRow(0);
    pizzarow.id = "row_" + row_counter;

    var pname = pizzarow.insertCell(0);
    var pprice = pizzarow.insertCell(1);
    var premove = pizzarow.insertCell(2);

    pname.innerHTML = document.getElementById("name_" + id).innerHTML;
    pprice.innerHTML = document.getElementById("price_" + id).innerHTML;
    premove.innerHTML = "<input type='button' class='btn btn-close' onclick='RemoveId(" + id + "," + row_counter + ")' />";
}

function RemoveId(id, rowid) {
    var idx = order.findIndex(p => p == id);
    order.splice(idx, 1);

    document.getElementById("row_" + rowid).remove();
    document.getElementById("orderready").value = "Send Order (" + order.length + ")";
}

function MyOrder() {
    if (order.length == 0) {
        document.getElementById("orderError").innerHTML = "Must Select Pizzas";
        return false;
    }

    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var branchId = $("#selectedBranch").val();
    $.ajax({
        type: 'post',
        url: "/Orders/Create",
        dataType: 'JSON',
        data: {
            __RequestVerificationToken: token,
            pizzaIds: order,
            branchid: branchId
        },
        success: function (response) {
            window.location.href = response.redirectToUrl;
        }
    });
}