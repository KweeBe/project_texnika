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
    public partial class VidTexnikiForm : Form
    {
        public VidTexnikiForm()
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
        private void VidTexnikiForm_Load(object sender, EventArgs e)
        {
            if (VidTexniki.View() == false)
                this.Close();
            dataGridView1.DataSource = VidTexniki.vid;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите вид техники");
                return;
            }
            DialogResult otv = MessageBox.Show("Точно хотите удалить?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (otv == DialogResult.Yes)
            {
                VidTexniki.Delete(id);
                VidTexniki.View();
            }
            id = "";
        }

        static public string id = "";
        static public string name = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        static public int flag;
        private void button1_Click(object sender, EventArgs e)
        {
            flag = 0;
            AddEditVidTexniki add = new AddEditVidTexniki();
            add.ShowDialog();
            VidTexniki.View();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("Выберите вид техники");
                return;
            }
            flag = 1;
            AddEditVidTexniki edit = new AddEditVidTexniki();
            edit.ShowDialog();
            VidTexniki.View();
            id = "";
        }
    }
}
