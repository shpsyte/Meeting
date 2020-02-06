// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function SendParticipantToServer() {
  let btn = document.querySelector("#sent");

  if (btn === null) return;

  btn.addEventListener("click", function() {
    event.preventDefault();

    let url = btn.getAttribute("data-url");
    let token = document.querySelector("input[name=__RequestVerificationToken]")
      .value;
    let email = document.querySelector("#Email").value;
    let name = document.querySelector("#Name").value;
    let data = { email, name };

    if (email === "") {
      document.querySelector("span[data-valmsg-for='Email']").innerHTML =
        "This field is required";
      return;
    }

    _post(url, token, data, SuccessSendParticipantToServer, error);
  });
}

function error(data) {
  console.log("Erro -> Faz alguma coisa", data);
}

function SuccessSendParticipantToServer(data) {
  let errors = document.querySelector("#errors");
  if (data.success) {
    errors.innerHTML = data.atualmetting.link;
  } else {
    errors.innerHTML = "The link will be provide, please wait...";
  }
}

/* Regras para o servidor */

function CreateLinkToServer() {
  let btn = document.querySelector("#sent");

  if (btn === null) return;

  btn.addEventListener("click", function() {
    event.preventDefault();

    let url = btn.getAttribute("data-url");
    let token = document.querySelector("input[name=__RequestVerificationToken]")
      .value;
    let link = document.querySelector("#Link").value;

    var data = { link };

    _post(url, token, data, SuccessCreateLinkToServer, error);
  });
}

function SuccessCreateLinkToServer(data) {
  let link = document.querySelector("#Link");
  let errors = document.querySelector("#errors");
  if (data.success) {
    errors.textContent = "Link create/updated with success";
    link.value = data.meetingSetup.link;
  } else {
    errors.textContent = data.errors[0].message;
    console.log("Algo deu errado --> ", data.errors[0].message);
  }
}

function GetParticipants() {
  let btn = document.querySelector("#getparticipants");

  if (btn === null) return;

  btn.addEventListener("click", function() {
    event.preventDefault();
    let url = "/Home/GetParticipants/";
    _get(url, SuccessGetParticipantes, error);
  });
}

function SuccessGetParticipantes(data) {
  let participants = data.data;
  console.log(data);

  let ul = document.querySelector("#participants");
  ul.innerHTML = "";
  for (const participant of participants) {
    let li = document.createElement("li");

    if (!participant.active) {
      li.classList.add("inactive");
    }
    let text = document.createTextNode(
      participant.name === null ? participant.email : participant.name
    );
    li.appendChild(text);
    ul.appendChild(li);
  }
}

function _get(url, callback, errorcallback) {
  console.log("Getting data....", url);
  $.ajax({
    type: "GET",
    url: url,
    error: function(data) {
      errorcallback(data);
    },
    success: function(data) {
      callback(data);
    },
    dataType: "json"
  });
}

function _post(url, token, data, callback, errorcallback) {
  console.log("Posting data....", url, data);
  $.ajax({
    type: "POST",
    headers: {
      RequestVerificationToken: token
    },
    url: url,
    data: data,
    error: function(data) {
      errorcallback(data);
    },
    success: function(data) {
      callback(data);
    },
    dataType: "json"
  });
}
