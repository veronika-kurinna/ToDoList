const urlGet = "http://localhost:5226/api/ToDoListItem/Get";
const urlCreate = "http://localhost:5226/api/ToDoListItem/Create";

function getToDoListItems() {
    let request = {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    };

    return fetch(urlGet, request)
        .then(response => response.json())
        .then(response => response.toDoListItems)
        .catch(error => console.log(error.message));
}

function createToDoListItem(item) {
    let request = {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name: item
        })
    };

    return fetch(urlCreate, request)
        .then(response => response.json())
        .then(response => response.toDoListItem)
        .catch(error => console.log(error.message));
}

