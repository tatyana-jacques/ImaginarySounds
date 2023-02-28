
const teste = document.querySelector("header")



const data = {
    title: "Meditative Theme",
    artist: "Tatyana Jacques",
    cover: "./Images/cover.png",
    file: "./Music/anjos.wav"
}

const player = {
    cover: document.querySelector("#cover"),
    title: document.querySelector(".cardContent h5"),
    artist: document.querySelector(".artist"),
    audio: document.querySelector("audio"),
}

player.cover.src = data.cover;
player.audio.src = "./Music/anjos.mp3"
player.title.innerText = data.title;

window.audios = [
    {
        title: "Meditative Theme",
        artist: "Tatyana Jacques",
        cover: "./Images/album.png",
        file: "./Music/anjos.wav"
    },
    {
        title: "Piano Theme",
        artist: "Tatyana Jacques",
        cover: "./Images/album.png",
        file: "./Music/aurora.wav"
    },
    {
        title: "Vintage Electronic Rockabilly Theme",
        artist: "Tatyana Jacques",
        cover: "./Images/album.png",
        file: "./Music/crazyRace.wav"
    },
];