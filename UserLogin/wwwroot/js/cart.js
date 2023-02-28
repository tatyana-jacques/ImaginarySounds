const download = new Blob([item.file], { tipe: "audio/mpeg" })
const href = URL.createObjectURL(download)
const downloadButton = Object.assign(document.createElement("a"), {
    href, style: "display:none",
    download: "myMusic.mp3"
})
downloadButton.classList.add("buttonMusic")
downloadButton.innerText = "Download"

downloadButton.click();
URL.revokeObjectURL(href)
