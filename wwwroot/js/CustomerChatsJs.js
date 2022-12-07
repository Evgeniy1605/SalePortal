document.getElementById("asCustomer").addEventListener("click", function () {
    document.getElementById("chats").innerHTML = "";
    var userId = parseInt(document.getElementById("userId").value);
    $('#chats').load(`/Chat/CustomerChats/?userId=${userId}`)
});