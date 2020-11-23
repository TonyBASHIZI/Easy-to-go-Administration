using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyToGo.Classes;
using EasyToGo.Repports;
using DevExpress.XtraReports.UI;

namespace EasyToGo.Formulaires
{
    public partial class formCompagnie : Form
    {
        string imglocation = "";
        public formCompagnie()
        {
            InitializeComponent();
        }
        private void getCode()
        {
            Random rd = new Random();
            int x = rd.Next(1, 500);
            string code = "TR-" + x;
            txtcode.Text = "" + code;
        }
        private void initialise()
        {
            txtnom.Text = "";
            txtrccm.Text = "";
            txtadresse.Text = "";
            //txtcommi.Text = "";
            txtdescri.Text = "";
            txtemail.Text = "";
           
            getCode();

        }
        private Byte[] convertImagePicEdit(PictureEdit pic)
        
        {
            MemoryStream ms = new MemoryStream();
            Bitmap bmpImage = new Bitmap(pic.Image);
            Byte[] bytImage;
            bytImage = ms.ToArray();
            try
            {
               
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                
                ms.Close();
                

            }catch(Exception ex)
            {
                MessageBox.Show("Veiller choisir une image ou logo");
            }

            return bytImage;
            
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void formCompagnie_Load(object sender, EventArgs e)
        {
            getCode();
            Classes.ClassGlossaire.Instance.GetDatas(gridControl1, "*", "compagnie");
        }
      

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*png|jpg files(*.jpg)|*jpg|All files(*.*)|*.*  ";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imglocation = dialog.FileName.ToString();
                pictureEdit1.Image = Image.FromFile(imglocation);


            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (txtcode.Text == "")
            {
                MessageBox.Show("Veillez remplir tous les champs!");
            }
            else
            {
                try
                {
                    ClassCompagnie c = new ClassCompagnie();
                    c.Code = txtcode.Text.ToUpper();
                    c.Noms = txtnom.Text.ToUpper();
                    c.photo = convertImagePicEdit(pictureEdit1);
                    c.Rccm = txtrccm.Text.ToUpper();
                    //c.Ref_POS = comboBoPOS.Text;
                    c.Adresse = txtadresse.Text.ToUpper();
                    c.Email = txtemail.Text;
                    c.Description = txtdescri.Text.ToUpper();
                    c.Etat = ComboEtat.Text;

                    ClassGlossaire.Instance.insertCompagie(c);

                    ClassGlossaire.Instance.GetDatas(gridControl1, "*", "compagnie");
                    initialise();
                    //getCode();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (txtcode.Text == "")
            {
                MessageBox.Show("Veillez remplir tous les champs!");
            }
            else
            {
                ClassCompagnie c = new ClassCompagnie();
                c.Code = txtcode.Text.ToUpper();
                c.Noms = txtnom.Text.ToUpper();
                c.photo = convertImagePicEdit(pictureEdit1);
                c.Rccm = txtrccm.Text.ToUpper();
                //c.Ref_POS = comboBoPOS.Text;
                c.Adresse = txtadresse.Text.ToUpper();
                c.Email = txtemail.Text;
                c.Description = txtdescri.Text.ToUpper();
                c.Etat = ComboEtat.Text;

                ClassGlossaire.Instance.updateCompagnie(c);
                ClassGlossaire.Instance.GetDatas(gridControl1, "*", "compagnie");
                initialise();
                //getCode();
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            txtcode.Text = gridView1.GetFocusedRowCellValue("code").ToString();
            txtnom.Text = gridView1.GetFocusedRowCellValue("noms").ToString();
            txtemail.Text = gridView1.GetFocusedRowCellValue("email").ToString();
            txtadresse.Text = gridView1.GetFocusedRowCellValue("adresse").ToString();
            txtrccm.Text = gridView1.GetFocusedRowCellValue("rccm").ToString();
            //txtcommi.Text = gridView1.GetFocusedRowCellValue("commission").ToString();
            txtdescri.Text = gridView1.GetFocusedRowCellValue("description").ToString();
            ComboEtat.Text = gridView1.GetFocusedRowCellValue("etat").ToString();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ClassCompagnie c = new ClassCompagnie();
            c.Code = txtcode.Text.ToUpper();
            c.Noms = txtnom.Text.ToUpper();
            c.Description = txtdescri.Text;

            if (ClassGlossaire.Instance.updateStatut("INACTIF", c.Code, "compagnie") == true)
            {
                ClassGlossaire.Instance.updateCompte(c.Noms,c.Description,c.Code);
            }
            
            
            ClassGlossaire.Instance.GetDatas(gridControl1, "*", "compagnie");

            initialise();
            //getCode();

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                ficheCompagnie j = new ficheCompagnie();
                j.DataSource = ClassGlossaire.Instance.sortietFicheCompagnie();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (txtcode.Text == "")
            {
                MessageBox.Show("Veillez remplir tous les champs!");
            }
            else
            {
                try
                {
                    ClassCompagnie c = new ClassCompagnie();
                    c.Code = txtcode.Text.ToUpper();
                    c.Noms = txtnom.Text.ToUpper();
                    c.photo = convertImagePicEdit(pictureEdit1);
                    c.Rccm = txtrccm.Text.ToUpper();
                    //c.Ref_POS = comboBoPOS.Text;
                    c.Adresse = txtadresse.Text.ToUpper();
                    c.Email = txtemail.Text;
                    c.Description = txtdescri.Text.ToUpper();
                    c.Etat = ComboEtat.Text;

                    ClassGlossaire.Instance.insertCompagie(c);

                    ClassGlossaire.Instance.GetDatas(gridControl1, "*", "compagnie");
                    initialise();
                    //getCode();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
