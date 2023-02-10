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
    public partial class AddEditRaspoloj : Form
    {
        public AddEditRaspoloj()
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

        private void AddEditRaspoloj_Load(object sender, EventArgs e)
        {
            Sotrudniks.ViewCombobox();
            comboBox2.DataSource = Sotrudniks.combobox;
            comboBox2.DisplayMember = "fio";
            comboBox2.ValueMember = "idSotrud";

            VidTexniki.ViewCombobox();
            comboBox3.DataSource = VidTexniki.vidCombobox;
            comboBox3.DisplayMember = "name_vid";
            comboBox3.ValueMember = "idVid";

            Texniks.ViewComboboxPoVidy(comboBox3.SelectedValue.ToString());
            comboBox1.DataSource = Texniks.comboboxVid;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "idTex";



            if (RaspolojForm.flag == 1)
            {
                this.Text = "Изменение";
                comboBox3.SelectedValue = RaspolojForm.idVid;
                comboBox1.SelectedValue = RaspolojForm.idTex;
                textBox1.Text = RaspolojForm.kab;
                comboBox2.Text = RaspolojForm.fio;
                dateTimePicker1.Value = Convert.ToDateTime(RaspolojForm.dataYstan);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
       
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Дата установки не может превышать сегодняшнюю дату!");
                return;
            }

            if (RaspolojForm.flag == 1)
            {
                if (Raspoloj.Edit(RaspolojForm.nomer,comboBox1.SelectedValue.ToString(), textBox1.Text,
                    comboBox2.SelectedValue.ToString(),
                    dateTimePicker1.Value.ToString("yyyy-MM-dd")) == true)
                {
                    Raspoloj.DataYdaleniya(comboBox1.SelectedValue.ToString(), 
                        dateTimePicker1.Value.ToString("yyyy-MM-dd"), 
                        Convert.ToDateTime(RaspolojForm.dataYstan).ToString("yyyy-MM-dd"));
                    this.Close();
                }
            }
            else
            {
                if(Raspoloj.Add(comboBox1.SelectedValue.ToString(), textBox1.Text, 
                    comboBox2.SelectedValue.ToString(), 
                    dateTimePicker1.Value.ToString("yyyy-MM-dd")) == true)
                {
                    Raspoloj.DataYdaleniya(comboBox1.SelectedValue.ToString(), dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    this.Close();
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == -1)
            {
                return;
            }
            Texniks.ViewComboboxPoVidy(comboBox3.SelectedValue.ToString());
            comboBox1.DataSource = Texniks.comboboxVid;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "idTex";
        }
    }
}
