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

    console.log("Posting data....", data);

    _post(url, token, data);
  });
}

function _post(url, token, data) {
  $.ajax({
    type: "POST",
    headers: {
      RequestVerificationToken: token
    },
    url: url,
    data: data,
    error: function(data) {
      console.log(data);
    },
    success: function(data) {
      if (data.success) {
        console.log("Sucesso -> Faz alguma coisa", data);
      } else {
        console.log("Erro -> Faz alguma coisa", data);
      }
    },
    dataType: "json"
  });
}
