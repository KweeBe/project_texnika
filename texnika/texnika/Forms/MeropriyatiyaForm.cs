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
    public partial class MeropriyatiyaForms : Form
    {
        public MeropriyatiyaForms()
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

        static public string id = "";
        static public string name = "";
        static public string date = "";
        static public string vremya = "";
        static public string kabinet = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            date = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            vremya = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            kabinet = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            TexnikaInMerop.View(id);
            dataGridView2.DataSource = TexnikaInMerop.tex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите мероприятие");
                return;
            }
            DialogResult otv = MessageBox.Show("Точно хотите удалить?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (otv == DialogResult.Yes)
            {
                Meropriyatiya.Delete(id);
                Meropriyatiya.View();
                id = "";
            }
        }

        private void MeropriyatiyaForm_Load(object sender, EventArgs e)
        {
            if (Meropriyatiya.View() == false)
                this.Close();
            dataGridView1.DataSource = Meropriyatiya.merop;
        }

        static public int flag = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            flag = 0;
            AddEditMeropriyatiya add = new AddEditMeropriyatiya();
            add.ShowDialog();
            Meropriyatiya.View();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите мероприятие");
                return;
            }
            flag = 1;
            AddEditMeropriyatiya add = new AddEditMeropriyatiya();
            add.ShowDialog();
            Meropriyatiya.View();
        }

        static public string nom = "";
        static public string idT = "";
        static public string naame = "";

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                return;
            }
            nom = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            idT = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            naame = dataGridView2.CurrentRow.Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (nom == "")
            {
                MessageBox.Show("Выберите технику");
                return;
            }
            DialogResult otv = MessageBox.Show("Точно хотите удалить?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (otv == DialogResult.Yes)
            {
                TexnikaInMerop.Delete(nom);
                TexnikaInMerop.View(id);
                nom = "";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(id == "")
            {
                MessageBox.Show("Выберите мероприятие!");
                return;
            }
            AddTexInMerop add = new AddTexInMerop();
            add.ShowDialog();
            TexnikaInMerop.View(id);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Meropriyatiya.View();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            Meropriyatiya.View(e.Start.ToString("yyyy-MM-dd"));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите мероприятие");
                return;
            }
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != null)
            {
                Documents.Merop(id,name, date, vremya, kabinet, saveFileDialog1.FileName);
            }
        }
    }
}
