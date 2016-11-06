using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookApp.ManagerImpl;

namespace BookApp.Views
{
    public partial class AuthorsInfo : System.Web.UI.Page
    {
        private AuthorManager authorManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }

        }

        protected void Insert(object sender, EventArgs e)
        {
            if (!(String.IsNullOrWhiteSpace(txtName.Text) && String.IsNullOrWhiteSpace(txtLastName.Text)))
            {
                authorManager = new AuthorImpXMLManager();
               
                Author author  = new Author()
                {
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    BirthDate = Convert.ToDateTime(Request.Form["txtBirthdate"])
                  };
                authorManager.Add(author);
                  errorMe.Text = "";
                }
            else
            {
                errorMe.Text = "No se pueden dejar campos vacios";
            }


            this.BindGrid();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                int authorId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                AuthorManager manager = new AuthorImpXMLManager();
                Author author=manager.FindOne(authorId);
                 manager.Delete(author);

                this.BindGrid();
            }
            catch (Exception)
            {
                Response.Redirect("AuthorsInfo.aspx");
            }
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int authorId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            AuthorManager manager = new AuthorImpXMLManager();
            Author author= new Author();
            string date = Request.Form["txtBirthdate"];
            author.Id=authorId;
            author.Name = (row.FindControl("txtName") as TextBox).Text;
            author.LastName = (row.FindControl("txtLastName") as TextBox).Text;
            author.BirthDate = Convert.ToDateTime(date);
            manager.Update(authorId,author);
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();

        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridView1.EditIndex)
            {
 }
        }


        private void BindGrid()
        {
            AuthorManager manager = new AuthorImpXMLManager();
            GridView1.DataSource = manager.FindAll();
                GridView1.DataBind();
           
        }
    }
}