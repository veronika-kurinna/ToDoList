getData();

function addListItems(data) {
    const ul = document.querySelector("ul");

    for (var i = 0; i < data.length; i++) {
        let li = document.createElement("li");
        li.classList.add("list-group-item");
        li.innerHTML = `<input class="form-check-input me-1" type="checkbox" value="${data[i].id}" id="${data[i].id}">
                        <label for="${data[i].id}">${data[i].name}</label>`;
        ul.append(li);
    }
}

function getData() {
    let url = "http://localhost:5226/api/ToDoListItem/Get";

    let promise = fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    });

    promise
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            addListItems(data.toDoListItems);
        })
    .catch(error => console.log(error.message));
}