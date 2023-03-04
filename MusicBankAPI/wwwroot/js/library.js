let uId = JSON.parse(localStorage.getItem("uId")) ?? 0

const card = document.querySelector(".card")

const stopAudio = document.querySelector("#stop")
stopAudio.addEventListener("click", (() => {
    const audio = document.querySelector('audio')
    audio.pause()

}))

const home = document.querySelector("#home")
home.addEventListener("click", () => {
    window.location.href = "./index.html"

})

getAudios()


function getAudios() {
    fetch("http://localhost:5276/api/UserSongs/GetUserSongs?userId=uId")
        .then(e => e.json())
        .then(data => {
            const list = data.map(item => {
                return {
                    id: item.id,
                    songId: item.song.id,
                    title: item.song.title,
                    file: item.song.storageData,
                    cover: item.song.cover
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

        const controlersContainer = document.createElement("div")
        controlersContainer.classList.add("downloadContainer")
        const playButton = document.createElement("button")
        playButton.classList.add("buttonBuy")
        playButton.innerText = "Play"

        const link = document.createElement("div")
        link.innerHTML = `<a class="link" href = ${item.file} download > Download</a >`

        card.appendChild(cardContainer)
        cardContainer.appendChild(cover)
        cardContainer.appendChild(audioDescription)

        audioDescription.appendChild(cardContent)
        cardContent.appendChild(title)


        audioDescription.appendChild(controlersContainer)
        controlersContainer.append(playButton)
        controlersContainer.appendChild(link)


        playButton.addEventListener("click", (() => {
            const audio = document.querySelector('audio')
            audio.src = item.file
            audio.play()
        }))


    })

}