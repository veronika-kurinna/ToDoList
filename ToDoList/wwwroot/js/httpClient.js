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

