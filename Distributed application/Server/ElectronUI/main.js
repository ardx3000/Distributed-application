const { app, BrowserWindow, ipcMain } = require('electron');
const path = require('path');
const net = require('net');

let mainWindow;

function createWindow() {
    mainWindow = new BrowserWindow({
        width: 800,
        height: 600,
        webPreferences: {
            preload: path.join(__dirname, 'preload.js'),
            contextIsolation: true,
            enableRemoteModule: false,
        },
    });

    mainWindow.loadFile('index.html');
}

app.on('ready', () => {
    createWindow();
    setTimeout(startNamedPipeClient, 5000); // 5 seconds delay
});

app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') {
        app.quit();
    }
});

app.on('activate', () => {
    if (BrowserWindow.getAllWindows().length === 0) {
        createWindow();
    }
});

function startNamedPipeClient() {
    const client = net.connect({ path: '\\\\.\\pipe\\myPipe' }, () => {
        console.log('Connected to the named pipe');
        client.id = 'some-id';
        client.write('Hello from Electron!'); // Send initial message to .NET Core
    });

    client.on('data', (data) => {
        console.log('Received data from named pipe:', data.toString());
        mainWindow.webContents.send('named-pipe-data', data.toString());
    });

    client.on('end', () => {
        console.log('Disconnected from named pipe');
    });

    client.on('error', (error) => {
        console.error('Error with named pipe client:', error);
    });

    ipcMain.on('add-user', (event, user) => {
        const userData = JSON.stringify(user);
        client.write(userData);
    });
}
