using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.Logic
{
    public class ValidateExcel
    {
        
        public static bool CheckInstalledExcel()
        {
            var rutaDirectory = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs";
            bool validateState = false;
            var GetFiles = Directory.GetFiles(rutaDirectory);
            var GetFolders = Directory.GetDirectories(rutaDirectory);

            foreach (var item in GetFiles)
            {
                if (item.Contains("Excel") == true)
                {
                    return true;
                }
                else
                {
                    validateState = false;
                }
            }
            foreach (var item in GetFolders)
            {
                var rutaFolders=Path.Combine(rutaDirectory, item);
                var FilesSubDirectory = Directory.GetFiles(rutaFolders);
                foreach (var subFiles in FilesSubDirectory)
                {
                    if (subFiles.Contains("Excel") == true)
                    {
                        return true;
                    }
                    else
                    {
                        validateState = false;
                    }
                }
            }

            return validateState;
        }
    }
}
