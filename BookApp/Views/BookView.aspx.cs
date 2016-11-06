using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookApp.ManagerImpl;
using BookApp.AbstractBehavior;

namespace BookApp.Views
{
    public partial class BookView : System.Web.UI.Page
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

            BookManager manager = new BookImpXMLManager();
            GridView1.DataSource = manager.FindAll();
            GridView1.DataBind();



        }

        private void FillComboBox(GridViewRowEventArgs e)
        {
            EditorialManager managerEditorial = new EditorialImpXMLManager();
            AuthorManager managerAuthor = new AuthorImpXMLManager();
            GenreManager managerGenre = new GenreImpXMLManager();


            DropDownList AuthorDrop = (DropDownList)e.Row.FindControl("AuthorDropDown");
            DropDownList GenreDrop = (DropDownList)e.Row.FindControl("GenreDropDown");
            DropDownList EditorialDrop = (DropDownList)e.Row.FindControl("EditorialDropDown");

            if (AuthorDrop != null)
            {
                AuthorDrop.DataSource = managerAuthor.FindAll();
                AuthorDrop.DataTextField = "Name";
                AuthorDrop.DataValueField = "Id";
                AuthorDrop.DataBind();


            }

            if (GenreDrop != null)
            {

                GenreDrop.DataSource = managerGenre.FindAll();
                GenreDrop.DataTextField = "Description";
                GenreDrop.DataValueField = "Id";
                GenreDrop.DataBind();

            }

            if (EditorialDrop != null)
            {

                EditorialDrop.DataSource = managerEditorial.FindAll();

                EditorialDrop.DataTextField = "Name";
                EditorialDrop.DataValueField = "Id";
                EditorialDrop.DataBind();

            }





            dpGenre.DataSource = managerGenre.FindAll();

            dpGenre.DataTextField = "Description";
            dpGenre.DataValueField = "Id";
            dpGenre.DataBind();

            dpEditorial.DataSource = managerEditorial.FindAll();

            dpEditorial.DataTextField = "Name";
            dpEditorial.DataValueField = "Id";
            dpEditorial.DataBind();


            dpAuthor.DataSource = managerAuthor.FindAll();

            dpAuthor.DataTextField = "Name";
            dpAuthor.DataValueField = "Id";
            dpAuthor.DataBind();






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
            BookManager manager = new BookImpXMLManager();
            Book book = manager.FindOne(id);
            book.Published_Date = Convert.ToDateTime(Request.Form["date"].ToString());
            book.ISBN = (row.FindControl("txtISBN") as TextBox).Text;
            book.Name = (row.FindControl("txtName") as TextBox).Text;
            book.Genre_Id = Convert.ToInt32((row.FindControl("GenreDropDown") as DropDownList).SelectedValue);
            book.Editorial_Id = Convert.ToInt32((row.FindControl("EditorialDropDown") as DropDownList).SelectedValue);
            book.Author_Id = Convert.ToInt32((row.FindControl("AuthorDropDown") as DropDownList).SelectedValue);
            manager.Update(id, book);
            GridView1.EditIndex = -1;
            this.BindGrid();
        }
        protected void Insert(object sender, EventArgs e)
        {

            if (!(String.IsNullOrWhiteSpace(txtName.Text) && String.IsNullOrWhiteSpace(txtISBN.Text)))
            {
                BookManager manager = new BookImpXMLManager();
                 EditorialManager managerEditorial = new EditorialImpXMLManager();
            AuthorManager managerAuthor = new AuthorImpXMLManager();
            GenreManager managerGenre = new GenreImpXMLManager();

                Book book = new Book();


                try
                {

                    book.Author_Id = Convert.ToInt32(dpAuthor.SelectedItem.Value);
                    book.Name = txtName.Text;
                    book.ISBN = txtISBN.Text;
                    book.Published_Date = Convert.ToDateTime(Request.Form["date"].ToString());
                    book.Editorial_Id = Convert.ToInt32(dpEditorial.SelectedItem.Value);
                    book.Genre_Id = Convert.ToInt32(dpGenre.SelectedItem.Value);
                    book.Genre = managerGenre.FindOne(Convert.ToInt32(dpGenre.SelectedItem.Value));
                    book.Editorial = managerEditorial.FindOne(Convert.ToInt32(dpEditorial.SelectedItem.Value));
                    book.Author = managerAuthor.FindOne(Convert.ToInt32(dpAuthor.SelectedItem.Value));

                    manager.Add(book);

                }
                catch (Exception exception)
                {

                    Response.Redirect("BookView.aspx");

                }


                errorMe.Text = "";
            }
            else
            {
                errorMe.Text = "NO se pueden dejar campos vacios";
            }


            this.BindGrid();

        }


        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            BookManager manager = new BookImpXMLManager();
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            Book book = manager.FindOne(id);
            manager.Delete(book);



            this.BindGrid();
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {




            FillComboBox(e);

        }
















    }


}