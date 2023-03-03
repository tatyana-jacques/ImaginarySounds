let cart = JSON.parse(localStorage.getItem("shoppingCart")) ?? []

const cartNumber = document.querySelector("#cartNumber")
cartNumber.innerHTML = cart.length

const uName = document.getElementById("uName")
const email = document.getElementById("email")
const password = document.getElementById("password")

const home = document.querySelector("#home")
home.addEventListener("click", () => {
    const cartString = JSON.stringify(cart)
    localStorage.setItem("shoppingCart", cartString)
    window.location.href = "./index.html"

})

const submit = document.getElementById("submit")
submit.addEventListener("click", () => { register() })

function register() {
    const inputs = {

        "name": uName.value,
        "email": email.value,
        "password": password.value
    }
    fetch("http://localhost:5276/api/Users",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(inputs),
        }).then(() => {
            alert("Successful registration!")
            uName.innerHTML = ""
            email.innerHTML = ""
            window.location.href = "./login.html"

        })
        .catch(() => alert("Error :("))

}
