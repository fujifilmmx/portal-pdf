using System;
using System.IO;
using pdfTextExctractorConsole;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pdfTextExctractor
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Submit1.ServerClick += new System.EventHandler(this.Submit1_ServerClick);
            this.Load += new System.EventHandler(this.Page_Load);
        }
        private void Submit1_ServerClick(object sender, System.EventArgs e)
        {
            if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
            {
                if (!Directory.Exists(Server.MapPath("Data")))
                    Directory.CreateDirectory(Server.MapPath("Data"));
                if (!Directory.Exists(Server.MapPath("Converted")))
                    Directory.CreateDirectory(Server.MapPath("Converted"));

                string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                string SaveLocation = Server.MapPath("Data") + "\\" + fn;
                string ConvertedFileLocation = Server.MapPath("Converted") + "\\" + fn; 
                try
                {
                    File1.PostedFile.SaveAs(SaveLocation);
                    Pdf.ReplaceTextInPdfFile(SaveLocation, ConvertedFileLocation, txtOld.Value, txtNew.Value);
                    byte[] Content = File.ReadAllBytes(ConvertedFileLocation); //missing ;
                    Response.ContentType = "text/csv";
                    Response.AddHeader("content-disposition", "attachment; filename=" + fn);
                    Response.BufferOutput = true;
                    Response.OutputStream.Write(Content, 0, Content.Length);
                    Response.End();
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                    //Note: Exception.Message returns a detailed message that describes the current exception. 
                    //For security reasons, we do not recommend that you return Exception.Message to end users in 
                    //production environments. It would be better to return a generic error message. 
                }
            }
            else
            {
                Response.Write("Please select a file to upload.");
            }
        }
    }
}