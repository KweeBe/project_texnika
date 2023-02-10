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
    public partial class AddEditPostavhik : Form
    {
        public AddEditPostavhik()
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
        private void AddEdtiPostavhik_Load(object sender, EventArgs e)
        {
            if (PostavshikForm.flag == 1)
            {
                this.Text = "Изменение";
                textBox1.Text = PostavshikForm.name;
                textBox2.Text = PostavshikForm.adres;
                textBox3.Text = PostavshikForm.email;
                maskedTextBox1.Text = PostavshikForm.tel;
                textBox4.Text = PostavshikForm.lico;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" ||
                textBox3.Text == "" || textBox4.Text == "" )
            {
                MessageBox.Show("Заполните поля!");
                return;
            }

            if (Email.CheckEmail(textBox3.Text) == false)
            {
                MessageBox.Show("Емаил не правильный!");
                return;
            }

            if (PostavshikForm.flag == 1)
            {
                if (Postavshik.Edit(PostavshikForm.id, textBox1.Text, textBox2.Text,
                    textBox3.Text, maskedTextBox1.Text, textBox4.Text, PostavshikForm.tel) == true)
                    this.Close();
            }
            else
            {
                if (Postavshik.Add(textBox1.Text, textBox2.Text,
                    textBox3.Text, maskedTextBox1.Text, textBox4.Text) == true)
                    this.Close();
            }
        }
    }
}
