using Server.Connection;

namespace Server
{
    public partial class Form1 : Form
    {
        private Connection.SocketServer _socketServer;


        public Form1(SocketServer server)
        {
            InitializeComponent();
            _socketServer = server; // Assign the existing instance
            InitializeTextBox();
            _socketServer.DataReceived += SocketServer_DataReceived; // Subscribe t
        }
        private void InitializeTextBox()
        {
            // Initialize TextBox
            textBox1 = new TextBox();
            textBox1.Multiline = true; // Allow multiple lines
            textBox1.ScrollBars = ScrollBars.Vertical; // Add vertical scroll bar
            textBox1.Dock = DockStyle.Fill; // Dock to fill the form
            textBox1.ReadOnly = true; // Make it read-only

            Controls.Add(textBox1);
            // Add TextBox to the form's controls
            textBox1.BringToFront();
        }
        private void SocketServer_DataReceived(object sender, string data)
        {
            // Update the textBox1 control with received data
            UpdateTextBox(data);
        }
        private void UpdateTextBox(string text)
        {
            // Update textBox1 control with received text
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action<string>(UpdateTextBox), text);
            }
            else
            {
                textBox1.AppendText(text + Environment.NewLine);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _socketServer.Stop(); // Stop the server
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
