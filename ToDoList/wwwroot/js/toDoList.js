getToDoListItems()
    .then(items => renderToDoList(items));

document.querySelector("#createItem").addEventListener("click", function () {
    createToDoListItem()
        .then(id => renderNewItem(id));
});

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

function createToDoListItem() {
    let url = "http://localhost:5226/api/ToDoListItem/Create";
    let toDoListItem = document.querySelector("#newItem").value;

    let request = {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name: toDoListItem
        })
    };

    return fetch(url, request)
        .then(response => response.json())
        .catch(error => console.log(error.message));
}

function renderNewItem(id) {
    let toDoListItem = document.querySelector("#newItem").value;

    let ul = document.querySelector("ul");
    let li = document.createElement("li");
    li.classList.add("list-group-item");
    li.innerHTML = `<input class="form-check-input me-1" type="checkbox" value="${id}" id="${id}">
                    <label>${toDoListItem}</label>`;
    ul.append(li);

    document.querySelector("#newItem").value = "";
}
