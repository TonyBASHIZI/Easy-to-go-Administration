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
    public partial class formBanquing : Form
    {
        public formBanquing()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void formBanquing_Load(object sender, EventArgs e)
        {
            Classes.ClassGlossaire.Instance.chargerComboRefCompagnie(comboRefcompte);
            Classes.ClassGlossaire.Instance.getComm(txtcommi, txttransact);
            
        }

        private void comboRefcompte_SelectedValueChanged(object sender, EventArgs e)
        {
           labsolde.Text =  Classes.ClassGlossaire.Instance.getSolde(comboRefcompte.Text);
           labcompa.Text = Classes.ClassGlossaire.Instance.getCompta(comboRefcompte.Text);
           textBox2.Text = Classes.ClassGlossaire.Instance.getCompta(comboRefcompte.Text);
           textBox3.Text = Classes.ClassGlossaire.Instance.getSolde(comboRefcompte.Text);
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            ClassTransfert tr = new ClassTransfert();
            tr.Bordereau = textBox1.Text;
            tr.Beneficiaire = textBox2.Text;
            tr.Montant = textBox3.Text;
            Classes.ClassGlossaire.Instance.insertTranfer(tr);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            labsolde.Text = "0";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Classes.ClassGlossaire.Instance.updatePoorcentage(txtcommi.Text, txttransact.Text, "Paiement");
            Classes.ClassGlossaire.Instance.getComm(txtcommi, txttransact);

        }
    }
}
