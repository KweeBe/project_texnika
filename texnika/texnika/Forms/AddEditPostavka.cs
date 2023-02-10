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
    public partial class AddEditPostavka : Form
    {
        public AddEditPostavka()
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

        private void AddEditPostavka_Load(object sender, EventArgs e)
        {
            Postavshik.ViewCombobox(DateTime.Now.ToString("yyyy-MM-dd"));
            comboBox1.DataSource = Postavshik.combobox;
            comboBox1.DisplayMember = "Name_postav";
            comboBox1.ValueMember = "idPostav";
            if(PostavkaForm.flag == 1)
            {
                this.Text = "Изменение";
                comboBox1.Text = PostavkaForm.name;
                dateTimePicker1.Value = Convert.ToDateTime(PostavkaForm.data);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PostavkaForm.flag == 1)
            {
                if (Postavka.Edit(PostavkaForm.id, comboBox1.SelectedValue.ToString(),
                    dateTimePicker1.Value.ToString("yyyy-MM-dd")) == true)
                    this.Close();
            }
            else
            {
                if (Postavka.Add(comboBox1.SelectedValue.ToString(),
                    dateTimePicker1.Value.ToString("yyyy-MM-dd")) == true)
                    this.Close();
            }
        }
    }
}
