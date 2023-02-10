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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        static public int flag = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            flag = 0;
            if (DBconnect.Connect() == false)
            {
                this.Close();
            }
            if(DBconnect.ChekTables() == 1)
            {
                flag = 1;
                MessageBox.Show("Вы подключились не к тобой базе данных!");
                this.Close();
                Settings set = new Settings();
                set.Show();
            }
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBconnect.CloseBd();
            if(flag == 0)
            {
                
                Form form1 = Application.OpenForms[0];
                form1.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VidTexnikiForm vidF = new VidTexnikiForm();
            vidF.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SotrudnikForm sotr = new SotrudnikForm();
            sotr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MeropriyatiyaForms merop = new MeropriyatiyaForms();
            merop.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PostavshikForm postav = new PostavshikForm();
            postav.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PostavkaForm postav = new PostavkaForm();
            postav.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SpisanieForm spisanie = new SpisanieForm();
            spisanie.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RaspolojForm raspol = new RaspolojForm();
            raspol.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TexniksView view = new TexniksView();
            view.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SpravochSistema sprav = new SpravochSistema();
            sprav.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                ImpAndExp.Backup(saveFileDialog1.FileName);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName != "")
            {
                ImpAndExp.CreateBD();
                ImpAndExp.Restore(openFileDialog1.FileName);
            }
        }
    }
}
