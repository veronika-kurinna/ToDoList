getToDoListItems()
    .then(items => renderToDoList(items));

function renderToDoList(items) {
    for (let i = 0; i < items.length; i++) {
        renderItem(items[i]);
    }
}

function renderItem(item) {
    let ul = document.querySelector("#toDoListItems");
    let li = document.createElement("li");
    li.classList.add("list-group-item");
    li.innerHTML = `<input class="form-check-input me-1" type="checkbox" value="${item.id}" id="${item.id}">
                    <label>${item.name}</label>`;
    ul.append(li);
}

function addItemClickHandler() {
    let newItem = document.querySelector("#inputAddItem").value;
    if (!newItem) {
        throw new Error("Item is required");
    }

    createToDoListItem(newItem)
        .then(item => renderItem(item));

    document.querySelector("#inputAddItem").value = "";
}
