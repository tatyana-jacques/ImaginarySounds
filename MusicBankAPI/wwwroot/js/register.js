const name = document.getElementById("name")
const email = document.getElementById("email")
const password = document.getElementById("password")

function login() {
    const inputs = {
        "id": 0,
        "name": uName.value,
        "text": text.value,
        "dataPostagem": date

    }
    fetch("http://localhost:5018/api/Posts",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(inputs),
        }).then(() => {
            updateFront()

        })
        .catch(() => alert("Error"))

}
