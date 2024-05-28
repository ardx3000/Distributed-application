using Client.Connection;

namespace Client
{
    public partial class Form1 : Form
    {
        private SocketClient _socketClient;

        public Form1(SocketClient socketClient)
        {
            InitializeComponent();
            _socketClient = socketClient;

            _socketClient.DataReceived += _socketClient_DataReceived;
        }

        private void _socketClient_DataReceived(object sender, string data)
        {
            UpdateTextBox(data);
        }

        private void UpdateTextBox(string message)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke((Action)(() =>
                {
                    textBox1.AppendText(message + Environment.NewLine);
                }));
            }
            else
            {
                textBox1.AppendText(message + Environment.NewLine);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Send some data to the server for testing
            _socketClient.Send("Hello from the client!!! ");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
