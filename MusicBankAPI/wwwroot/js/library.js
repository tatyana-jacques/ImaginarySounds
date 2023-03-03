const downloadArea = document.querySelector(".downloadArea")
const cart = JSON.parse(localStorage.getItem("cart")) ?? []

shopping(cart)

function shopping(list) {

    list.forEach((item) => {
        const link = document.createElement("div")
        link.innerHTML = `<a class="link" href = ${item.file} download > Download</a >`

        downloadArea.appendChild(link)

    })

}