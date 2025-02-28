namespace ProyectoAutoPartes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonIngreso_Click(object sender, EventArgs e)
        {
            formMenu formMenu = new formMenu();
            formMenu.ShowDialog(this);
            this.Hide();
            this.Close();
        }
    }
}
