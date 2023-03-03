let cart = JSON.parse(localStorage.getItem("shoppingCart")) ?? []

const card = document.querySelector(".card");

const cartNumber = document.querySelector("#cartNumber")
cartNumber.innerHTML = cart.length

const stopAudio = document.querySelector("#stop")
stopAudio.addEventListener("click", (() => {
    const audio = document.querySelector('audio')
    audio.src = audio
    audio.stop()

}))

const redirectCart = document.querySelector("#redirectCart")
redirectCart.addEventListener("click", () => {
    const cartString = JSON.stringify(cart)
    localStorage.setItem("shoppingCart", cartString)
    window.location.href = "./cart.html"

})

const redirectRegister = document.querySelector("#redirectRegister")
redirectRegister.addEventListener("click", () => {
    const cartString = JSON.stringify(cart)
    localStorage.setItem("shoppingCart", cartString)
    window.location.href = "./register.html"
})

const redirectLogin = document.querySelector("#redirectLogin")
redirectLogin.addEventListener("click", () => {
    const cartString = JSON.stringify(cart)
    localStorage.setItem("shoppingCart", cartString)
    window.location.href = "./login.html"

})

GetAudios()

function GetAudios() {
    fetch("http://localhost:5276/api/Songs")
        .then(e => e.json())
        .then(data => {
            const list = data.map(item => {
                return {
                    id: item.id,
                    title: item.title,
                    file: item.storageData,
                    cover: item.cover,
                    composerId: item.composerId,
                    artistId: item.artistId
                }
            })
            playList(list)
        })
        .catch(error => {
            alert(error)
        })
}




function playList(list) {

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

        const buttonBuy = document.createElement("button")
        buttonBuy.classList.add("buttonBuy")
        const cartIcon = document.createElement("img")
        cartIcon.classList.add("cart")
        cartIcon.src = ".././Images/cart.png"

        const controlersContainer = document.createElement("div")
        controlersContainer.classList.add("controlersContainer")
        const playButton = document.createElement("button")
        playButton.classList.add("buttonBuy")
        playButton.innerText = "Play"

        card.appendChild(cardContainer)
        cardContainer.appendChild(cover)
        cardContainer.appendChild(audioDescription)

        audioDescription.appendChild(cardContent)
        cardContent.appendChild(title)
        buttonBuy.appendChild(cartIcon)

        audioDescription.appendChild(controlersContainer)
        controlersContainer.append(playButton)
        controlersContainer.appendChild(buttonBuy)


        playButton.addEventListener("click", (() => {
            const audio = document.querySelector('audio')
            audio.src = item.file
            audio.play()

        }))


        buttonBuy.addEventListener("click", (() => { AddToCart(item) }))
    })
}

function AddToCart(item) {
    let canBuy = true

    for (var i = 0; i < cart.length; i++) {

        if (item.title === cart[i].title) {
            alert("Item already added!")
            canBuy = false
        }
    }
    if (canBuy === true) {
        cart.push(item)
        cartNumber.innerHTML = cart.length
    }

}











