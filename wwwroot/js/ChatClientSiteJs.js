const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

document.getElementById("sendBtn").addEventListener("click", function () {
    const userName = document.getElementById("userName").value;   // получаем введенное имя
    const message = document.getElementById("message").value;

    hubConnection.invoke("Send", message, userName) // отправка данных серверу
        .catch(function (err) {
            return console.error(err.toString());
        });
});
// получение данных с сервера
hubConnection.on("Receive", function (message, userName) {

    // создаем элемент <b> для имени пользователя
    const userNameElem = document.createElement("b");
    userNameElem.textContent = `${userName}: `;

    // создает элемент <p> для сообщения пользователя
    const elem = document.createElement("p");
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(message));

    // добавляем новый элемент в самое начало
    // для этого сначала получаем первый элемент
    const firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
});

hubConnection.start()
    .then(function () {
        document.getElementById("sendBtn").disabled = false;
    })
    .catch(function (err) {
        return console.error(err.toString());
    });