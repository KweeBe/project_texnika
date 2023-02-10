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
    public partial class PostavkaForm : Form
    {
        public PostavkaForm()
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
        private void PostavkaForm_Load(object sender, EventArgs e)
        {
            if (Postavka.View() == false)
                this.Close();
            dataGridView1.DataSource = Postavka.postav;
            label4.Text = "Всего поставлено техники: ";
            label4.Text += Texniks.KolvoPostavlenix();
        }

        static public int flag = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            flag = 0;
            AddEditPostavka add = new AddEditPostavka();
            add.ShowDialog();
            Postavka.View();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите поставку");
                return;
            }
            flag = 1;
            AddEditPostavka edit = new AddEditPostavka();
            edit.ShowDialog();
            Postavka.View();
        }

        static public string id = "";
        static public string postav = "";
        static public string name = "";
        static public string data = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            postav = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            name = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            data = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            idTex = "";
            Texniks.View(id);
            dataGridView2.DataSource = Texniks.tex;
            label3.Text = "Поставлено техники: ";
            label3.Text += dataGridView2.RowCount.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите поставку");
                return;
            }
            DialogResult otv = MessageBox.Show("Точно хотите удалить?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (otv == DialogResult.Yes)
            {
                Postavka.Delete(id);
                Postavka.View();
                id = "";
            }
        }

        static public string idTex = "";
        static public string nameTex = "";
        static public string vid = "";
        static public string cena = "";
        static public string nomerZ = "";
        static public string nomerI = "";
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                return;
            }
            idTex = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            nameTex = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            vid = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            cena = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            nomerZ = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            nomerI = dataGridView2.CurrentRow.Cells[4].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (idTex == "")
            {
                MessageBox.Show("Выберите технику");
                return;
            }
            DialogResult otv = MessageBox.Show("Точно хотите удалить?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (otv == DialogResult.Yes)
            {
                Texniks.Delete(idTex);
                Texniks.View(id);
                idTex = "";
            }
        }

        static public int flagT = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите поставку");
                return;
            }
            flagT = 0;
            AddEditTexnik add = new AddEditTexnik();
            add.ShowDialog();
            Texniks.View(id);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (idTex == "")
            {
                MessageBox.Show("Выберите технику");
                return;
            }
            flagT = 1;
            AddEditTexnik edit = new AddEditTexnik();
            edit.ShowDialog();
            Texniks.View(id);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Postavka.View();
            Texniks.View("");
            label3.Text = "Поставлено техники: ";
            label3.Text += dataGridView2.RowCount.ToString();
            label4.Text = "Всего поставлено техники: ";
            label4.Text += Texniks.KolvoPostavlenix();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date > dateTimePicker2.Value.Date)
            {
                MessageBox.Show("Дата от не может привышать дату до");
                return;
            }
            Postavka.View(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            Texniks.View("");
            label3.Text = "Поставлено техники: ";
            label3.Text += dataGridView2.RowCount.ToString();
            label4.Text = "Всего поставлено техники: ";
            label4.Text += Texniks.KolvoPostavlenix(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
        }
    }
}
