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
    public partial class EditPrichina : Form
    {
        public EditPrichina()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if( textBox1.Text == "")
            {
                MessageBox.Show("Введите причину!");
                return;
            }
            if (TexnikaInAkt.Edit(SpisanieForm.nomer, textBox1.Text))
            {
                this.Close();
            }
        }

        private void EditPrichina_Load(object sender, EventArgs e)
        {
            textBox1.Text = SpisanieForm.prichina;
        }
    }
}
