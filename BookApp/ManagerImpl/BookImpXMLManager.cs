using BookApp.AbstractBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BookApp.ManagerImpl
{
    public class BookImpXMLManager : BookManager
    {
       private XDocument doc;
        private const String  file ="C:\\Users\\20151303\\Desktop\\BookApp\\BookApp\\book.xml";
       
        
        
        public BookImpXMLManager()
        {
            doc = XDocument.Load(file);
        }

        

        public void Add(Book book)
        {
            XElement genre = new XElement("Genre", new XElement("id", book.Genre.Id), new XElement("Description", book.Genre.Description));
            XElement author = new XElement("Author", new XElement("id", book.Author.Id), new XElement("Name", book.Author.Name), new XElement("LastName", book.Author.LastName), new XElement("BirthDate", book.Author.BirthDate));
            XElement editorial = new XElement("Editorial", new XElement("id", book.Editorial.Id), new XElement("Name", book.Editorial.Name));
            XElement element = new XElement("Book", new XElement("id", new Random().Next()), new XElement("ISBN", book.ISBN), new XElement("Name", book.Name), 
                new XElement("PublishedDate", book.Published_Date),
                genre,author,editorial);
            doc.Root.Add(element);
            doc.Save(file);

        }

        public void Delete(Book book)
        {

            XElement elemet = doc.Descendants("Book").FirstOrDefault(p => p.Element("id").Value == Convert.ToString(book.Id));
            elemet.Remove();
            doc.Save(file);

        }

        public Book FindOne(long id)
        {
         XElement p = doc.Descendants("Book").FirstOrDefault(bookToFind => bookToFind.Element("id").Value == Convert.ToString(id));
         Book book = new Book();
         book.Id = Convert.ToInt32(p.Element("id").Value);
         book.Name = p.Element("Name").Value;
         book.ISBN = p.Element("ISBN").Value;
         book.Published_Date = Convert.ToDateTime(p.Element("PublishedDate").Value);
         book.Genre = new Genre()
         {
             Id = Convert.ToInt32(p.Element("Genre").Element("id").Value),
             Description = p.Element("Genre").Element("Description").Value
         };
        book.Author= new Author()
                {
                    Id = Convert.ToInt32(p.Element("Author").Element("id").Value),
                    Name = p.Element("Author").Element("Name").Value,
                    LastName = p.Element("Author").Element("LastName").Value,
                    BirthDate = Convert.ToDateTime(p.Element("Author").Element("BirthDate").Value)
                };
         book.Editorial = new Editorial()
                {
                    Id = Convert.ToInt32(p.Element("Editorial").Element("id").Value),
                    Name = p.Element("Editorial").Element("Name").Value
                };



         return book;
        }

        public void Update(int id, Book book)
        {
            XElement emp = doc.Descendants("Book").FirstOrDefault(p => p.Element("id").Value == Convert.ToString(book.Id));

            emp.Element("id").Value = Convert.ToString(book.Id);
            emp.Element("Name").Value = book.Name;
            emp.Element("ISBN").Value = book.ISBN;
            emp.Element("PublishedDate").Value = Convert.ToString(book.Published_Date);
            emp.Element("Genre").Element("id").Value = Convert.ToString(book.Genre.Id);
            emp.Element("Genre").Element("Description").Value = Convert.ToString(book.Genre.Description);
            emp.Element("Author").Element("Name").Value = book.Author.Name;
            emp.Element("Author").Element("LastName").Value = book.Author.LastName;
            emp.Element("Author").Element("BirthDate").Value = Convert.ToString(book.Author.BirthDate);
            emp.Element("Editorial").Element("id").Value = Convert.ToString(book.Editorial.Id);
            emp.Element("Editorial").Element("Name").Value = Convert.ToString(book.Editorial.Name);
            emp.Remove();
            doc.Save(file);
            doc.Root.Add(emp);
            doc.Save(file);
      

        }


        public IEnumerable<Book> FindAll()
        {
            IEnumerable<Book> list=null;
            
            try
            {
                list = doc.Descendants("Book").Select(p => new Book
                {
                    Id = Convert.ToInt32(p.Element("id").Value),
                    Name = p.Element("Name").Value,
                    ISBN = p.Element("ISBN").Value,
                    //Published_Date = Convert.ToDateTime(p.Element("PublishedDate").Value),
                    Genre = new Genre()
                    {
                        Id = Convert.ToInt32(p.Element("Genre").Element("id").Value),
                        Description = p.Element("Genre").Element("Description").Value
                    },
                    Author = new Author()
                    {
                        Id = Convert.ToInt32(p.Element("Author").Element("id").Value),
                        Name = p.Element("Author").Element("Name").Value,
                        LastName = p.Element("Author").Element("LastName").Value/*,
                    BirthDate = Convert.ToDateTime(p.Element("Author").Element("BirthDate").Value)*/
                    },
                    Editorial = new Editorial()
                    {
                        Id = Convert.ToInt32(p.Element("Editorial").Element("id").Value),
                        Name = p.Element("Editorial").Element("Name").Value
                    }

                });
            }
            catch (Exception e)
            {

            }
            return list;
        }

       
    }
}