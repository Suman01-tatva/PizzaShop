const togglePassword = (input, icon) => {
  if (input.type === "password") {
    input.type = "text";
    icon.textContent = "";
    icon.src = "~/images/eye-open.png";
  } else {
    input.type = "password";
    icon.textContent = "";
    icon.src = "~/images/eye-close.png";
  }
};

//Custom  validation
document.addEventListener("DOMContentLoaded", () => {
  const form = document.querySelector("form");

  form.addEventListener("submit", function (event) {
    if (!this.checkValidity()) {
      event.preventDefault();
      this.classList.add("was-validated");
    }
  });
});
