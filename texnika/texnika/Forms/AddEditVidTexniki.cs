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
    public partial class AddEditVidTexniki : Form
    {
        public AddEditVidTexniki()
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

        private void AddEditVidTexniki_Load(object sender, EventArgs e)
        {
            if (VidTexnikiForm.flag == 1)
            {
                this.Text = "Изменение";
                textBox1.Text = VidTexnikiForm.name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите название!");
                return;
            }
            if (VidTexnikiForm.flag == 1)
            {
                if (VidTexniki.Edit(VidTexnikiForm.id, textBox1.Text, VidTexnikiForm.name) == true)
                    this.Close();
            }
            else
            {
                if (VidTexniki.Add(textBox1.Text) == true)
                    this.Close();
            }
        }
    }
}
