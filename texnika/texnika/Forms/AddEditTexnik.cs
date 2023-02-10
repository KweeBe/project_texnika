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
    public partial class AddEditTexnik : Form
    {
        public AddEditTexnik()
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

        private void AddEditTexnik_Load(object sender, EventArgs e)
        {
            VidTexniki.ViewCombobox();
            comboBox1.DataSource = VidTexniki.vidCombobox;
            comboBox1.DisplayMember = "Name_vid";
            comboBox1.ValueMember = "idVid";

            if(PostavkaForm.flagT == 1)
            {
                this.Text = "Изменение";
                textBox1.Text = PostavkaForm.nameTex;
                comboBox1.Text = PostavkaForm.vid;
                textBox2.Text = PostavkaForm.nomerZ;
                textBox3.Text = PostavkaForm.nomerI;
                textBox4.Text = PostavkaForm.cena.Replace(',','.');
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (PostavkaForm.flagT == 1)
            {
                if (Texniks.Edit(PostavkaForm.idTex, textBox1.Text,
                    comboBox1.SelectedValue.ToString(), textBox2.Text,
                    textBox3.Text, textBox4.Text, PostavkaForm.nomerZ,
                    PostavkaForm.nomerI) == true)
                {
                    this.Close();
                }
            }
            else
            {
                if(Texniks.Add(PostavkaForm.id, textBox1.Text, 
                    comboBox1.SelectedValue.ToString(), textBox2.Text, 
                    textBox3.Text, textBox4.Text) == true)
                {
                    this.Close();
                }
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
