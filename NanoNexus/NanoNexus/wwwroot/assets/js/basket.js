let addTobBaskerBtns = document.querySelectorAll(".add-to-basket")


addTobBaskerBtns.forEach(btn => btn.addEventListener("click", function (e) {


    let url = btn.getAttribute("href");

    e.preventDefault();

    fetch(url)
        .then(response => {
            if (response.status == 200) {
                location.reload()
            }
            else {
                alert("error")
            }
        })

}))

