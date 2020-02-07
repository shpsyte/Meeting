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

function NotifyNewParticipant(name, email) {
  connection.invoke("SendMessage", name, email).catch(function(err) {
    return console.error(err.toString());
  });
}

connection.on("SendMessage", function(user, email) {
  participants.push(user);
  let ul = document.querySelector("#participants");
  if (ul === null) return;
  let li = document.createElement("li");
  li.appendChild(document.createTextNode(user));
  ul.appendChild(li);

  document.querySelector("#players").innerHTML =
    "Players" + "(" + participants.length + ")";
});
