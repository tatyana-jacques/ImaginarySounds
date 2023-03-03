let cart = JSON.parse(localStorage.getItem("shoppingCart")) ?? []
let logedIn = false;

const cartNumber = document.querySelector("#cartNumber")
cartNumber.innerHTML = cart.length

const home = document.querySelector("#home")
home.addEventListener("click", () => {
    const cartString = JSON.stringify(cart)
    localStorage.setItem("shoppingCart", cartString)
    window.location.href = "./index.html"

})
const email = document.getElementById("email")
const password = document.getElementById("password")
const submit = document.getElementById("submit")
submit.addEventListener("click", () => getUsers())


function getUsers() {
    fetch("http://localhost:5276/api/Users")
        .then(e => e.json())
        .then(data => {
            const list = data.map(item => {
                return {
                    id: item.id,
                    email: item.email,
                    password: item.password,
                    name: item.name,
                }
            })

            list.forEach((item) => {
                //alert("welcome" + item.name)
                if (item.email === email.value && item.password === password.value) {
                    alert("Welcome " + item.name + "!")
                    logedIn = true
                    const idString = JSON.stringify(item.id)
                    localStorage.setItem("uId", idString)
                    window.location.href = "./cart.html"

                }

            })

            if (!logedIn) {
                alert("Invalid login!")
            }


        })

        .catch(error => {
            alert(error)
        })

}



