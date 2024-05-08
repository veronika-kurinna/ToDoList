let toDoListItems = [];

getToDoListItems()
    .then(items => {
        toDoListItems = items;
        renderToDoList(items);
    });

function renderToDoList(items) {
    for (let i = 0; i < items.length; i++) {
        renderItem(items[i]);
    }
}

function renderItem(item) {
    let ul = document.querySelector("#toDoList");
    let li = document.createElement("li");
    let isStatusDone = item.status == statusDone;

    li.classList.add("list-group-item");
    li.innerHTML = `<input class="form-check-input me-1" type="checkbox" id="${item.id}" onclick='toggleStatusClickHandler(${item.id})' ${isStatusDone ? 'checked' : ''}>
                    <label class="label-${item.id} ${isStatusDone ? 'strikethrough' : ''}">${item.name}</label>`;
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
        .then(item => {
            toDoListItems.push(item);
            renderItem(item);
        });

    input.value = "";
}

function toggleStatusClickHandler(id) {
    let item = toDoListItems.find(e => e.id == id);
    let label = document.querySelector(".label-" + item.id);

    if (item.status == statusToDo) {
        label.classList.add("strikethrough");
        item.status = statusDone;
        updateToDoListItem(item);
    }
    else
    {
        label.classList.remove("strikethrough");
        item.status = statusToDo;
        updateToDoListItem(item);
    }
}