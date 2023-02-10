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
    public partial class RaspolojForm : Form
    {
        public RaspolojForm()
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

        static public int flag2 = 0;
        private void RaspolojForm_Load(object sender, EventArgs e)
        {
            try
            {
                flag2 = 0;

                VidTexniki.ViewCombobox();
                comboBox2.DataSource = VidTexniki.vidCombobox;
                comboBox2.DisplayMember = "name_vid";
                comboBox2.ValueMember = "idVid";
                comboBox2.SelectedIndex = -1;

                if (Raspoloj.View("") == false)
                    this.Close();
                dataGridView1.DataSource = Raspoloj.raspol;
            }
            catch
            {
                this.Close();
            }
        }

        static public string nomer = "";
        static public string idTex = "";
        static public string idVid = "";
        static public string nameT = "";
        static public string kab = "";
        static public string fio = "";
        static public string dataYstan = "";
        static public string dataYdal = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            nomer = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            idVid = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            idTex = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            nameT = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            kab = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            fio = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            dataYstan = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            dataYdal = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            if (dataYdal != "")
            {
                button5.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                button5.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nomer == "")
            {
                MessageBox.Show("Выберите расположение");
                return;
            }
            DialogResult otv = MessageBox.Show("Точно хотите удалить?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(otv == DialogResult.Yes)
            {
                Raspoloj.Delete(nomer);
                if(comboBox1.Text == "")
                {
                    Raspoloj.View("");
                }
                else
                {
                    Raspoloj.View(comboBox1.SelectedValue.ToString());
                }
                nomer = "";
            }
        }

        static public int flag = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            flag = 0;
            AddEditRaspoloj add = new AddEditRaspoloj();
            add.ShowDialog();
            if (comboBox1.Text == "")
            {
                Raspoloj.View("");
            }
            else
            {
                Raspoloj.View(comboBox1.SelectedValue.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {          
            if(nomer == "")
            {
                MessageBox.Show("Выберите расположение");
                return;
            }
            flag = 1;
            AddEditRaspoloj edit = new AddEditRaspoloj();
            edit.ShowDialog();
            if (comboBox1.Text == "")
            {
                Raspoloj.View("");
            }
            else
            {
                Raspoloj.View(comboBox1.SelectedValue.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == -1)
            {
                return;
            }
            flag2 = 1;
            Raspoloj.View(comboBox1.SelectedValue.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag2 = 0;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            Raspoloj.View("");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == -1)
            {
                return;
            }
            Raspoloj.ViewVid(comboBox2.SelectedValue.ToString());
            Texniks.ViewComboboxRaspoloj(comboBox2.SelectedValue.ToString());
            comboBox1.DataSource = Texniks.comboboxRaspoloj;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "idTex";
            comboBox1.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (nomer == "")
            {
                MessageBox.Show("Выберите расположение");
                return;
            }
            DialogResult otv = MessageBox.Show("Точно хотите отправить технику на склад?", "Отправка",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (otv == DialogResult.Yes)
            {
                Raspoloj.NaSklad(nomer, DateTime.Now.ToString("yyyy-MM-dd"));
                if (comboBox1.Text == "")
                {
                    Raspoloj.View("");
                }
                else
                {
                    Raspoloj.View(comboBox1.SelectedValue.ToString());
                }
                nomer = "";
            }
        }
    }
}
