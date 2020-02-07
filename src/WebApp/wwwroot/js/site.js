let participants = [];

// Write your JavaScript code.
function ToggleSpiner(behavior) {
  // document.querySelector("#loading").classList.toggle("hide");

  if (behavior === "hide")
    document.querySelector("#loading").classList.add("hide");
  else document.querySelector("#loading").classList.remove("hide");
}

function SendParticipantToServer() {
  let chck = document.querySelector("#AcitveP");
  chck.addEventListener("click", function() {
    document.querySelector("#Active").value = this.checked;
  });

  let btn = document.querySelector("#sent");

  if (btn === null) return;

  btn.addEventListener("click", function() {
    event.preventDefault();
    ToggleSpiner("show");

    let url = btn.getAttribute("data-url");
    let token = document.querySelector("input[name=__RequestVerificationToken]")
      .value;
    let email = document.querySelector("#Email").value;
    let name = document.querySelector("#Name").value;
    let active = document.querySelector("#Active").value;
    let data = { email, name, active };

    if (email === "") {
      document.querySelector("#errors").innerHTML = "Email is required";

      ToggleSpiner("hide");
      return;
    }

    if (name === "") {
      document.querySelector("#errors").innerHTML = "Name is required";

      ToggleSpiner("hide");
      return;
    }

    _post(url, token, data, SuccessSendParticipantToServer, error);
  });
}

function error(data) {
  console.log("Erro -> Faz alguma coisa", data);
}

function SuccessSendParticipantToServer(data) {
  let link_element = document.querySelector("#zoom_link");
  let link = data.atualmetting.link;
  let text =
    link === null
      ? "The link will be provide, please wait..."
      : "Enter in Class";
  link_element.setAttribute("href", link);
  link_element.innerHTML = text;
  ToggleSpiner("hide");
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

function CreateBindToGetParticipants() {
  //let btn = document.querySelector("#getparticipants");

  setTimeout(GetParticipants, 2000);
  // btn.addEventListener("click", () => {
  //   event.preventDefault();
  //   ToggleSpiner("show");
  //   GetParticipants();
  //   ToggleSpiner("hide");
  // });

  setInterval(GetParticipants, 5000);
}

function SuccessCreateLinkToServer(data) {
  let link = document.querySelector("#Link");
  let errors = document.querySelector("#errors");
  if (data.success) {
    errors.innerHTML = "Link create/updated with success";
    link.value = data.meetingSetup.link;
  } else {
    errors.innerHTML = data.errors[0].message;
  }

  setTimeout(function() {
    errors.innerHTML = "";
  }, 1500);
}

function GetParticipants() {
  let btn = document.querySelector("#getparticipants");
  let url = btn.getAttribute("data-url");
  _get(url, SuccessGetParticipantes, error);
}

function SuccessGetParticipantes(data) {
  let ul = document.querySelector("#participants");
  ul.innerHTML = "";
  participants = [];

  let participantsjson = data.data;

  if (data.data.length === 0) {
    let li = document.createElement("li");
    let text = document.createTextNode("Ow now... nobody here.... ;(");
    li.appendChild(text);
    ul.appendChild(li);
  }

  for (const participant of participantsjson) {
    participants.push(participant);

    let li = document.createElement("li");
    let text = document.createTextNode(
      participant.name === null ? participant.email : participant.name
    );
    if (!participant.active) {
      li.classList.add("inactive");
    }

    li.appendChild(text);
    ul.appendChild(li);
  }
  let total = participants.length;

  document.querySelector("#players").innerHTML = "Player" + "(" + total + ")";
}

function CreateBindToCreatePair() {
  let btn = document.querySelector("#createpair");

  btn.addEventListener("click", function() {
    ToggleSpiner("show");
    createpair();
    ToggleSpiner("hide");
  });
}

function createpair() {
  let activesparticipants = [];
  let ul = document.querySelector("#pairparticipants");
  ul.innerHTML = "";
  for (const participant of participants) {
    if (participant.active) activesparticipants.push(participant);
  }
  let total = activesparticipants.length;

  if (total == 0) {
    addLiToUl(ul, "Ow not... nobody here ... ;(");
    return;
  }

  if (total >= 2) {
    for (let index = 0; index < Math.floor(total / 2); index++) {
      activesparticipants = shuffle(activesparticipants);
      let par1 = activesparticipants.pop();
      activesparticipants = shuffle(activesparticipants);
      let par2 = activesparticipants.pop();
      // console.log(index, activesparticipants.length, activesparticipants);
      let name = par1.name + "   .................   " + par2.name;
      addLiToUl(ul, name);
    }
  } else {
    let par1 = activesparticipants.pop();
    let name = par1.name + " ................." + "Tacher";
    addLiToUl(ul, name);
  }

  if (activesparticipants.length == 1) {
    let par1 = activesparticipants.pop();
    let name = par1.name + " ................." + "Tacher";
    addLiToUl(ul, name);
  }
}

function addLiToUl(ul, text) {
  let li = document.createElement("li");
  li.appendChild(document.createTextNode(text));
  ul.appendChild(li);
}

function shuffle(array) {
  var currentIndex = array.length,
    temporaryValue,
    randomIndex;

  while (0 !== currentIndex) {
    randomIndex = Math.floor(Math.random() * currentIndex);
    currentIndex -= 1;

    temporaryValue = array[currentIndex];
    array[currentIndex] = array[randomIndex];
    array[randomIndex] = temporaryValue;
  }

  return array;
}

function _get(url, callback, errorcallback) {
  // console.log("Getting data....", url);
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
