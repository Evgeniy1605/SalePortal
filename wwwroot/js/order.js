const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/orders")
    .build();

// отправка сообщения на сервер
document.getElementById("GetOrders").addEventListener("click", function () {

    const userId = parseInt(document.getElementById("userId").value);


    hubConnection
        .invoke("Orders", (userId))
        .catch(function (err) {
            return console.error(err.toString());
        });
});
hubConnection.on("Receive", function (result) {
    document.getElementById("ordersResult").innerHTML = " ";
    result.forEach(item => {
        
        var id = item.id;
        var commodityId = item.commodity.id;
        var price = item.commodity.price;
        var imagePath = `~/Images/${commodityId}.png`
        var name = item.commodity.name;
        document.getElementById("ordersResult").innerHTML += `<td><img src="${imagePath}"  width="50" /></td> <tr><td>${name}</td> <td>${price} $</td>  <td><a class="btn btn-primary" href="/Identity/DetailsOfOrder/?id=${id}" >Details</a></td> </tr>`

        
    });
});

// Seles
// отправка сообщения на сервер
document.getElementById("GetSales").addEventListener("click", function () {

    const userId = parseInt(document.getElementById("userId").value);


    hubConnection
        .invoke("Sales", (userId))
        .catch(function (err) {
            return console.error(err.toString());
        });
});

hubConnection.on("Receive", function (result) {
    document.getElementById("ordersResult").innerHTML = " ";
    result.forEach(item => {

        var id = item.id;
        var commodityId = item.commodity.id;
        var price = item.commodity.price;
        var imagePath = `~/Images/${commodityId}.png`
        var name = item.commodity.name;
        var approved = item.approvedByOwner;
        document.getElementById("ordersResult").innerHTML += `<td><img asp-append-version ="true" src="${imagePath}"  width="50" /></td> <tr><td>${name}</td> <td>${price} $</td>  <td><a class="btn btn-primary" href="/Identity/DetailsOfOrder/?id=${id}" >Details</a></td> </tr>`
       

    });
});
hubConnection.start()
    .then(function () {
        document.getElementById("GetOrders").disabled = false;
    })
    .catch(function (err) {
        return console.error(err.toString());
    });