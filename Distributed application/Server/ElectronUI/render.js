const { ipcRenderer } = require('electron');

document.getElementById('runCommand').addEventListener('click', () => {
    ipcRenderer.send('run-command');
});

ipcRenderer.on('command-result', (event, data) => {
    document.getElementById('output').innerText += data;
});

ipcRenderer.on('command-error', (event, data) => {
    document.getElementById('output').innerText += `Error: ${data}`;
});
