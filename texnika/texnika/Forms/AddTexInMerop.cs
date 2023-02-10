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
    public partial class AddTexInMerop : Form
    {
        public AddTexInMerop()
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
        private void AddTexInMerop_Load(object sender, EventArgs e)
        {
            VidTexniki.ViewCombobox();
            comboBox2.DataSource = VidTexniki.vidCombobox;
            comboBox2.DisplayMember = "name_vid";
            comboBox2.ValueMember = "idVid";
            comboBox2.SelectedIndex = -1;
            comboBox2.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "")
            {
                return;
            }
            if(TexnikaInMerop.Add(MeropriyatiyaForms.id, comboBox1.SelectedValue.ToString())== true)
            {
                this.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
                return;
            }
            Texniks.ViewCombobox(Convert.ToDateTime(MeropriyatiyaForms.date).ToString("yyyy-MM-dd"), comboBox2.SelectedValue.ToString());
            comboBox1.DataSource = Texniks.comboboxMerop;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "idTex";
        }
    }
}
