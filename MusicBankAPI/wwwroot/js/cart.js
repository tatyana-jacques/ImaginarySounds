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
    const list = cart.map((item) => {
        return {

            "userId": uId,
            "songId": item.id,
        }
    })

    fetch("http://localhost:5276/api/UserSongs/PostUserSongs",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                userSongList: list
            }),
        }).then(() => {

            const identifier = setInterval(async () => {
                let finishProcess = false
                const toJson = await fetch("http://localhost:5276/api/UserSongs/GetStatusByUserId?userId=" + uId)
                const dado = await toJson.json()
                if (dado == 1) {
                    alert("Seus dados estão na biblioteca.")
                    finishProcess = true
                }

                if (finishProcess === true) {
                    clearInterval(identifier)
                }
            }, 1000)

        })
        .catch(() => alert("Error :("))


    cart = []
    card.innerHTML = " "
    const cartString = JSON.stringify(cart)
    localStorage.setItem("shoppingCart", cartString)

}
