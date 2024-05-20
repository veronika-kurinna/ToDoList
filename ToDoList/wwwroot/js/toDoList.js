let toDoListItems = [];

getToDoListItems()
    .then(items => {
        toDoListItems = mapItems(items);
        renderToDoList(toDoListItems);
    });

function mapItems(items) {
    let itemViews = items;
    items.forEach(item => item.IsEditable = false);
    return itemViews;
}

function renderToDoList(items) {
    for (let i = 0; i < items.length; i++) {
        renderToDoListItem(items[i]);
    }
}

function renderToDoListItem(item) {
    let ul = document.querySelector("#toDoList");
    let li = document.createElement("li");

    li.innerHTML = buildInnerHtml(item);
    if (item.status == statusToDo) {
        li.classList.add("list-group-item", "list-group-item-primary", item.id);
        ul.insertBefore(li, ul.firstChild);
    } else {
        li.classList.add("list-group-item", "list-group-item-success", item.id);
        ul.append(li);
    }
}

function buildInnerHtml(item) {
    let editableItem = `<div class="toDoListItem ${item.id}">
                            <div class="editableItem">
                                <div class="input-group">
                                    <input type="text" class="form-control" id="editItemInput" value="${item.name}">
                                    <button class="btn btn-dark" type="button" onclick='cancelEditClickHandler(${item.id})'>Cancel</button>
                                    <button class="btn btn-primary" type="button" onclick='saveNameClickHandler(${item.id})'>Save</button>
                                </div>
                            </div>
                        </div>`;

    let notEditableItem = `<div class="toDoListItem ${item.id}">
                                <div class="notEditableItem">
                                    <div>
                                        <input class="form-check-input me-1" type="checkbox" onclick='toggleStatusClickHandler(${item.id})' ${item.status == statusDone ? 'checked' : ''}>
                                        <label class="label-${item.id} ${item.status == statusDone ? 'strikethrough' : ''}">${item.name}</label>
                                    </div>
                                    <div class="btn-group me-1">
                                        <button class="btn btn-outline-dark" type="button" onclick='editNameClickHandler(${item.id})'>
	                                        <i class="bi bi-pencil-fill"></i>
                                        </button>
                                        <button class="btn btn-outline-dark" type="button" onclick='deleteToDoListItemClickHandler(${item.id})'>
                                            <i class="bi bi-trash-fill"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>`;
    if (item.IsEditable) {
        return editableItem;
    } else {
        return notEditableItem;
    }
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
            renderToDoListItem(item);
        });

    input.value = "";
}

function toggleStatusClickHandler(id) {
    let item = toDoListItems.find(e => e.id == id);
    if (item.status == statusDone) {
        item.status = statusToDo;
    }
    else 
    {
        item.status = statusDone;
    }

    updateToDoListItem(item)
        .then(() => {
            let li = document.getElementsByClassName(`list-group-item ${item.id}`)[0];
            li.remove();
            renderToDoListItem(item);
        })
        .catch(error => console.log(error.message));
}

function editNameClickHandler(id) {
    let item = toDoListItems.find(e => e.id == id);
    let editedItem = toDoListItems.find(e => e.IsEditable == true);
    if (editedItem !== undefined) {
        editedItem.IsEditable = false;
        rerenderItem(editedItem);
    }
    item.IsEditable = true;
    rerenderItem(item);
}

function saveNameClickHandler(id) {
    let item = toDoListItems.find(e => e.id == id);
    let input = document.querySelector("#editItemInput");
    let editedName = input.value;

    if (!editedName) {
        throw new Error("Item is required");
    }
    item.name = editedName;
    item.IsEditable = false;
    updateToDoListItem(item)
        .then(() => rerenderItem(item))
        .catch(error => console.log(error.message));
}

function cancelEditClickHandler(id) {
    let item = toDoListItems.find(e => e.id == id);
    item.IsEditable = false;
    rerenderItem(item);
}

function rerenderItem(item) {
    let toDoListItem = document.getElementsByClassName(`toDoListItem ${item.id}`)[0];
    toDoListItem.remove();

    let li = document.getElementsByClassName(`list-group-item ${item.id}`)[0];
    li.innerHTML = buildInnerHtml(item);
    li.append();
}

function deleteToDoListItemClickHandler(id) {
    deleteToDoListItem(id)
        .then(() => {
            let li = document.getElementsByClassName(`list-group-item ${id}`)[0];
            li.remove();
            toDoListItems = toDoListItems.filter(item => item.id !== id);
        })
        .catch(error => console.log(error.message));
}
