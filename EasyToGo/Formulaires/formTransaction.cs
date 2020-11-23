using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyToGo.Classes;
using EasyToGo.Repports;
using DevExpress.XtraReports.UI;

namespace EasyToGo.Formulaires
{
    public partial class formTransaction : Form
    {
        public formTransaction()
        {
            InitializeComponent();
        }

        private void formTransaction_Load(object sender, EventArgs e)
        {
            ClassGlossaire.Instance.GetDatas(gridControl1, "*", "transaction");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                ficheTransBus j = new ficheTransBus();
                j.DataSource = ClassGlossaire.Instance.sortietTransactBus();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                ficheFiltreTransac j = new ficheFiltreTransac();
                j.DataSource = ClassGlossaire.Instance.sortieFiltreTransc(textBox1.Text,textBox2.Text);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
