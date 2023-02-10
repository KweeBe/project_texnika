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
    public partial class AddEditMeropriyatiya : Form
    {
        public AddEditMeropriyatiya()
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

        private void AddEditMeropriyatiya_Load(object sender, EventArgs e)
        {
            if (MeropriyatiyaForms.flag == 1)
            {
                this.Text = "Изменение";
                textBox1.Text = MeropriyatiyaForms.name;
                dateTimePicker1.Value = Convert.ToDateTime(MeropriyatiyaForms.date);
                maskedTextBox1.Text = MeropriyatiyaForms.vremya;
                textBox2.Text = MeropriyatiyaForms.kabinet;
            }
            else
                maskedTextBox1.Text = "00:00";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" )
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            string time = maskedTextBox1.Text;
            if ((int.TryParse(time.Substring(0,2), out int x) &&  x > 23) 
                || (int.TryParse(time.Substring(0, 2), out int b) && x < 0))
            {
                MessageBox.Show("Время введено  не верно!");
                return;
            }
            if (Convert.ToInt32(time.Substring(3, 2)) > 59 || Convert.ToInt32(time.Substring(3, 2)) < 0)
            {
                MessageBox.Show("Время введено  не верно!");
                return;
            }
            if (x == 0 && b == 0)
            {
                MessageBox.Show("Время введено  не верно!");
                return;
            }

            if (MeropriyatiyaForms.flag == 1)
            {
                if (Meropriyatiya.Edit(MeropriyatiyaForms.id,textBox1.Text, 
                    dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                    maskedTextBox1.Text, textBox2.Text) == true)
                    this.Close();
            }
            else
            {
                if (Meropriyatiya.Add(textBox1.Text, dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                    maskedTextBox1.Text, textBox2.Text) == true)
                    this.Close();
            }
        }
    }
}
