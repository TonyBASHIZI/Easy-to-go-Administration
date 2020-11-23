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
    public partial class formRegister : Form
    {
        public formRegister()
        {
            InitializeComponent();
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }
        public void initiale()
        {
            txttel.Text = "";
            txtnom.Text = "";
            txtpwd.Text = "";
            txtuser.Text = "";


        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(txtuser.Text =="" || txtpwd.Text =="" || txtnom.Text == "" || txttel.Text == "")
            {
                MessageBox.Show("Remplisser tous les champs!!");
            }
            else
            {
                ClassRegister reg = new ClassRegister();
                reg.Nom = txtnom.Text;
                reg.Telephone = txttel.Text;
                reg.Pwd = txtpwd.Text;
                reg.Users = txtuser.Text;
                ClassGlossaire.Instance.insertUsers(reg);

                initiale();
                ClassGlossaire.Instance.GetDatas(gridControl1, "*", "users");

            }
            

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (txtuser.Text == "" || txtpwd.Text == "" || txtnom.Text == "" || txttel.Text == "")
            {
                MessageBox.Show("Remplisser tous les champs!!");
            }
            else
            {
                ClassRegister reg = new ClassRegister();
                reg.Id = int.Parse(label6.Text);
                reg.Nom = txtnom.Text;
                reg.Telephone = txttel.Text;
                reg.Pwd = txtpwd.Text;
                reg.Users = txtuser.Text;
                ClassGlossaire.Instance.modifierUsers(reg);
                initiale();

            }

        }

        private void formRegister_Load(object sender, EventArgs e)
        {
            ClassGlossaire.Instance.GetDatas(gridControl1, "*", "users");
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            label6.Text = gridView1.GetFocusedRowCellValue("id").ToString();
            txtnom.Text = gridView1.GetFocusedRowCellValue("nom").ToString();
            txttel.Text = gridView1.GetFocusedRowCellValue("telephone").ToString();
            txtpwd.Text = gridView1.GetFocusedRowCellValue("passe").ToString();
            txtuser.Text = gridView1.GetFocusedRowCellValue("username").ToString();
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ClassRegister reg = new ClassRegister();
            reg.Id = int.Parse(label6.Text);
            reg.Nom = txtnom.Text;
            reg.Telephone = txttel.Text;
            reg.Pwd = txtpwd.Text;
            reg.Users = txtuser.Text;
            ClassGlossaire.Instance.deleteLogin(reg);
            initiale();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
