var productId = document.getElementById("productpick").value;

function LoadProducts() {
    var http = new XMLHttpRequest();
    console.log(productId);
    http.open('GET', '../products/GetProductById/' + productId, true);
    http.send();

    http.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var oj = JSON.parse(this.responseText);
            if (oj !== null) {
                document.getElementById("desc").innerText = oj.result.productDescription;
                document.getElementById("price").innerText = oj.result.unitPrice;
            }

        } else {
            console.log("No Product Found!");
        }
    };
}

var productpick = document.getElementById("productpick");
productpick.addEventListener("change", (e) => {
    productId = e.target.value
    LoadProducts()
})

function calculate() {
    var field1 = document.getElementById("price").innerText;
    var field2 = document.getElementById("quantity").value;

    var result = parseFloat(field1) * parseFloat(field2);


    if (!isNaN(result)) {
        document.getElementById("subtotal").innerText = result;

    }
}

var quantityinput = document.getElementById("quantity");
quantityinput.addEventListener("input", (e) => {
    calculate()
})

var customerId = document.getElementById("customerpick").value;
function LoadCustomers() {
    var http = new XMLHttpRequest();
    console.log(customerId);
    http.open('GET', '../customer/GetCustomerById/' + customerId, true);
    http.send();

    http.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var obj = JSON.parse(this.responseText);


        } else {
            console.log("No Customer Name Found!");
        }
    };
}

var customerpick = document.getElementById("customerpick");
customerpick.addEventListener("input", (c) => {
    customerId = c.target.value
    LoadCustomers()
})

window.onload = function () {
    n = new Date();
    y = n.getFullYear();
    m = n.getMonth() + 1;
    d = n.getDate();
    document.getElementById("pdate").innerHTML = y + "-" + m + "-" + d;
}

var customerName;
var date;
var productName;
var unitPrice;
var quantity;
var subTotal;

function AddItems() {
    var table = document.getElementById("tableproduct");
    customerName = document.getElementById("customerpick").value;
    date = document.getElementById("pdate").innerText;
    productName = document.getElementById("productpick").value;
    unitPrice = document.getElementById("price").innerText;
    quantity = document.getElementById("quantity").value;
    subTotal = document.getElementById("subtotal").innerText;

    var prodCustName = document.createElement("input");
    prodCustName.setAttribute("type", "text");
    prodCustName.setAttribute("id", "custname");
    prodCustName.setAttribute("class", "form-control");
    prodCustName.disabled = true;

    var prodDate = document.createElement("input");
    prodDate.setAttribute("type", "text");
    prodDate.setAttribute("id", "prodate");
    prodDate.setAttribute("class", "form-control");
    prodDate.disabled = true;

    var prodName = document.createElement("input");
    prodName.setAttribute("type", "text");
    prodName.setAttribute("id", "prodname");
    prodName.setAttribute("class", "form-control");
    prodName.disabled = true;

    var prodUnitPrice = document.createElement("input");
    prodUnitPrice.setAttribute("type", "text");
    prodUnitPrice.setAttribute("id", "unitprice");
    prodUnitPrice.setAttribute("class", "form-control");
    prodUnitPrice.disabled = true;

    var prodQuantity = document.createElement("input");
    prodQuantity.setAttribute("type", "text");
    prodQuantity.setAttribute("id", "prodquantity");
    prodQuantity.setAttribute("class", "form-control");
    prodQuantity.disabled = true;

    var prodSubTotal = document.createElement("input");
    prodSubTotal.setAttribute("type", "text");
    prodSubTotal.setAttribute("id", "subtotal");
    prodSubTotal.setAttribute("class", "form-control");
    prodSubTotal.disabled = true;

    var btnDelete = document.createElement("input");
    btnDelete.setAttribute("type", "button");
    btnDelete.setAttribute("id", "btndelete");
    btnDelete.setAttribute("onclick", "deleteRows()");
    btnDelete.setAttribute("class", "btn btn-danger");
    btnDelete.setAttribute("value", "Delete");

    prodCustName.value = customerName;
    prodDate.value = date;
    prodName.value = productName;
    prodUnitPrice.value = unitPrice;
    prodQuantity.value = quantity;
    prodSubTotal.value = subTotal;

    var row = table.insertRow(1);

    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    var cell4 = row.insertCell(3);
    var cell5 = row.insertCell(4);
    var cell6 = row.insertCell(5);
    var cell7 = row.insertCell(6);

    cell1.appendChild(prodCustName);
    cell2.appendChild(prodDate);
    cell3.appendChild(prodName);
    cell4.appendChild(prodUnitPrice);
    cell5.appendChild(prodQuantity);
    cell6.appendChild(prodSubTotal);
    cell7.appendChild(btnDelete);

    AddOrderLine();
}

var products = [];
var orderLine;
function AddOrderLine() {
    orderLine = {
        OrderitemDate: null,
        ProductId: 0,
        OrderitemUnitPrice: 0,
        OrderitemQuantity: 0,
        OrderitemProductPrice: 0,
        OrderId: 0

    };


    orderLine.OrderitemDate = date;
    orderLine.ProductId = parseInt(productName);
    orderLine.OrderitemUnitPrice = parseInt(unitPrice);
    orderLine.OrderitemQuantity = parseInt(quantity);
    orderLine.OrderitemProductPrice = parseInt(subTotal);
    orderLine.OrderId = 0;
    products.push(orderLine);

    console.log(products);
}

function confirmOrder() {

    var date = document.getElementById("pdate").innerText;
    var customer = document.getElementById("customerpick").value;

    var orders = {
        ProductOrderDate: date,
        CustomerId: parseInt(customer),
        OrderLineItems: products
    };
    console.log(JSON.stringify(orders));
    $.ajax({
        url: '../AddCart/CreateOrder',
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(orders),
        processData: false,
        success: function (data) {
            console.log(data);
            alert("The Selected OrderLine(s) has been checked and saved Successfully!");
            location.reload();
        },
        error: function () {
            location.reload();
        }

    });
    alert("The order has been checked and updated Successfully! If the Quantity is above 100 the order will not be updated successfully!");
    location.reload();
}

function deleteRows() {
    var table = document.getElementById("tableproduct");
    var rowCount = table.rows.length;

        var row = table.deleteRow(rowCount - 1);
        rowCount--;
    }
 
function deleteEntireOrder() {
    var http = new XMLHttpRequest();

    var table = document.getElementById("orderstable");
    for (var i = 1; i < table.rows.length; i++) {
        table.rows[i].onclick = function () {
            var ordid = this.cells[0].innerText;
            var date = this.cells[2].innerText;
            var customerId = this.cells[1].innerText;
            


            var orders = {
                Id: parseInt(ordid),
                OrderitemDate: date,
                CustomerId: parseInt(customerId),
                Products: products
                

            };
            console.log(JSON.stringify(orders));
            $.ajax({
                url: '../Orders/DeleteEntireOrder',
                dataType: 'json',
                type: 'post',
                contentType: 'application/json',
                data: JSON.stringify(orders),
                processData: false,
                success: function (data, textStatus, jQxhr) {
                    console.log(data, textStatus, jQxhr);
                },
                error: function (jqXhr, textStatus, errorThrown) {
                    console.log(jqXhr, textStatus, errorThrown);
                }
            });
            alert("The Order has been checked and deleted Successfully!");
            location.reload();
        }
    }

}