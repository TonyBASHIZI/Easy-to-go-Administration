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
    public partial class formCompte : Form
    {
        public formCompte()
        {
            InitializeComponent();
        }

        private void formCompte_Load(object sender, EventArgs e)
        {
            ClassGlossaire.Instance.GetDatas(gridControl1, "*", "compte");

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            labdesign.Text = gridView1.GetFocusedRowCellValue("designation").ToString();
            labcompte.Text = gridView1.GetFocusedRowCellValue("ref_compagnie").ToString();
            labsolde.Text = gridView1.GetFocusedRowCellValue("solde").ToString();
            label7.Text = gridView1.GetFocusedRowCellValue("designation").ToString();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                ficheCompte j = new ficheCompte();
                j.DataSource = ClassGlossaire.Instance.sortietFichecompte();
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
