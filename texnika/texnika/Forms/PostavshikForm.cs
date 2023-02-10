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
    public partial class PostavshikForm : Form
    {
        public PostavshikForm()
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
        static public string adres = "";
        static public string email = "";
        static public string tel = "";
        static public string lico = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            adres = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            email = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            tel = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            lico = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите поставщика");
                return;
            }
            DialogResult otv = MessageBox.Show("Точно хотите удалить?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (otv == DialogResult.Yes)
            {
                Postavshik.Delete(id);
                Postavshik.View(textBox1.Text);
                id = "";
            }
        }

        private void PostavshikForm_Load(object sender, EventArgs e)
        {
            if (Postavshik.View(textBox1.Text) == false)
                this.Close();
            dataGridView1.DataSource = Postavshik.postav;
        }

        static public int flag = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            flag = 0;
            AddEditPostavhik add = new AddEditPostavhik();
            add.ShowDialog();
            Postavshik.View(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите поставщика");
                return;
            }
            flag = 1;
            AddEditPostavhik edit = new AddEditPostavhik();
            edit.ShowDialog();
            Postavshik.View(textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите поставщика");
                return;
            }
            DogovorForm dogovorss = new DogovorForm();
            dogovorss.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Postavshik.View(textBox1.Text);
        }
    }
}
