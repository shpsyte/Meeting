"use strict";

var connection_link = new signalR.HubConnectionBuilder()
  .withUrl("/newLinkHub")
  .build();

connection_link
  .start()
  .then()
  .catch(function(err) {
    return console.error(err.toString());
  });

connection_link.on("UpdateLink", function(link) {
  let link_element = document.querySelector("#zoom_link");
  let text = "Enter in Class";
  link_element.setAttribute("href", link);
  link_element.innerHTML = link;
});
