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

    li.classList.add("list-group-item");
    li.innerHTML = `<div class="toDoListItem">
                        <div>
                            <input class="form-check-input me-1" type="checkbox" id="${item.id}" onclick='toggleStatusClickHandler(${item.id})' ${item.status == statusDone ? 'checked' : ''}>
                            <label class="label-${item.id} ${item.status == statusDone ? 'strikethrough' : ''}">${item.name}</label>
                        </div>
                        <div class="btn-group me-1">
                            <button class="btn btn-outline-secondary" type="button" id="" onclick='toggleStatusArchivedClickHandler(${item.id})'>
                                <i class="bi-${item.id} ${item.status == statusArchived ? 'bi-arrow-down-square-fill' : 'bi-arrow-up-square-fill'}"></i>
                            </button>
                        </div>
                    </div>`;
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

function toggleStatusArchivedClickHandler(id) {
    let item = toDoListItems.find(e => e.id == id);
    let icon = document.querySelector(".bi-" + item.id);

    if (item.status == statusArchived) {
        icon.classList.remove("bi-arrow-down-square-fill");
        icon.classList.add("bi-arrow-up-square-fill");
        item.status = statusToDo;
        updateToDoListItem(item);
    }
    else
    {
        icon.classList.remove("bi-arrow-up-square-fill");
        icon.classList.add("bi-arrow-down-square-fill");
        item.status = statusArchived;
        updateToDoListItem(item);
    }
}