getToDoListItems()
    .then(items => renderToDoList(items));

function renderToDoList(items) {
    for (let i = 0; i < items.length; i++) {
        renderItem(items[i]);
    }
}

function renderItem(item) {
    let ul = document.querySelector("#toDoList");
    let li = document.createElement("li");
    li.classList.add("list-group-item");
    li.innerHTML = `<input class="form-check-input me-1" type="checkbox" id="${item.id}" onclick='toggleStatus(${JSON.stringify(item)})'>
                    <label class="label-${item.id}">${item.name}</label>`;
    ul.append(li);
    renderIsStatusDone(item);
}

function addItemClickHandler() {
    let input = document.querySelector("#addItemInput");
    let nameItem = input.value;
    if (!nameItem) {
        throw new Error("Item is required");
    }

    let newItem = { name: nameItem };
    createToDoListItem(newItem)
        .then(item => renderItem(item));

    input.value = "";
}

function renderIsStatusDone(item) {
    let label = document.querySelector(".label-" + item.id);
    let checkbox = document.getElementById(item.id);
    let statusDone = 1;
    if (item.status == statusDone) {
        label.classList.add("strikethrough");
        checkbox.checked = true;
    }
}

function toggleStatus(item) {
    let checkbox = document.getElementById(item.id).checked;
    let label = document.querySelector(".label-" + item.id);
    let updatedItem;

    if (checkbox) {
        label.classList.add("strikethrough");
        let statusDone = 1;
        updatedItem = { id: item.id, name: item.name, status: statusDone }
        updateToDoListItem(updatedItem);
    }
    else {
        label.classList.remove("strikethrough");
        let statusToDo = 0;
        updatedItem = { id: item.id, name: item.name, status: statusToDo }
        updateToDoListItem(updatedItem);
    }
}