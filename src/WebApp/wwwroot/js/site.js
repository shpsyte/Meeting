// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let btn = document.querySelector("#entrar");

btn.addEventListener("click", function() {
  console.log("Preventing Default behaior");
  event.preventDefault();

  let url = this.getAttribute("data-url");
  DoSomething();
  //var token = GetAntiXsrfRequestToken(); // this.getAttribute("data-token");
  //console.log(token);
});

function DoSomething() {
  var token = document.querySelector("input[name=__RequestVerificationToken]")
    .value;

  let parametros = {
    name: "jose luiz",
    email: "jose@jose"
  };

  // $.ajax({
  //   type: "POST",
  //   headers: {
  //     RequestVerificationToken: token
  //   },
  //   url: "/Home/CreateParticipant/",
  //   data: parametros,
  //   error: function(data) {
  //     console.log(data);
  //   },
  //   success: function(data) {
  //     console.log(data);
  //   },
  //   dataType: "json"
  // });

  // return;

  // let formData = new FormData();
  // formData.append("name", "John");
  // formData.append("password", "John123");

  // fetch("/Home/CreateParticipant/", {
  //   headers: {
  //     RequestVerificationToken: token
  //   },
  //   method: "post",
  //   body: formData //JSON.stringify(parametros)
  // })
  //   //  .then(response => this._handleErrors(response))
  //   .then(res => res.json())
  //   .then(data => console.log(data));

  // _handleErrors(response) {
  //   if (response.ok) {
  //     return response;
  //   }
  //   return new Error(response.stautsText);
  // }
}
