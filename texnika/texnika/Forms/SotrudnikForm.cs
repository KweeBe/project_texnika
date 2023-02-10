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
    public partial class SotrudnikForm : Form
    {
        public SotrudnikForm()
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
        private void SotrudnikForm_Load(object sender, EventArgs e)
        {
            if (Sotrudniks.View(textBox1.Text, comboBox1.Text) == false)
                this.Close();
            dataGridView1.DataSource = Sotrudniks.sotrud;
            comboBox1.SelectedIndex = 0;
        }

        static public string id = "";
        static public string fio = "";
        static public string dolj = "";
        static public string tel = "";
        static public string adres = "";
        static public string stat = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            fio = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            dolj = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            tel = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            adres = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            stat = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }
            if (stat == "Работает")
            {
                DialogResult otv = MessageBox.Show("Точно хотите уволить сотрудника с работы?", "Увольнение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (otv == DialogResult.Yes)
                {
                    Sotrudniks.Yvolnenie(id);
                    Raspoloj.DataYdaleniyaYvolSotr(id, DateTime.Now.ToString("yyyy-MM-dd"));
                    Sotrudniks.View(textBox1.Text, comboBox1.Text);
                    id = "";
                }
            }
            else
            {
                DialogResult otv = MessageBox.Show("Точно хотите вернуть сотрудника на работу?", "Возвращение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (otv == DialogResult.Yes)
                {
                    Sotrudniks.Vozvrat(id);
                    Sotrudniks.View(textBox1.Text, comboBox1.Text);
                    id = "";
                }
            }
        }

        static public int flag = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }
            flag = 1;
            AddEditSotrudnik edit = new AddEditSotrudnik();
            edit.ShowDialog();
            Sotrudniks.View(textBox1.Text, comboBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = 0;
            AddEditSotrudnik add = new AddEditSotrudnik();
            add.ShowDialog();
            Sotrudniks.View(textBox1.Text, comboBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Sotrudniks.View(textBox1.Text, comboBox1.Text) == false)
                this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Sotrudniks.View(textBox1.Text, comboBox1.Text) == false)
                this.Close();
        }
    }
}
