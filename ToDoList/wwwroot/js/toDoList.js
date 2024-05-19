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

    li.classList.add("list-group-item", item.id);
    li.innerHTML = buildInnerHtml(item);
    ul.append(li);
}

function buildInnerHtml(item) {
    let editableItem = `<div class="toDoListItem ${item.id}">
                            <div class="editableItem">
                                <div class="input-group">
                                    <input type="text" class="form-control" id="editItemInput" value="${item.name}">
                                    <button class="btn btn-secondary" type="button" onclick='cancelEditClickHandler(${item.id})'>Cancel</button>
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
                                        <button class="btn btn-outline-secondary" type="button" onclick='editNameClickHandler(${item.id})'>
	                                        <i class="bi bi-pencil-fill"></i>
                                        </button>
                                        <button class="btn btn-outline-secondary" type="button" onclick='toggleStatusArchivedClickHandler(${item.id})'>
                                            <i class="bi-${item.id} ${item.status == statusArchived ? 'bi-arrow-down-square-fill' : 'bi-arrow-up-square-fill'}"></i>
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
        updateToDoListItem(item)
                .then(() => {
                    removeItem(item.id);
                    renderItem(item);
                })
                .catch(error => console.log(error.message));
    }
    else 
    {
        item.status = statusDone;
        updateToDoListItem(item)
                .then(() => {
                    removeItem(item.id);
                    renderItem(item);
                })
                .catch(error => console.log(error.message));
    }
}

function toggleStatusArchivedClickHandler(id) {
    let item = toDoListItems.find(e => e.id == id);

    if (item.status == statusArchived) {
        item.status = statusToDo;
        updateToDoListItem(item)
            .then(() => {
                removeItem(item.id);
                renderItem(item);
            })
            .catch(error => console.log(error.message));
    }
    else
    {
        item.status = statusArchived;
        updateToDoListItem(item)
            .then(() => {
                removeItem(item.id);
                renderItem(item);
            })
            .catch(error => console.log(error.message));
    }
} 

function editNameClickHandler(id) {
    let item = toDoListItems.find(e => e.id == id);
    let editedItem = toDoListItems.find(e => e.IsEditable == true);
    if (editedItem !== undefined) {
        removeItem(editedItem.id);
        editedItem.IsEditable = false;
        renderItem(editedItem);
    }
    removeItem(item.id);
    item.IsEditable = true;
    renderItem(item);
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
        .then(() => {
            removeItem(item.id);
            renderItem(item);
        })
        .catch(error => console.log(error.message));
}

function cancelEditClickHandler(id) {
    let item = toDoListItems.find(e => e.id == id);
    removeItem(item.id);
    item.IsEditable = false;
    renderItem(item);
}

function removeItem(id) {
    let item = document.getElementsByClassName(`toDoListItem ${id}`)[0];
    item.remove();
}

function renderItem(item) {
    let li = document.getElementsByClassName(`list-group-item ${item.id}`)[0];
    li.innerHTML = buildInnerHtml(item);
    li.append();
}