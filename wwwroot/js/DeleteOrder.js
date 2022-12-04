document.getElementById("deleteOrder").addEventListener("click", function () {
    var Delete = prompt("Are you sure that you wanna delete this order? (Yes/no)");
    if (Delete === "Yes") {
        var id = document.getElementById("orderId").value;
        $.ajax({
            type: "POST",
            url: `/Identity/DeleteOrder/?id=${id}`,
            success: function () {
                window.location.href = "/Identity/UserPage";
                
            }

        });
    }
});