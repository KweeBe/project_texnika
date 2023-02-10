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
    public partial class SpisanieForm : Form
    {
        public SpisanieForm()
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
        private void SpisanieForm_Load(object sender, EventArgs e)
        {
            if (AktSpisaniya.View() == false)
                this.Close();
            dataGridView1.DataSource = AktSpisaniya.akt;
            label4.Text = "Всего списаной техники: ";
            label4.Text += TexnikaInAkt.KolvoSpisanTexniki();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите акт");
                return;
            }
            DialogResult otv = MessageBox.Show("Точно хотите удалить?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(otv == DialogResult.Yes)
            {
                AktSpisaniya.Delete(id);
                AktSpisaniya.View();
                id = "";
            }
        }

        static public string id = "";
        static public string data = "";

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            data = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            if (e.ColumnIndex == 0)
            {
                if(saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != null)
                {
                    Documents.AktSpisaniya(id, data, saveFileDialog1.FileName);
                }
            }
            nomer = "";
            TexnikaInAkt.View(id);
            dataGridView2.DataSource = TexnikaInAkt.tex;
            if (AktSpisaniya.Statys(id, DateTime.Now.ToString("yyyy-MM-dd")) == false)
            {
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
            label3.Text = "Списано техники: ";
            label3.Text += dataGridView2.RowCount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AktSpisaniya.Add(DateTime.Now.ToString("yyyy-MM-dd"));
            AktSpisaniya.View();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(nomer == "")
            {
                MessageBox.Show("Выберите технику");
                return;
            }
            EditPrichina edit = new EditPrichina();
            edit.ShowDialog();
            TexnikaInAkt.View(id);
        }

        static public string nomer = "";
        static public string idT = "";
        static public string name = "";
        static public string prichina = "";

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                return;
            }
            nomer = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            idT = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            name = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            prichina = dataGridView2.CurrentRow.Cells[3].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите акт");
                return;
            }
            AddEditTexnikInAkt add = new AddEditTexnikInAkt();
            add.ShowDialog();
            TexnikaInAkt.View(id);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nomer == "")
            {
                MessageBox.Show("Выберите технику");
                return;
            }
            DialogResult otv = MessageBox.Show("Точно хотите удалить?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (otv == DialogResult.Yes)
            {
                TexnikaInAkt.Delete(nomer);
                Texniks.Spisanie(idT, "Нет");
                Raspoloj.DataYdaleniyaIsNull(idT, Convert.ToDateTime(data).ToString("yyyy-MM-dd"));
                TexnikaInAkt.View(id);
                nomer = "";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value.Date > dateTimePicker2.Value.Date)
            {
                MessageBox.Show("Дата от не может привышать дату до");
                return;             
            }
            AktSpisaniya.View(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            TexnikaInAkt.View("");
            label4.Text = "Всего списаной техники: ";
            label4.Text += TexnikaInAkt.KolvoSpisanTexniki(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            label3.Text = "Списано техники: ";
            label3.Text += dataGridView2.RowCount.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AktSpisaniya.View();
            TexnikaInAkt.View("");
            label4.Text = "Всего списаной техники: ";
            label4.Text += TexnikaInAkt.KolvoSpisanTexniki();
            label3.Text = "Списано техники: ";
            label3.Text += dataGridView2.RowCount.ToString();
        }
    }
}
