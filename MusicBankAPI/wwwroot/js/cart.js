const downloadArea = document.querySelector(".downloadArea")
const cart = JSON.parse(localStorage.getItem("cart")) ?? []
const card = document.querySelector(".card")

shopping(cart)

function shopping(list) {
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


        const controlersContainer = document.createElement("div")
        controlersContainer.classList.add("controlersContainer")
        const playButton = document.createElement("button")
        playButton.classList.add("buttonControlers")
        playButton.innerText = "Play"
        const stopButton = document.createElement("button")
        stopButton.classList.add("buttonControlers")
        stopButton.innerText = "Stop"

        card.appendChild(cardContainer)
        cardContainer.appendChild(cover)
        cardContainer.appendChild(audioDescription)

        audioDescription.appendChild(cardContent)
        cardContent.appendChild(title)
        cardContent.appendChild(buttonRemove)


        audioDescription.appendChild(controlersContainer)
        controlersContainer.append(playButton)
        controlersContainer.append(stopButton)

        playButton.addEventListener("click", (() => {
            const audio = document.querySelector('audio')
            audio.src = item.file
            audio.play()

        }))
        stopButton.addEventListener("click", (() => {
            const audio = document.querySelector('audio')
            audio.src = item.file
            audio.stop()

        }))

        buttonRemove.addEventListener("click", (() => { cart.push(item) }))

    })





}

