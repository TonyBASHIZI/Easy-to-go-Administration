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
    public partial class formAgent : Form
    {
        public formAgent()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                ClassAgent ag = new ClassAgent();
                ag.Nom = txtnom.Text;
                ag.Matricule = txtmat.Text;
                ag.Postnom = txtpostnom.Text;
                ag.Tel = txtphone.Text;
                ag.Adresse = txtAdresse.Text;
                ag.Email = txtEmail.Text;
                ag.Prenom = txtprenom.Text;
                ag.Solde = txtsolde.Text;
                ag.Password = txtpassword.Text;


                ClassGlossaire.Instance.insertAgent(ag);
                initialise();
                ClassGlossaire.Instance.GetDatas(gridControl1, "*", "agent");

            }
            catch (Exception ex)
            {

            }
        }
        private void getMat()
        {
            Random rd = new Random();
            int x = rd.Next(1, 500);
            string matr = "TR-AG-" + x;
            txtmat.Text = "" + matr;
        }
        private void initialise()
        {
            getMat();
            txtnom.Text = "";
            txtprenom.Text = "";
            txtAdresse.Text = "";
            txtpostnom.Text = "";
            txtphone.Text = "";
            txtEmail.Text = "";
           
        }
        
 
        private void formAgent_Load(object sender, EventArgs e)
        {
            getMat();
            ClassGlossaire.Instance.GetDatas(gridControl1, "*", "agent");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                ClassAgent ag = new ClassAgent();
                ag.Nom = txtnom.Text;
                ag.Matricule = txtmat.Text;
                ag.Postnom = txtpostnom.Text;
                ag.Tel = txtphone.Text;
                ag.Adresse = txtAdresse.Text;
                ag.Email = txtEmail.Text;
                ag.Prenom = txtprenom.Text;
                ag.Solde = txtsolde.Text;
                ag.Password = txtpassword.Text;

                ClassGlossaire.Instance.updateAgent(ag);
                initialise();
                ClassGlossaire.Instance.GetDatas(gridControl1, "*", "agent");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                ClassAgent ag = new ClassAgent();
                ag.Matricule = txtmat.Text;
              
                ClassGlossaire.Instance.deleteAgent(ag);
                initialise();
                ClassGlossaire.Instance.GetDatas(gridControl1, "*", "agent");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            txtmat.Text = gridView1.GetFocusedRowCellValue("matricule").ToString();
            txtnom.Text = gridView1.GetFocusedRowCellValue("nom").ToString();
            txtpostnom.Text = gridView1.GetFocusedRowCellValue("postnom").ToString();
            txtprenom.Text = gridView1.GetFocusedRowCellValue("prenom").ToString();
            txtAdresse.Text = gridView1.GetFocusedRowCellValue("adresse").ToString();
            txtEmail.Text = gridView1.GetFocusedRowCellValue("email").ToString();
            txtphone.Text = gridView1.GetFocusedRowCellValue("tel").ToString();

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
