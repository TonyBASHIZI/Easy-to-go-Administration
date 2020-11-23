using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using System.Collections;
using EasyToGo.Classes;
using EasyToGo.Formulaires;

namespace EasyToGo.Classes
{
    class ClassGlossaire
    {
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        Connection cnx;
        MySqlDataAdapter dt = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adpr = null;
        DataSet dste;
        private string server;
        private string database;
        private string uid;
        private string password;
        clsDatebaseBackupRestor bd = new clsDatebaseBackupRestor();
        private string port;
       // private string str, code_isn;
        private static ClassGlossaire _instance = null;


        public static ClassGlossaire Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClassGlossaire();
                return _instance;
            }
        }

        #region Common

        public void InitializeConnection()
        {
            try
            {
                cnx = new Connection(); cnx.Connect();
                con = new MySqlConnection(cnx.path);

                if (!con.State.ToString().ToLower().Equals("open"))
                {
                    con.Open();
                }
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Une erreur s'est produite lors du chargement des données en temps réel. \n\nL'Application va s'arrêter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Une erreur s'est produite lors de l'opération : " + ex.Message);
                Application.Exit();
                
            }
            //try
            //{

                //server = "192.168.17.18";
                //database = "easy_to_go";
                //uid = "Julio";
                //password = "myserver";
                //port = "3306";
                //string co = "Server=" + server + ";UserId=" + uid + ";Port=" + port + ";Password=" + password + ";Database=" + database;
                //con = new MySqlConnection(co);
                //con.Open();
                //server = "192.162.69.191";
                //database = "c1alohadynamics_db";
                //uid = "c1aloha_db";
                //password = "adminadmin";
                //port = "3306";
                //string co = "Server=" + server + ";UserId=" + uid + ";Port=" + port + ";Password=" + password + ";Database=" + database;
                //con = new MySqlConnection(co);
                //con.Open();
                //MessageBox.Show("Connection ok");

                //string co = "Data Source=localhost;Initial Catalog=easy_to_go; User Id=root; Password=root;";
                //con = new MySqlConnection(co);
                //con.Open();


                //if (!con.State.ToString().ToLower().Equals("open"))
                //{
                //    con.Open();
                //}
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Impossible de se connecter a un serveur!! contactez Administrateur");
            //}
        }

        private void SetParameter(IDbCommand cmd, string name, DbType type, int length, object value)
        {
            IDbDataParameter param = cmd.CreateParameter();

            param.ParameterName = name;
            param.DbType = type;
            param.Size = length;

            if (value == null)
            {
                if (!param.IsNullable)
                {
                    param.DbType = DbType.String;
                }

                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }

            cmd.Parameters.Add(param);
        }

        public void GetDatas(GridControl grid, string field, string table)
        {
            InitializeConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT " + field + " FROM " + table + " ORDER BY id DESC ";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void GetCombosData(ComboBoxEdit combo, string field, string table)
        {
            combo.Properties.Items.Clear();

            InitializeConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT " + field + " FROM " + table;

                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        combo.Properties.Items.Add(dr[field]).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            finally
            {
                dr.Dispose();
            }
        }
        #endregion

        #region compagnie
         
        public void insertCompagie(ClassCompagnie c)
        {
            InitializeConnection();
            try
            {
                
                string q = "insert into compagnie(code,noms,description,adresse,photo,rccm,email) value(@code,@noms,@description,@adresse,@photo,@rccm,@email)";
                cmd = new MySqlCommand(q,con);
                cmd.Parameters.Add(new MySqlParameter("@code", c.Code));
                cmd.Parameters.Add(new MySqlParameter("@noms", c.Noms));
                cmd.Parameters.Add(new MySqlParameter("@description", c.Description));
                cmd.Parameters.Add(new MySqlParameter("@adresse", c.Adresse));
                cmd.Parameters.Add(new MySqlParameter("@photo", c.photo));
                cmd.Parameters.Add(new MySqlParameter("@rccm", c.Rccm));
                cmd.Parameters.Add(new MySqlParameter("@email", c.Email));
                //cmd.Parameters.Add(new MySqlParameter("@commission", c.Commission));

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Insertion effectuée avec succes!");
                    cmd.Dispose();
                    con.Close();
                    //creation compte
                    c.refSolde = 0;

                    createCompte(c);
                    createUser(c.Noms, c.Description, "1234", 1);

                }

            }catch(Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
           
        }
        public void updateCompagnie(ClassCompagnie c)
        {
            InitializeConnection();
            try
            {

                string q = "update compagnie set noms=@noms,description=@description,adresse=@adresse,photo=@photo,rccm=@rccm,email=@email,etat=@etat where code=@code";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@code", c.Code));
                cmd.Parameters.Add(new MySqlParameter("@noms", c.Noms));
                cmd.Parameters.Add(new MySqlParameter("@description", c.Adresse));
                cmd.Parameters.Add(new MySqlParameter("@adresse", c.Description));
                cmd.Parameters.Add(new MySqlParameter("@photo", c.photo));
                cmd.Parameters.Add(new MySqlParameter("@rccm", c.Rccm));
                cmd.Parameters.Add(new MySqlParameter("@email", c.Email));      
                //cmd.Parameters.Add(new MySqlParameter("@ref_pos", c.Ref_POS));
                cmd.Parameters.Add(new MySqlParameter("@etat", c.Etat));

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Modification effectuée avec succes!");
                    cmd.Dispose();
                    con.Close();
                    ClassCompte co = new ClassCompte();
                    co.Ref_compagnie = c.Code;
                    co.Designation = c.Noms;
                    co.Description = c.Description;
                    updateCompte(co.Designation,co.Description,"compte");

                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
           

        }

        public void deleteCompagnie(ClassCompagnie c)
        {
            string q = "delete from compagnie where code=@code";
            cmd = new MySqlCommand(q, con);
            cmd.Parameters.Add(new MySqlParameter("@code", c.Code));
            DialogResult result = MessageBox.Show("Voulez-vous vraiment excuter cette operation ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Suppression avec succes!!");

                }
            }

            else
            {
               // MessageBox.Show("Opération Annulée !");
            }
        }
        public void createCompte(ClassCompagnie compte)
        {
            InitializeConnection();
            try
            {
                string q = "insert into compte(designation,description,solde,ref_compagnie) values(@designation,@description,@solde,@ref_compagnie)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@designation", compte.Noms));
                cmd.Parameters.Add(new MySqlParameter("@description", compte.Description));
                cmd.Parameters.Add(new MySqlParameter("@solde", compte.refSolde));
                cmd.Parameters.Add(new MySqlParameter("@ref_compagnie", compte.Code));

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("creation compte effectué avec succes!!");
                }

            }catch(Exception ex)
            {

                //MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            
        
        
        }
        public bool updateStatut(string etat, string code, string table)
        {
            bool v = false;
            InitializeConnection();
            try
            {
                string q = "update " + table + " set etat=@etat where code=@code ";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@etat", etat));
                cmd.Parameters.Add(new MySqlParameter("@code", code));


                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Modification etat compagnie!");
                    v = true;
                    cmd.Dispose();
                    con.Close();
                    
                    

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            return v;
           
        }
          
        
        public void updateStatutcompte(string etat, string code, string table)
        {
            InitializeConnection();
            try
            {
                string q = "update " + table + " set etat=@etat where designation=@designation ";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@etat", etat));
                cmd.Parameters.Add(new MySqlParameter("@designation", code));


                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Modification etat compte!");
                    cmd.Dispose();
                    con.Close();
                    deleteUser(code, "utilisateur");
                    
                    
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           


        }
        public void deleteUser(string code, string table)
        {
            InitializeConnection();
            try
            {
                string q = "delete from " + table + " where description=@description ";
                cmd = new MySqlCommand(q, con);
                
                cmd.Parameters.Add(new MySqlParameter("@description", code));


                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("delete User!");
                }

            }
            catch (Exception ex)
            {

              //  MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }



        }
        public void updateCompte(string design, string desc, string ref_comp)
        {
            InitializeConnection();
            try
            {
                string q = "update compte set designation=@designation,description=@description where ref_compagnie=@ref_compagnie";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@designation", design));
                cmd.Parameters.Add(new MySqlParameter("@description", desc));
                cmd.Parameters.Add(new MySqlParameter("@ref_compagnie", ref_comp));

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Modication du compte effectuée avec succes!!");
                    cmd.Dispose();
                    con.Close();

                    deleteUser(design,"utilisateur");
                }

            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.Message);
            }
            
        
        
        }
        public DataTable GetData()
        {
            DataTable dts = new DataTable();

            try
            {
                InitializeConnection();
                string q = "SELECT solde,designation FROM `compte` GROUP BY designation";
                MySqlDataAdapter sda = new MySqlDataAdapter(q, con);
                
                sda.Fill(dts);

                
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return dts;
        }
        public DataTable GetDataTransct()
        {
            DataTable dts = new DataTable();

            try
            {
                InitializeConnection();
                string q = "SELECT id,SUM(montant) as montant,dateTransact FROM `transaction` GROUP BY dateTransact";
                // q = "SELECT * from transactionDates";
                MySqlDataAdapter sda = new MySqlDataAdapter(q, con);

                sda.Fill(dts);


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();

            }

            return dts;
        }

        public DataTable GetDataCompagnieBus()
        {
            DataTable dts = new DataTable();

            try
            {
                InitializeConnection();
                string q = "SELECT COUNT(id) as nb,ref_compagnie,id from bus GROUP by ref_compagnie";
                MySqlDataAdapter sda = new MySqlDataAdapter(q, con);

                sda.Fill(dts);


            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return dts;
        }
        public void createUser(string descri,string users, string passwords, int niv)
        {
            InitializeConnection();
            try
            {
                string q = "insert into utilisateur(description,username,password,niveau) values(@description,@username,@password,@niveau)";
                cmd = new MySqlCommand(q,con);
                cmd.Parameters.Add(new MySqlParameter("@description", descri));
                cmd.Parameters.Add(new MySqlParameter("@username", users));
                cmd.Parameters.Add(new MySqlParameter("@password", passwords));
                cmd.Parameters.Add(new MySqlParameter("@niveau", niv));

                if(cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Niveau d'acces pour cette compagnie est créee");
                }


            }catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

        }
        public string getCommission()
        {
            
            string c = "";
            try
            {

                InitializeConnection();

                string q = "select SUM(commission) as nb from transaction";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = dr.GetString("nb");
                }


            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Une erreur s'est produite lors du chargement des données en temps réel. \n\nL'Application va s'arrêter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Une erreur s'est produite lors de l'opération : " + ex.Message);
                Application.Exit();
     
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }


            return c;
        }
        public string getTransact()
        {

            string c = "";
            try
            {

                InitializeConnection();

                string q = "select SUM(fraisTransact) as nb from transaction";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = dr.GetString("nb");
                }


            }
            catch (Exception ex)
            {
               
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }


            return c;
        }
        public string getCompagnie()
        {

            string c = "";
            try
            {

                InitializeConnection();

                string q = "select COUNT(id) as nb from compagnie";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = dr.GetString("nb");
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }


            return c;
        }
        public void insertAgent(ClassAgent ag)
        {
            InitializeConnection();
            try
            {
                string q = "insert into agent(matricule,nom,postnom,prenom,adresse,email,tel,solde,password) values(@matricule,@nom,@postnom,@prenom,@adresse,@email,@tel,@solde,@password)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@matricule", ag.Matricule));
                cmd.Parameters.Add(new MySqlParameter("@nom", ag.Nom));
                cmd.Parameters.Add(new MySqlParameter("@postnom", ag.Postnom));
                cmd.Parameters.Add(new MySqlParameter("@prenom", ag.Prenom));
                cmd.Parameters.Add(new MySqlParameter("@adresse", ag.Adresse));
                cmd.Parameters.Add(new MySqlParameter("@email", ag.Email));
                cmd.Parameters.Add(new MySqlParameter("@tel", ag.Tel));
                cmd.Parameters.Add(new MySqlParameter("@solde", ag.Solde));
                cmd.Parameters.Add(new MySqlParameter("@password", ag.Password));
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Insertion effectuee avec succes!!");
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

        public void updateAgent(ClassAgent ag)
        {
            InitializeConnection();
            try
            {
                string q = "update agent set nom=@nom,postnom=@postnom,prenom=@prenom,adresse=@adresse,email=@email,tel=@tel,solde=@solde,password=@password where matricule=@matricule";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@matricule", ag.Matricule));
                cmd.Parameters.Add(new MySqlParameter("@nom", ag.Nom));
                cmd.Parameters.Add(new MySqlParameter("@postnom", ag.Postnom));
                cmd.Parameters.Add(new MySqlParameter("@prenom", ag.Prenom));
                cmd.Parameters.Add(new MySqlParameter("@adresse", ag.Adresse));
                cmd.Parameters.Add(new MySqlParameter("@email", ag.Email));
                cmd.Parameters.Add(new MySqlParameter("@tel", ag.Tel));
                cmd.Parameters.Add(new MySqlParameter("@solde", ag.Solde));
                cmd.Parameters.Add(new MySqlParameter("@password", ag.Password));

                if (cmd.ExecuteNonQuery() == 1)
                {
                    //MessageBox.Show("Modification effectuee avec succes!!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

        }
        public void deleteAgent(ClassAgent ag)
        {
            InitializeConnection();
            try
            {
                string q = "delete from agent where matricule=@matricule";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@matricule", ag.Matricule));


                DialogResult result = MessageBox.Show("Voulez-vous vraiment excuter cette operation ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Suppression avec succes!!");

                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

        }
        public DataSet sortietTransactBus()
        {

            try
            {
                InitializeConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("SELECT id,ref_bus,ref_client, SUM(montant) as montant,SUM(montant - (fraisTransact + commission)) as commission, SUM(fraisTransact + commission) as fraisTransact  FROM `transaction` GROUP BY `ref_bus` ", con);
                //cmd = new MySqlCommand("SELECT * from transact ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "transact");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortietHistoriqueTransact()
        {

            try
            {
                InitializeConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("SELECT * from rechargehistory", con);
                //cmd = new MySqlCommand("SELECT * from transact ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "rechargehistory");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortietFichecompte()
        {

            try
            {
                InitializeConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("SELECT * from compte", con);
                //cmd = new MySqlCommand("SELECT * from transact ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "compte");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortietFicheCompagnie()
        {

            try
            {
                InitializeConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("SELECT * from compagnie", con);
                //cmd = new MySqlCommand("SELECT * from transact ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "compagnie");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public void insertUsers(ClassRegister reg)
        {
            try
            {
                InitializeConnection();
                string q = "insert into users(nom,telephone,name,password) value(@nom,@telephone,@name,@password)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@nom", reg.Nom));
                cmd.Parameters.Add(new MySqlParameter("@telephone", reg.Telephone));
                cmd.Parameters.Add(new MySqlParameter("@name", reg.Users));
                cmd.Parameters.Add(new MySqlParameter("@password", reg.Users));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Creation de l'utilisateur avec succes");

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void modifierUsers(ClassRegister reg)
        {
            try
            {
                InitializeConnection();
                string q = "update users set nom=@nom,telephone=@telephone,name=@name,password=@password where id=@id";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@id", reg.Id));
                cmd.Parameters.Add(new MySqlParameter("@nom", reg.Nom));
                cmd.Parameters.Add(new MySqlParameter("@telephone", reg.Telephone));
                cmd.Parameters.Add(new MySqlParameter("@name", reg.Users));
                cmd.Parameters.Add(new MySqlParameter("@password", reg.Users));
                cmd.ExecuteNonQuery();

                MessageBox.Show("Modification avec succes!!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public Boolean LoginTest(string username, string password)
        {
            Boolean b = false;

            try
            {
                InitializeConnection();

                cmd = new MySqlCommand("SELECT name, password FROM users where name ='" + username + "' AND password = '" + password + "'", con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    b = true;
                }

                if (b == true)
                {
                    //MessageBox.Show("La connection a reussie !");
                    b = true;
                    
                }
                else
                {
                    MessageBox.Show("Echec de Connexion mot de passe ou utilisateur introuvable");
                    b = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return b;
        }
        

        public void deleteLogin(ClassRegister reg)
        {
            try
            {
                InitializeConnection();
                string q = "delete from users where id=@id";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@id", reg.Id));

                DialogResult result = MessageBox.Show("Voulez-vous vraiment excuter cette operation ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Suppression avec succes!!");

                    }
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void chargerComboRefCompagnie(System.Windows.Forms.ComboBox data)
        {
            try
            {
                InitializeConnection();

                string req = "SELECT `ref_compagnie`  FROM `compte`";
                cmd = new MySqlCommand(req, con);

                dr = cmd.ExecuteReader();
                data.Items.Clear();

                while (dr.Read())
                {
                    data.Items.Add(dr["ref_compagnie"].ToString());

                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }


        }
        public string getSolde(string ref_c)
        {
            string solde = "Null";
            try
            {
                InitializeConnection();

                string req = "SELECT `solde`  FROM `compte` WHERE ref_compagnie = '"+ref_c+"'";
                cmd = new MySqlCommand(req, con);

                dr = cmd.ExecuteReader();
              
                if (dr.Read())
                {
                    solde = (dr["solde"].ToString());

                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return solde;
        }
        public string getCompta(string ref_c)
        {
            string desi = "|";
            try
            {
                InitializeConnection();

                string req = "SELECT `designation`  FROM `compte` WHERE ref_compagnie = '" + ref_c + "'";
                cmd = new MySqlCommand(req, con);

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    desi = (dr["designation"].ToString());

                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return desi;
        }

        public void insertTranfer(ClassTransfert bq)
        {
             try
            {
                InitializeConnection();
                string q = "insert into transfertbq(bordereau,beneficiaire,montant) value(@bordereau,@beneficiaire,@montant)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@bordereau", bq.Bordereau));
                cmd.Parameters.Add(new MySqlParameter("@beneficiaire", bq.Beneficiaire));
                cmd.Parameters.Add(new MySqlParameter("@montant", bq.Montant));
               

                if(cmd.ExecuteNonQuery() ==1)
                {
                    //MessageBox.Show("Operation effectuée avec succes");
                    cmd.Dispose();
                    con.Close();
                    decimal mont = 0;
                   updateSolde(bq.Beneficiaire, mont);
                }
                else
                {
                    MessageBox.Show("Verifier la connection svp!");
                }
                

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void updateSolde(string code, decimal montant)
        {
            
            try
            {
                InitializeConnection();
                string q = "update compte set solde=@solde where designation=@designation";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@designation", code));
                cmd.Parameters.Add(new MySqlParameter("@solde", montant));
                cmd.ExecuteNonQuery();

                MessageBox.Show("Modification du solde avec succes!!");
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public DataSet sortietHistoriquetTransfertBq()
        {

            try
            {
                InitializeConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("SELECT * from transfertbq", con);
                //cmd = new MySqlCommand("SELECT * from transact ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "transfertbq");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }

        

        public DataSet sortieFiltreTransc(string dat1, string dte2)
        {
            try
            {
                InitializeConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("SELECT * from transaction where Date_Format(dateTransact,'%d/%m/%Y') between '" + dat1 + "' and '" + dte2 + "'", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "transaction");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieTranferts()
        {
            try
            {
                InitializeConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("SELECT * from compte",con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "compte");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public void Backup(string chemin)
        {
            //InitializeConnection();
            //string file = "C:\\backupEasytogo.sql";
         
            //    using (MySqlCommand cmd = new MySqlCommand())
            //    {
            //        using (MySqlBackup mb = new MySqlBackup(cmd))
            //        {
            //            cmd.Connection = con;
            //            con.Open();
            //            mb.ExportToFile(file);
            //            con.Close();
            //        }
            //    }
            }
        public void updatePoorcentage(string comm, string pource, string design)
        {
            try
            {
                InitializeConnection();
                string q = "update commission set commission=@commission, fraistransact=@fraistransact where designation=@designation";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@commission", comm));
                cmd.Parameters.Add(new MySqlParameter("@fraistransact", pource));
                cmd.Parameters.Add(new MySqlParameter("@designation", "Paiement"));
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Modification des commissions et frais de transactions  effectées avec succes");

                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void getComm(TextBox t1, TextBox t2)
        {
            try
            {
                InitializeConnection();
                string q = "select commission,fraistransact from commission";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    t1.Text = (dr["commission"].ToString());
                    t2.Text = (dr["fraistransact"].ToString());

                }


            }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
        
        #endregion



    }

       
  
    

    

}