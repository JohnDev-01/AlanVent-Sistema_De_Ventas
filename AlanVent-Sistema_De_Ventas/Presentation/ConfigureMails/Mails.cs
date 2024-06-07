using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlanVent_Sistema_De_Ventas.Logic;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Microsoft.Office.Interop;

namespace AlanVent_Sistema_De_Ventas.Presentation.ConfigureMails
{
    public partial class Mails : Form
    {
        public Mails()
        {
            InitializeComponent();
        }

        private void btnSends_Click(object sender, EventArgs e)
        {
            var correoBase1 = "johnkerlin28@outlook.es";
            var correoBase = "johnkerlin52@gmail.com";
            var contra = "Johnsilvestre";

            MailMessage oMailMessage = new MailMessage(correoBase, correoBase1, "Hola Envio Contra",
                "Esto deberia de se La contrasena");

            oMailMessage.IsBodyHtml = false;

            SmtpClient smtpClient = new SmtpClient("smtp.office365.com",587);
            smtpClient.Credentials = new NetworkCredential(correoBase1, contra);
            smtpClient.EnableSsl = true;

            smtpClient.Send(oMailMessage);
            //smtpClient.Dispose();
        }
        private void ValidateNumberToString()
        {
            try
            {
                double Number = Convert.ToDouble(txtmonto.Text);
                txtmonto.Text = Bases.AsignarComa(Number);
                txtmonto.Focus();
                txtmonto.SelectionStart = txtmonto.Text.Length;
            }
            catch (Exception ex)
            {
                if (txtmonto.Text.Length > 0)
                    txtmonto.Text = txtmonto.Text.Remove(txtmonto.Text.Length - 1);
            }
        }
        private void btnConvert_Click(object sender, EventArgs e)
        {

        }

        private void txtmonto_TextChanged(object sender, EventArgs e)
        {
            ValidateNumberToString();
            double number = Convert.ToDouble(txtmonto.Text);
            label1.Text = (number + 1).ToString();
        }
    }
}
