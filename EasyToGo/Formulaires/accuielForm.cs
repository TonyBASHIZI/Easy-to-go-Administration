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
using MySql.Data.MySqlClient;

namespace EasyToGo.Formulaires
{
    public partial class accuielForm : Form
    {
       private int cmpte = 0;
        public accuielForm()
        {
            InitializeComponent();
            //timer1.Enabled = true;
        }
        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chartControl4_Click(object sender, EventArgs e)
        {

        }
        private void chart1()
        {
            //int x = 80;
            //int y = 50;
            //try
            //{

            //    chartControl1.Series["Transactions"].Points.AddPoint("" + x, y);
            //    chartControl1.Series["Bus"].Points.AddPoint("" +500,1000);
               


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }
        public void getTOTALCompagnie()
        {
            try
            {
                DataTable dtt = ClassGlossaire.Instance.GetData();
                //get reference bus 
                string[] x = (from p in dtt.AsEnumerable() orderby p.Field<string>("designation") ascending select p.Field<string>("designation")).ToArray();

                // get the total of transact

                decimal[] y = (from p in dtt.AsEnumerable() orderby p.Field<Decimal>("solde") ascending select p.Field<Decimal>("solde")).ToArray();

                chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
                chart2.Series[0].Points.DataBindXY(x, y);

                chart2.Legends[0].Enabled = true;
                chart2.ChartAreas[0].Area3DStyle.Enable3D = false;
            }catch(Exception ex)
            {
                timer1.Stop();
                MessageBox.Show("Une erreur s'est produite lors du chargement des données en temps réel. \n\nL'Application va s'arrêter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Une erreur s'est produite lors de l'opération : " + ex.Message);
                Application.Exit();
                //MessageBox.Show(ex.Message);
            }
           
        }
        public void getTransactionsDate()
        {
           // DataTable dtt = ClassGlossaire.Instance.GetDataTransct();

           // DateTime[] x = (from p in dtt.AsEnumerable() orderby p.Field<DateTime>("dateTransact") ascending select p.Field<DateTime>("dateTransact")).ToArray();
           //// int[] t = (from p in dtt.AsEnumerable() orderby p.Field<int>("montant") ascending select p.Field<int>("montant")).ToArray();
           // decimal[] y = (from p in dtt.AsEnumerable() orderby p.Field<Decimal>("montant") ascending select p.Field<Decimal>("montant")).ToArray();

           // //chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
           // chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
           // chart3.Series[0].Points.DataBindXY(x, y);
           // //chart3.Series[1].Points.DataBindXY(x, t);

           // chart3.Legends[0].Enabled = true;
           // chart3.ChartAreas[0].Area3DStyle.Enable3D = false;
           // //chart2.ChartAreas[0].CursorX.LineColor = Color.Beige;
        }
        public void getBusCompagnie()
        {
            try
            {
                DataTable dtt = ClassGlossaire.Instance.GetDataCompagnieBus();
                //get reference bus 
                string[] x = (from p in dtt.AsEnumerable() orderby p.Field<string>("ref_compagnie") ascending select p.Field<string>("ref_compagnie")).ToArray();

                // get the total
                //SELECT compagnie.noms as nom,bus.plaque as plaques,COUNT(bus.id) as nb FROM `bus` RIGHT JOIN compagnie on bus.ref_compagnie = compagnie.noms
                //GROUP By compagnie.noms,bus.plaque

                int[] y = (from p in dtt.AsEnumerable() orderby p.Field<string>("ref_compagnie") ascending select p.Field<int>("id")).ToArray();

                chartCompBus.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
                chartCompBus.Series[0].Points.DataBindXY(x, y);
                chartCompBus.Legends[0].Enabled = true;
                chartCompBus.ChartAreas[0].Area3DStyle.Enable3D = false;
            }
            catch (Exception ex)
            {
                timer1.Stop();
                MessageBox.Show(this, "Une erreur s'est produite lors du chargement des données en temps réel. \n\nL'Application va s'arrêter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Une erreur s'est produite lors de l'opération : " + ex.Message);
                Application.Exit();
            }
            
        }

        private void getDash()
        {
            try
            {
                
                chart1();
                labcomm.Text = ClassGlossaire.Instance.getCommission();
                labtrans.Text = ClassGlossaire.Instance.getTransact();
                labcompa.Text = ClassGlossaire.Instance.getCompagnie();
                //getTransactionsDate();
                getTOTALCompagnie();
                getBusCompagnie();
            }
            catch (Exception ex)
            {
                timer1.Stop();
                MessageBox.Show(this,"Une erreur s'est produite lors du chargement des données en temps réel. \n\nL'Application va s'arrêter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Une erreur s'est produite lors de l'opération : " + ex.Message);
                Application.Exit();
            }
            
        }
        private void accuielForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            getDash();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void chartControl1_Click(object sender, EventArgs e)
        {

        }

        private void chartControl3_Click(object sender, EventArgs e)
        {

        }

        private void chartCompBus_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            cmpte++;
            if (cmpte == 5)
            {
                getDash();
                cmpte = 0;
            }
        }
    }
}
