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
    public partial class AddEditDogovor : Form
    {
        public AddEditDogovor()
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

        private void AddEditDogovor_Load(object sender, EventArgs e)
        {
            if (DogovorForm.flag == 1)
            {
                this.Text = "Изменение";
                dateTimePicker1.Value = Convert.ToDateTime(DogovorForm.dataZ);
                dateTimePicker2.Value = Convert.ToDateTime(DogovorForm.dataN);
                dateTimePicker3.Value = Convert.ToDateTime(DogovorForm.dataO);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("Дата начала не может быть раньше даты заключения");
                return;
            }
            if (dateTimePicker2.Value > dateTimePicker3.Value)
            {
                MessageBox.Show("Дата начала не может быть позже даты окончания");
                return;
            }

            if (DogovorForm.flag == 1)
            {
                if (Dogovors.Edit(DogovorForm.id, 
                    dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                    dateTimePicker2.Value.ToString("yyyy-MM-dd"),
                    dateTimePicker3.Value.ToString("yyyy-MM-dd"), 
                    PostavshikForm.id) == true)
                    this.Close();
            }
            else
            {
                if (Dogovors.Add(PostavshikForm.id,dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                    dateTimePicker2.Value.ToString("yyyy-MM-dd"),
                    dateTimePicker3.Value.ToString("yyyy-MM-dd")) == true)
                    this.Close();
            }
        }
    }
}
