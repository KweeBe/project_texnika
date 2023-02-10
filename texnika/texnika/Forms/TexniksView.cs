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
    public partial class TexniksView : Form
    {
        public TexniksView()
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
        private void TexniksView_Load(object sender, EventArgs e)
        {
            try
            {
                if (Texniks.Views(textBox1.Text, comboBox1.Text, comboBox2.Text) == false)
                    this.Close();
                dataGridView1.DataSource = Texniks.texView;
                VidTexniki.ViewComboboxFiltr();
                comboBox1.DataSource = VidTexniki.comboboxFiltr;
                comboBox1.DisplayMember = "Name_vid";
                comboBox1.ValueMember = "idVid";

                comboBox2.SelectedIndex = 0;
            }
            catch
            {
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Texniks.Views(textBox1.Text, comboBox1.Text, comboBox2.Text);
        }

        static public int flag = 0;
        static public int flag2 = 0;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flag = 1;
            Texniks.Views(textBox1.Text, comboBox1.Text, comboBox2.Text);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            flag2 = 1;
            Texniks.Views(textBox1.Text, comboBox1.Text, comboBox2.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != null)
            {
                Documents.TexnikaImeysh(saveFileDialog1.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value.Date > dateTimePicker2.Value.Date)
            {
                MessageBox.Show("Дата начала не может привышать дату окончания!");
                return;
            }


            if (saveFileDialog2.ShowDialog() == DialogResult.OK && saveFileDialog2.FileName != null)
            {
                Documents.TexnikaSpis(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"), saveFileDialog2.FileName);
            }
        }

        static public string id = "";
        static public string name = "";
        static public string vid = "";
        static public string nomer = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            vid = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            name = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            nomer = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            if (e.ColumnIndex == 0)
            {
                HistoriTexMerop histori = new HistoriTexMerop();
                histori.Show();
            }
        }
    }
}
