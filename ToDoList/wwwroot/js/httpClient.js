function getToDoListItems() {
    let request = {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    };

    return fetch(`${baseUrl}/api/ToDoListItem/Get`, request)
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
        body: JSON.stringify(item)
    };

    return fetch(`${baseUrl}/api/ToDoListItem/Create`, request)
        .then(response => response.json())
        .then(response => response.toDoListItem)
        .catch(error => console.log(error.message));
}

 function updateToDoListItem(item) {
    let request = {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(item)
    }

    return fetch(`${baseUrl}/api/ToDoListItem/Update/${item.id}`, request)
        .catch(error => console.log(error.message));
}

