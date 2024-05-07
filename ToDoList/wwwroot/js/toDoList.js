﻿getToDoListItems()
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
    li.innerHTML = `<input class="form-check-input me-1" type="checkbox" value="${item.id}" id="${item.id}" onclick="toggleStrikethrough(${item.id})">
                    <label class="label-${item.id}">${item.name}</label>`;
    ul.append(li);
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

function toggleStrikethrough(id) {
    let checkbox = document.getElementById(id).checked;
    let label = document.querySelector(".label-" + id);
    if (checkbox) {
        label.classList.add("strikethrough");
    }
    else {
        label.classList.remove("strikethrough");
    }
}
