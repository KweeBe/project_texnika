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
    public partial class AddEditSotrudnik : Form
    {
        public AddEditSotrudnik()
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
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEditSotrudnik_Load(object sender, EventArgs e)
        {
            if (SotrudnikForm.flag == 1)
            {
                textBox1.Text = SotrudnikForm.fio;
                textBox2.Text = SotrudnikForm.dolj;
                textBox3.Text = SotrudnikForm.adres;
                maskedTextBox1.Text = SotrudnikForm.tel;
                this.Text = "Изменение";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            if (SotrudnikForm.flag == 1)
            {
                if(Sotrudniks.Edit(SotrudnikForm.id, textBox1.Text, 
                    textBox2.Text, maskedTextBox1.Text, textBox3.Text, SotrudnikForm.tel))
                    this.Close();
            }
            else
            {
                if (Sotrudniks.Add(textBox1.Text, textBox2.Text,
                    maskedTextBox1.Text, textBox3.Text))
                    this.Close();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar < 'A' || e.KeyChar > 'я') && (e.KeyChar != 8) && (e.KeyChar != 32) && (e.KeyChar != 'Ё') && (e.KeyChar != 'ё'))
            {
                e.Handled = true;
            }
        }
    }
}
