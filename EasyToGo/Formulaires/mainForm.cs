using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using EasyToGo.Repports;
using EasyToGo.Classes;
using DevExpress.XtraReports.UI;

namespace EasyToGo.Formulaires
{
    public partial class mainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            formCompagnie ac = new formCompagnie();
            ac.MdiParent = this;
            ac.Show();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            accuielForm ac = new accuielForm();
            ac.MdiParent = this;
            ac.Show();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            formCompte ac = new formCompte();
            ac.MdiParent = this;
            ac.Show();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            formAgent ag = new formAgent();
            ag.ShowDialog();
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            formUtilisateur ag = new formUtilisateur();
            ag.ShowDialog();

        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            formTransaction ag = new formTransaction();
            ag.ShowDialog();

        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            formBanquing ag = new formBanquing();
            ag.ShowDialog();

        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ficheHistoriqueTransact j = new ficheHistoriqueTransact();
                j.DataSource = ClassGlossaire.Instance.sortietHistoriqueTransact();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Hide();
            splashForm sp = new splashForm();
            sp.ShowDialog();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            formRegister re = new formRegister();
            re.ShowDialog();
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ficheTransfertBQ j = new ficheTransfertBQ();
                j.DataSource = ClassGlossaire.Instance.sortietHistoriquetTransfertBq();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            formBackup bac = new formBackup();
            bac.ShowDialog();

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            accuielForm ac = new accuielForm();
            ac.MdiParent = this;
            ac.Show();

        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ficheTransfert j = new ficheTransfert();
                j.DataSource = ClassGlossaire.Instance.sortieTranferts();
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