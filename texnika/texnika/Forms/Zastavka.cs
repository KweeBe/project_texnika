using System;
using System.Windows.Forms;

namespace texnika
{
    public partial class Zastavka : Form
    {
        public Zastavka()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings setting = new Settings();
            setting.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
