let toDoListItems = [];

getToDoListItems()
    .then(items => {
        toDoListItems = items;
        renderToDoList(items);
        addPropertyToArray(toDoListItems);
    });

function addPropertyToArray(array) {
    array.forEach(function (item) {
        item.IsEditable = "false";
    });
}

function renderToDoList(items) {
    for (let i = 0; i < items.length; i++) {
        renderListGroupItem(items[i]);
    }
}

function renderListGroupItem(item) {
    let ul = document.querySelector("#toDoList");
    let li = document.createElement("li");

    li.classList.add("list-group-item");
    li.classList.add(item.id);
    li.innerHTML = renderItem(item);
    ul.append(li);
}

function renderItem(item) {
    return `<div class="toDoListItem ${item.id}">
                <div>
                    <input class="form-check-input me-1" type="checkbox" onclick='toggleStatusClickHandler(${item.id})' ${item.status == statusDone ? 'checked' : ''}>
                    <label class="label-${item.id} ${item.status == statusDone ? 'strikethrough' : ''}">${item.name}</label>
                </div>
                <div class="btn-group me-1">
                    <button class="btn btn-outline-secondary" type="button" onclick='editNameClickHandler(${item.id})'>
	                    <i class="bi bi-pencil-fill"></i>
                    </button>
                    <button class="btn btn-outline-secondary" type="button" onclick='toggleStatusArchivedClickHandler(${item.id})'>
                        <i class="bi-${item.id} ${item.status == statusArchived ? 'bi-arrow-down-square-fill' : 'bi-arrow-up-square-fill'}"></i>
                    </button>
                </div>
            </div>`;
}

function removeInputGroup(id) {
    let input = document.getElementsByClassName(`input-group ${id}`)[0];
    input.remove();
}

function appendItem(item) {
    let li = document.getElementsByClassName(`list-group-item ${item.id}`)[0];
    li.innerHTML = renderItem(item);
    li.append();
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
            renderListGroupItem(item);
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

function editNameClickHandler(id) {
    let item = toDoListItems.find(e => e.id == id);
    let editedItem = toDoListItems.find(e => e.IsEditable == true);
    if (editedItem !== undefined) {
        removeInputGroup(editedItem.id);
        editedItem.IsEditable = false;

        appendItem(editedItem);
    }

    let classToDoListItem = document.getElementsByClassName(`toDoListItem ${item.id}`)[0];
    classToDoListItem.remove();

    let li = document.getElementsByClassName(`list-group-item ${item.id}`)[0];
    let div = document.createElement("div");
    div.classList.add("input-group");
    div.classList.add(item.id);
    div.innerHTML = `<input type="text" class="form-control" id="editItemInput" value="${item.name}">
                     <button class="btn btn-secondary" type="button" onclick='cancelEditClickHandler(${item.id})'>Cancel</button>
                     <button class="btn btn-primary" type="button" onclick='saveNameClickHandler(${item.id})'>Save</button>`;
    li.append(div);

    item.IsEditable = true;
}

function saveNameClickHandler(id) {
    let item = toDoListItems.find(e => e.id == id);
    let input = document.querySelector("#editItemInput");
    let editedName = input.value;

    if (!editedName) {
        throw new Error("Item is required");
    }

    item.name = editedName;
    updateToDoListItem(item);

    removeInputGroup(item.id);
    item.IsEditable = false;

    appendItem(item);
}

function cancelEditClickHandler(id) {
    let item = toDoListItems.find(e => e.id == id);

    removeInputGroup(item.id);
    item.IsEditable = false;

    appendItem(item);
}