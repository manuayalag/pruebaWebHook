document.getElementById("sendMessageForm").addEventListener("submit", function (event) {
    event.preventDefault();

    const phoneNumber = document.getElementById("phoneNumber").value;
    const messageBody = document.getElementById("messageBody").value;

    fetch("/sendmessage", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            phoneNumber: phoneNumber,
            messageBody: messageBody
        })
    }).then(response => response.json())
        .then(data => {
            if (data.success) {
                alert("Message sent successfully!");
            } else {
                alert("Failed to send message.");
            }
        })
        .catch(error => console.error("Error:", error));
});
