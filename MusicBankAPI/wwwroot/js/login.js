const email = document.getElementById("email")
const password = document.getElementById("password")


buttonSubmit.addEventListener("click", () => getUsers())


function getUsers() {
    fetch("http://localhost:5276/api/Users")
        .then(e => e.json())
        .then(data => {
            const list = data.map(item => {
                return {
                    id: item.id,
                    email: item.email,
                    senha: item.senha,
                    name: item.name,
                }
            })

            list.forEach((item) => {
                if (item.email === email && item.senha === password) {
                    alert("Welcome " + item.name + "!")
                    const userId = JSON.stringify(item.id)
                    localStorage.setItem("id", idString)

                }
            }
            )


        })
        .catch(alert("Inexistent user"))
}



