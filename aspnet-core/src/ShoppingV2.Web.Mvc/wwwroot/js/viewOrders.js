function calculate() {
    var table = document.getElementById("orderitemstable");

    for (var i = 0; i < table.rows.length; i++) {
        var row = table.rows[i];
        var unitPrice = row.cells[4].childNodes[0].value;
        var prodQuantity = row.cells[5].childNodes[0].value;
        var answer = (Number(unitPrice) * Number(prodQuantity));
        row.cells[6].childNodes[0].value = answer;
    }

}
var products = [];
function updateOrder() {
    var orderId = 0;

    var table = document.getElementById("orderitemstable");
    var checkBox = table.getElementsByTagName("input");
    for (var i = 1; i < checkBox.length; i++) {
        if (checkBox[i].checked) {
            var row = checkBox[i].parentNode.parentNode;
            var orditid = row.cells[0].innerText;
            var prodid = row.cells[1].innerText;
            var ordid = row.cells[2].innerText;
            var date = row.cells[3].childNodes[0].value;
            var uprice = row.cells[4].childNodes[0].value;
            var qty = row.cells[5].childNodes[0].value;
            var prodprice = row.cells[6].childNodes[0].value;

            orderId = parseInt(ordid);

            var ordersLineItem = {
                Id: parseInt(orditid),
                OrderItemDate: date,
                ProductId: parseInt(prodid),
                OrderitemUnitPrice: parseInt(uprice),
                OrderitemQuantity: parseInt(qty),
                OrderItemProductPrice: parseInt(prodprice),
                OrderId: parseInt(ordid),
                IsDelete: false
            };
            ordersLineItem.OrderId = parseInt(orderId);

            products.push(ordersLineItem);
        } 
    }

    order = {
        orderId: orderId,
        orderLineItems: products
    }

    console.log(JSON.stringify(order));
    $.ajax({
        url: '../Orders/EditOrders',
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(order),
        processData: false,
        success: function (data) {
            console.log(data);
            alert("The Selected OrderLine(s) has been checked and updated Successfully!");
            location.reload();
        },
        error: function () {
            location.reload();
        }

    });
    alert("The Selected OrderLine(s) has been checked and updated Successfully! If the Quantity is above 100 the selected orderLine(s) will not be updated successfully!");
    location.reload();
}

function deleteOrder() {
    var orderId = 0;
    var http = new XMLHttpRequest();

    var table = document.getElementById("orderitemstable");
    var checkBox = table.getElementsByTagName("input");
    for (var i = 1; i < checkBox.length; i++) {
        if (checkBox[i].checked) {
            var row = checkBox[i].parentNode.parentNode;
            var orditid = row.cells[0].innerText;
            var prodid = document.getElementById("getproductid").innerText;
            var ordid = row.cells[2].innerText;
            var date = document.getElementById("getdate").value;
            var uprice = document.getElementById("getunitprice").value;
            var qty = document.getElementById("getquantity").value;
            var prodprice = document.getElementById("getproductprice").value;

            orderId = parseInt(ordid);

            var ordersLineItem = {
                Id: parseInt(orditid),
                OrderItemDate: date,
                OrderId: parseInt(ordid),
                ProductId: parseInt(prodid),
                OrderitemUnitPrice: parseInt(uprice),
                OrderitemQuantity: parseInt(qty),
                OrderItemProductPrice: parseInt(prodprice),
                IsDelete: true
            };

            products.push(ordersLineItem);
        } 
    }

    order = {
        orderId: orderId,
        orderLineItems: products
    }

    console.log(JSON.stringify(order));
    $.ajax({
        url: '../Orders/DeleteOrder',
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(order),
        processData: false,
        success: function (data, textStatus, jQxhr) {
            console.log(data, textStatus, jQxhr);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(jqXhr, textStatus, errorThrown);
        }
    });
    alert("The Selected Order Line(s) has been checked and deleted Successfully!");
    location.reload();
}

