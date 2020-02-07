"use strict";

var connection = new signalR.HubConnectionBuilder()
  .withUrl("/newParticipantHub")
  .build();

connection
  .start()
  .then(function() {
    console.log("escutando....");
  })
  .catch(function(err) {
    return console.error(err.toString());
  });

function NotifyNewParticipant(data) {
  connection.invoke("SendMessage", data).catch(function(err) {
    return console.error(err.toString());
  });
}

connection.on("SendMessage", function(data) {
  var registrado = false;
  for (const iterator of participants) {
    if (iterator.name === data.name && iterator.email === data.email) {
      registrado = true;
    }
  }

  if (!registrado) {
    participants.push(data);

    let ul = document.querySelector("#participants");
    if (ul === null) return;
    let li = document.createElement("li");
    li.appendChild(document.createTextNode(data.name));

    if (!data.active) {
      li.classList.add("inactive");
    }
    ul.appendChild(li);

    document.querySelector("#players").innerHTML =
      "Players" + "(" + participants.length + ")";
  }
});
