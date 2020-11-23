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

namespace EasyToGo.Formulaires
{
    public partial class formBackup : Form
    {
        public formBackup()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtemplassement.Text = dlg.SelectedPath;
                    
                }
            }
            catch (Exception)
            { }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (txtemplassement.Text == string.Empty)
            {
                MessageBox.Show("Veuillez selectionner d'abord un emplacement s.v.p.!");
            }
            else
            {
                clsDatebaseBackupRestor cl = new clsDatebaseBackupRestor();
                //txtemplassement.Text = ""+cl.getBackupPath();
                //Classes.ClassGlossaire.Instance.Backup(txtemplassement.Text);
                
            }
        }

        private void formBackup_Load(object sender, EventArgs e)
        {

        }
    }
}
