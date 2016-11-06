using BookApp.AbstractBehavior;
using BookApp.ManagerImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookApp.Views
{
    public partial class GenreView : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindGrid();
            }

        }




        private void BindGrid()
        {
            GenreManager manager = new GenreImpXMLManager();
        
            GridView1.DataSource = manager.FindAll();
                GridView1.DataBind();
        
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }








        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string description = (row.FindControl("txtDescription") as TextBox).Text;

            Genre genre = new Genre();
            genre.Id = id;
            genre.Description = description;


            GenreManager manager = new GenreImpXMLManager();
            manager.Update(id, genre);


        
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void Insert(object sender, EventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(txtDescription.Text))
            {

                GenreManager manager = new GenreImpXMLManager();
                Genre genre = new Genre();
                genre.Description = txtDescription.Text;
                manager.Add(genre);
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
                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

                GenreManager manager = new GenreImpXMLManager();

                Genre genre = manager.FindOne(id);

                manager.Delete(genre);
                
                this.BindGrid();
            }
            catch (Exception)
            {
                Response.Redirect("GenreView.aspx");
            }
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridView1.EditIndex)
            {

                //   (e.Row.Cells[2].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
        }


















    }
}