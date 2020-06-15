using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MundoCanjeWeb.Cpanel
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string logout = Request.QueryString["logout"];                    
                    if (logout != null)
                    {
                        if(logout=="true")
                        {
                            Session["Admin"] = null;
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
        }
        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            if ((TxbEmail.Text == "adminchris" && TxbClave.Text == "adminchris") || (TxbEmail.Text == "admin" && TxbClave.Text == "canje0421"))
            {
                Session.Add("Admin", TxbEmail.Text);                
                Response.Redirect("Default.aspx", false);

            }
            else
            {
                LblError.Text = "Usuario incorrecto.";
            }
        }
    }
}