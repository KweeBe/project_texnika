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
    public partial class DogovorForm : Form
    {
        public DogovorForm()
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
        private void DogovorForm_Load(object sender, EventArgs e)
        {
            if (Dogovors.View(PostavshikForm.id) == false)
                this.Close();
            dataGridView1.DataSource = Dogovors.dogovor;
            label1.Text = label1.Text + " " + PostavshikForm.name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите договор");
                return;
            }
            DialogResult otv = MessageBox.Show("Точно хотите удалить?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (otv == DialogResult.Yes)
            {
                Dogovors.Delete(id);
                Dogovors.View(PostavshikForm.id);
                id = "";
            }
        }

        static public string id = "";
        static public string dataZ = "";
        static public string dataN = "";
        static public string dataO = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dataZ = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            dataN = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dataO = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if(Dogovors.ProverkaStatysa(id, DateTime.Now.ToString()) == true)
            {
                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        static public int flag = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите договор");
                return;
            }
            flag = 1;
            AddEditDogovor edit = new AddEditDogovor();
            edit.ShowDialog();
            Dogovors.View(PostavshikForm.id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Dogovors.DogovorStatys(PostavshikForm.id) == true)
            {
                return;
            }
            flag = 0;
            AddEditDogovor add = new AddEditDogovor();
            add.ShowDialog();
            Dogovors.View(PostavshikForm.id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string data = "";
            if(Convert.ToDateTime(dataN).Date > DateTime.Now.Date)
            {
                data = Convert.ToDateTime(dataN).ToString("yyyy-MM-dd");
            }
            else
            {
                data = DateTime.Now.ToString("yyyy-MM-dd");
            }
            Dogovors.Rastorgnyt(PostavshikForm.id, data);
            Dogovors.View(PostavshikForm.id);
        }
    }
}
