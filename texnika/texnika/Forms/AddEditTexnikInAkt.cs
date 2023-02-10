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
    public partial class AddEditTexnikInAkt : Form
    {
        public AddEditTexnikInAkt()
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

        private void AddEditTexnikInAkt_Load(object sender, EventArgs e)
        {
            VidTexniki.ViewCombobox();
            comboBox2.DataSource = VidTexniki.vidCombobox;
            comboBox2.DisplayMember = "name_vid";
            comboBox2.ValueMember = "idVid";
            comboBox2.SelectedIndex = -1;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = comboBox1.SelectedValue.ToString();
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите причину!");
                return;
            }
            if (TexnikaInAkt.Add(SpisanieForm.id, id, textBox1.Text))
            {
                Texniks.Spisanie(id, "Да");
                Raspoloj.DataYdaleniya(id, Convert.ToDateTime(SpisanieForm.data).ToString("yyyy-MM-dd"));
                this.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
                return;
            }
            Texniks.ViewComboboxPoVidy(comboBox2.SelectedValue.ToString());
            comboBox1.DataSource = Texniks.comboboxVid;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "idTex";
        }
    }
}
