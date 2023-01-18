﻿const submitOrderWithPostOffices = document.getElementById("submitOrderWithPostOffices");
var commodityId = document.getElementById("commodityId").value;
submitOrderWithPostOffices.addEventListener("click", () => {
    var selectedPostOffice = document.getElementById("selectedPostOffices").value;
    
    if (selectedPostOffice != "") {
        var next = document.createElement("a");
        next.setAttribute("class", "btn btn-success");
        next.setAttribute("href", `/Identity/SaveOrder?typeOfDelivery=${selectedPostOffice}&commodityId=${commodityId}`);
        next.setAttribute("id", "wsdqeq")
        next.innerText = "Next";
        var partWithPostOffices = document.getElementById("PostOffices");
        partWithPostOffices.appendChild(next);
        
    }
});