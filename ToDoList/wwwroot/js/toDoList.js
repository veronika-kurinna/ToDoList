getToDoListItems()
    .then(items => renderToDoList(items));

function renderToDoList(items) {
    for (let i = 0; i < items.length; i++) {
        renderItem(items[i]);
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

    if (!toDoListItem) {
        throw new Error("Item is required");
    }

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

function renderItem(item) {
    let ul = document.querySelector("ul");
    let li = document.createElement("li");
    li.classList.add("list-group-item");
    li.innerHTML = `<input class="form-check-input me-1" type="checkbox" value="${item.id}" id="${item.id}">
                    <label>${item.name}</label>`;
    ul.append(li);
}

function addItemClickHandler() {
    createToDoListItem()
        .then(item => renderItem(item));

    document.querySelector("#newItem").value = "";
}
