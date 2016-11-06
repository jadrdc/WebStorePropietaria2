using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BookApp.ManagerImpl
{
    public class AuthorImpXMLManager : AuthorManager
    {

        private XDocument doc;
        private const String file = "C:\\Users\\20151303\\Desktop\\BookApp\\BookApp\\author.xml";


        public AuthorImpXMLManager()
        {
            doc = XDocument.Load(file);
        }

        public void Add(Author author)
        {
            XElement element = new XElement("Author",  new XElement("id",author.Id), new XElement("Name", author.Name), new XElement("LastName", author.LastName), new XElement("BirthDate", author.BirthDate));

            doc.Root.Add(element);
            doc.Save(file);
        
        }

        public void Delete(Author author)
        {
            XElement elemet = doc.Descendants("Author").FirstOrDefault(p=>p.Element("id").Value== Convert.ToString(author.Id));
            elemet.Remove();
            doc.Save(file);


        }

        public Author FindOne(long id)
        {
            XElement elemet = doc.Descendants("Author").FirstOrDefault(p => p.Element("id").Value == Convert.ToString(id));
            Author author = new Author();
            author.Id = Convert.ToInt32(elemet.Element("id").Value);
            author.Name = elemet.Element("Name").Value;
            author.LastName = elemet.Element("LastName").Value;
            author.BirthDate = Convert.ToDateTime(elemet.Element("BirthDate").Value);


            return author;

        }

        public void Update(int id,Author author)
        {
            XElement emp = doc.Descendants("Author").FirstOrDefault(p => p.Element("id").Value == Convert.ToString(author.Id));

            emp.Element("id").Value = Convert.ToString(author.Id);
            emp.Element("Name").Value = author.Name;
            emp.Element("LastName").Value = author.LastName;
            emp.Element("BirthDate").Value = Convert.ToString(author.BirthDate);
            emp.Remove();
            doc.Save(file);
            doc.Root.Add(emp);
            doc.Save(file);
      
        }


    public IEnumerable<Author> FindAll()
        {
            var bind = doc.Descendants("Author").Select(p => new Author
            {
                Id = Convert.ToInt32(p.Element("id").Value),
                Name = p.Element("Name").Value,
                LastName=p.Element("LastName").Value,
                BirthDate=Convert.ToDateTime(p.Element("BirthDate").Value)
            });

            return bind;
        }
        
       
    }
}