getToDoListItems()
    .then(items => renderToDoList(items));

function renderToDoList(items) {
    const ul = document.querySelector("ul");

    for (let i = 0; i < items.length; i++) {
        let li = document.createElement("li");
        li.classList.add("list-group-item");
        li.innerHTML = `<input class="form-check-input me-1" type="checkbox" value="${items[i].id}" id="${items[i].id}">
                        <label>${items[i].name}</label>`;
        ul.append(li);
    }
}

function getToDoListItems() {
    let url = "http://localhost:5226/api/ToDoListItem/Get";
    let request = {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    };

    return fetch(url, request)
        .then(response => response.json())
        .then(response => response.toDoListItems)
        .catch(error => console.log(error.message));
}