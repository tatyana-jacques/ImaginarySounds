const card = document.querySelector(".card");


var audios = [
    {
        title: "Meditative Theme",
        cover: "./Images/cover.png",
        file: "./Music/anjos.mp3"
    },
    {
        title: "Piano Theme",
        cover: "./Images/cover.png",
        file: "./Music/aurora.mp3"
    },
    {
        title: "Vintage Electronic Rockabilly Theme",
        cover: "./Images/cover.png",
        file: "./Music/crazyRace.mp3"
    },
];

playList(audios)


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

        const buttonMusic = document.createElement("button")
        buttonMusic.classList.add("buttonMusic")
        const cartIcon = document.createElement("img")
        cartIcon.classList.add("cart")
        cartIcon.src = "./Images/cart.png"

        const controlersContainer = document.createElement("div")
        controlersContainer.classList.add("controlersContainer")
        const playButton = document.createElement("button")
        playButton.classList.add("buttonMusic")
        playButton.innerText = "Play"
        const stopButton = document.createElement("button")
        stopButton.classList.add("buttonMusic")
        stopButton.innerText = "Stop"

        card.appendChild(cardContainer)
        cardContainer.appendChild(cover)
        cardContainer.appendChild(audioDescription)

        audioDescription.appendChild(cardContent)
        cardContent.appendChild(title)
        cardContent.appendChild(buttonMusic)
        buttonMusic.appendChild(cartIcon)

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

    })

}













