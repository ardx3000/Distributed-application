const { app, BrowserWindow, ipcMain } = require('electron');
const path = require('path');
const { spawn } = require('child_process');

let mainWindow;

function createWindow() {
    mainWindow = new BrowserWindow({
        width: 800,
        height: 600,
        webPreferences: {
            preload: path.join(__dirname, 'preload.js'),
            nodeIntegration: true,
            contextIsolation: false,
        },
    });

    mainWindow.loadFile('index.html');
    mainWindow.on('closed', function () {
        mainWindow = null;
    });
}

app.on('ready', createWindow);

app.on('window-all-closed', function () {
    if (process.platform !== 'darwin') {
        app.quit();
    }
});

app.on('activate', function () {
    if (mainWindow === null) {
        createWindow();
    }
});

ipcMain.on('run-command', (event, arg) => {
    const dotnet = spawn('dotnet', ['run', '--project', '../Client']);

    dotnet.stdout.on('data', (data) => {
        console.log(`stdout: ${data}`);
        event.reply('command-result', data.toString());
    });

    dotnet.stderr.on('data', (data) => {
        console.error(`stderr: ${data}`);
        event.reply('command-error', data.toString());
    });

    dotnet.on('close', (code) => {
        console.log(`child process exited with code ${code}`);
    });
});
