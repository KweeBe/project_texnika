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
    public partial class HistoriTexMerop : Form
    {
        public HistoriTexMerop()
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

        private void HistoriTexMerop_Load(object sender, EventArgs e)
        {
            label1.Text = label1.Text + TexniksView.vid + " " + TexniksView.name + " (" + TexniksView.nomer + ")";
            Meropriyatiya.ViewMeropPoTexnike(TexniksView.id);
            dataGridView1.DataSource = Meropriyatiya.meropPoTexnike;
        }
    }
}
