let cart = JSON.parse(localStorage.getItem("shoppingCart")) ?? []
let uId = JSON.parse(localStorage.getItem("uId")) ?? 0
const card = document.querySelector(".card")

const redirectCart = document.querySelector("#home")
redirectCart.addEventListener("click", () => {
    const cartString = JSON.stringify(cart)
    localStorage.setItem("shoppingCart", cartString)
    window.location.href = "./index.html"
})


const addLibraryButton = document.getElementById("addButton")
addLibraryButton.addEventListener("click", (() => addLib()))

shopping(cart)

function shopping(list) {
    card.innerHTML = " "
    list.forEach((item) => {
        const cardContainer = document.createElement("div")
        cardContainer.classList.add("cardContainer")

        const cover = document.createElement("img")
        cover.classList.add("cover")
        cover.src = item.cover

        const audioDescription = document.createElement("div")
        audioDescription.classList.add("audioDescription")

        const cardContent = document.createElement("div")
        cardContent.classList.add("cardContent")

        const title = document.createElement("h5")
        title.classList.add("cardTitle")
        title.innerText = item.title

        const buttonRemove = document.createElement("button")
        buttonRemove.classList.add("buttonBuy")
        buttonRemove.innerText = "Remove"

        card.appendChild(cardContainer)
        cardContainer.appendChild(cover)
        cardContainer.appendChild(audioDescription)

        audioDescription.appendChild(cardContent)
        cardContent.appendChild(title)
        cardContent.appendChild(buttonRemove)

        buttonRemove.addEventListener("click", (() => { RemoveCard(item) }))
    })

}

function RemoveCard(indice) {
    cart = cart.filter((item) => item !== indice)
    const cartString = JSON.stringify(cart)
    localStorage.setItem("shoppingCart", cartString)
    shopping(cart)
}

function addLib() {

    const status = {
        "status": 0,
        "userId": uId,
    }
    fetch("http://localhost:5276/api/StatusTables",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(status),
        }).then(() => {


        })
        .catch(() => alert("Error"))



    const list = cart.map((item) => {
        return {

            "userId": uId,
            "songId": item.id,
        }
    })

    fetch("http://localhost:5276/api/UserSongs",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                userSongList: list
            }),
        }).then(() => {

            // const identifier = setInterval(async () => {
            //     let finishProcess = false
            //     const toJson = await fetch("http://localhost:5276/api/UserSongs?Id=uID")
            //     alert(toJson.Id)
            //     const dado = await toJson.json()
            //     if (dado == 1) {
            //         alert("New items added to your library!")
            //         finishProcess = true
            //     }

            //     if (finishProcess === true) {
            //         clearInterval(identifier)
            //     }
            // }, 100000)
            //     setInterval(() => {
            //         fetch('http://localhost:5276/api/UserSongs')
            //             .then(response => response.json())
            //             .then(data => {
            //                 //let finishProcess = false
            //                 const list = data.map(item => {
            //                     return {
            //                         id: item.id,
            //                         status: item.Status,
            //                         useId: item.UserId,
            //                     }
            //                 })
            //                 const filteredList = list.filter((item, index) => {
            //                     return index === list.findIndex(obj => {
            //                         return obj.UserId === item.UserId;
            //                     })
            //                 });
            //                 const lastItem = filteredList.pop();
            //                 alert(lastItem.Status)
            //                 if (lastItem.Status == 1) {

            //                     clearInterval(identifier)
            //                 }


            //             }, 60000)
            //     })
            // })
        })
        .catch(() => alert("Error :("))




    fetch("http://localhost:5276/api/StatusTables")
        .then(response => response.json())
        .then(data => {
            //let finishProcess = false
            const list = data.map(item => {
                return {
                    id: item.id,
                    status: item.status,
                    userId: item.userId
                }
            })
            const filteredList = list.filter((item) => item.userId === uId)
            const lastItem = filteredList.pop();
            alert(lastItem.id)
            if (lastItem.status == 1) {

                //clearInterval(identifier)
            }

        })
        .catch(error => {
            alert(error)
        })







    cart = []
    card.innerHTML = " "
    const cartString = JSON.stringify(cart)
    localStorage.setItem("shoppingCart", cartString)
}


