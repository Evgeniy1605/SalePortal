const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();
const chatId = parseInt(document.getElementById("chatId").value);

document.getElementById("sendBtn").addEventListener("click", function () {
    const userId = parseInt(document.getElementById("userId").value); 
    const message = document.getElementById("chatInput").value;
    hubConnection.invoke("Send", message, userId, chatId) 
        .catch(function (err) {
            return console.error(err.toString());
        });
});

hubConnection.on("Receive", function (message, userName) {
    const p = document.createElement("p");
    p.textContent = userName + ":" + " ";
    const b = document.createElement("b");
    b.textContent = message;
    p.appendChild(b);
    document.getElementById("chatroom").appendChild(p);
    
});
hubConnection.start()
    .then(function () {
        document.getElementById("sendBtn").disabled = false;
    })
    .then(function () {
        hubConnection.invoke("Enter", chatId);
    })
    .catch(function (err) {
        return console.error(err.toString());
    });