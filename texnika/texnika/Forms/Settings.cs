using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace texnika
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.F1)
                {
                    SpravochSistema sprav = new SpravochSistema();
                    sprav.ShowDialog();
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch
            {
                return false;
            }
        }

        static public int flag = 0;

        private void button4_Click(object sender, EventArgs e)
        {
            flag = 0;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Введите все данные!");
                return;
            }

            DBconnect.nazvanieBD = textBox1.Text;
            DBconnect.users = textBox3.Text;
            DBconnect.pass = textBox4.Text;
            DBconnect.adress = textBox2.Text;

            if (DBconnect.Connect() == false)
            {
                DBconnect.CloseBd();
                return;
            }
            flag = 1;
            this.Close();
            Menu menu = new Menu();
            menu.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Введите все данные!");
                return;
            }
            DBconnect.nazvanieBD = textBox1.Text;
            DBconnect.users = textBox3.Text;
            DBconnect.pass = textBox4.Text;
            DBconnect.adress = textBox2.Text;

            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName != "")
            {
                ImpAndExp.CreateBD();
                ImpAndExp.Restore(openFileDialog1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Введите все данные!");
                return;
            }
            DBconnect.nazvanieBD = textBox1.Text;
            DBconnect.users = textBox3.Text;
            DBconnect.pass = textBox4.Text;
            DBconnect.adress = textBox2.Text;

            if (DBconnect.Connect())
            {
                MessageBox.Show("Успешно подключено");
                DBconnect.CloseBd();
            }
            else
            {
                DBconnect.CloseBd();
            }
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (flag != 0)
            {
                return;
            }
            Form form1 = Application.OpenForms[0];
            form1.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            flag = 0;
        }
    }
}
