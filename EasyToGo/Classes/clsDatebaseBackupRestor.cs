using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EasyToGo.Classes;

namespace EasyToGo.Classes
{
    class clsDatebaseBackupRestor
    {
        private static string backupPath = "";

        public String getBackupPath()
        {
        
                //backupPath = @"C:\BackupEcole";
                backupPath = Constants.Database.Backup;
                try
                {
                    if (Directory.Exists(backupPath))
                    {
                        return backupPath;
                    }
                    DirectoryInfo di = Directory.CreateDirectory(backupPath);
                    backupPath = di.FullName;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            
          
            return backupPath;
        }//Pour le selectionner un Chemin d
      
        //Pour le selectionner un Chemin d
    }
}
