import cart from "./shoppingcart.js"
const downloadArea = document.querySelector(".downloadArea")
const car = [{
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
}];

shopping(car)

        function shopping(list) {

            list.forEach((item) => {
                const link = document.createElement("div")
                link.innerHTML = `<a class="link" href = ${item.file} download > Download</a >`

               downloadArea.appendChild(link)

            })

        }

