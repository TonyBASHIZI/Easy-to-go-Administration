using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyToGo.Formulaires
{
    public partial class splashForm : Form
    {
        public splashForm()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (txtuser.Text == "" || txtpasse.Text == "")
            {
                MessageBox.Show("Tous les champs doivent etre completés!!!");
            }
            else
            {
                if (Classes.ClassGlossaire.Instance.LoginTest(txtuser.Text, txtpasse.Text) == true)
                {
                    this.Hide();
                    mainForm fo = new mainForm();
                    fo.ShowDialog();
                }

            }
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void splashForm_Load(object sender, EventArgs e)
        {
            Classes.ClassGlossaire.Instance.InitializeConnection();
             
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
