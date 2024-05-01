namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "Hello, this is an example text displayed in a RichTextBox!";
        }
    }
}
