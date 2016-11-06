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
    public partial class EditorialView : System.Web.UI.Page
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

            EditorialManager manager = new EditorialImpXMLManager();
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
            string name = (row.FindControl("txtName") as TextBox).Text;
            EditorialManager manager = new EditorialImpXMLManager();
            Editorial editorial = manager.FindOne(id);
            editorial.Name = name; 
            manager.Update(id, editorial);
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void Insert(object sender, EventArgs e)
        {
            if (!(String.IsNullOrWhiteSpace(txtName.Text))) {
                EditorialManager manager = new EditorialImpXMLManager();
                Editorial editorial= new Editorial();
                editorial.Name=txtName.Text;
                manager.Add(editorial);
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

                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

                EditorialManager manager = new EditorialImpXMLManager();
                Editorial editorial =manager.FindOne(id);
                manager.Delete(editorial);
                this.BindGrid();
            }
            catch (Exception)
            {
                Response.Redirect("EditorialView.aspx");
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