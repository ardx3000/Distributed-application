const { ipcRenderer } = require('electron');

window.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('user-form');

    form.addEventListener('submit', (event) => {
        event.preventDefault();

        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;
        const role = document.getElementById('role').value;

        ipcRenderer.send('add-user', { username, password, role });
    });
});

// Event handler for messages received from named pipe (from .NET Core)
ipcRenderer.on('named-pipe-data', (event, data) => {
    console.log('Received data from named pipe:', data);
    document.getElementById('output').innerText = data;
});

// Event handler for messages received from main process (IPC)
ipcRenderer.on('dotnet-data', (event, data) => {
    console.log('Received data from .NET Core process:', data);
    document.getElementById('output').innerText = data;
});

// Example: Sending a message to the main process
function sendMessageToMainProcess(message) {
    ipcRenderer.send('some-event-from-renderer', message);
}

// Example: Handling button click event in UI
document.getElementById('button-id').addEventListener('click', () => {
    // Perform actions or send messages to main process
    sendMessageToMainProcess('Button clicked!');
});
