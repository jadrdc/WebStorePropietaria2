using BookApp.AbstractBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BookApp.ManagerImpl
{
    public class GenreImpXMLManager : GenreManager
    {
        private XDocument doc;
        private const String file = "C:\\Users\\20151303\\Desktop\\BookApp\\BookApp\\genre.xml";

        public GenreImpXMLManager()
        {
            doc = XDocument.Load(file);
        }
        public void Add(Genre genre)
        {
            XElement emp = new XElement("Genre",
          new XElement("id", new Random().Next()),
          new XElement("Description",genre.Description));
            doc.Root.Add(emp);
            doc.Save(file);
            
        }
        public  void Delete(Genre genre)
        {
            XElement genrDoc = doc.Descendants("Genre").FirstOrDefault(p => p.Element("id").Value == Convert.ToString(genre.Id));
            genrDoc.Remove();
            doc.Save(file);

        }
        public  Genre FindOne(long id)
        {
            Genre genre = new Genre();

            try
            {
                XElement emp = doc.Descendants("Genre").FirstOrDefault(p => p.Element("id").Value == Convert.ToString(id));
                genre.Id =  Convert.ToInt32(emp.Element("id").Value);
                genre.Description =emp.Element("Description").Value;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            return genre;
        }
        public  void Update(int id, Genre genre)
        {

            XElement emp = doc.Descendants("Genre").FirstOrDefault(p => p.Element("id").Value == Convert.ToString(genre.Id));

                emp.Element("id").Value = Convert.ToString(genre.Id);
                emp.Element("Description").Value = genre.Description;
                emp.Remove();
                doc.Save(file);
                doc.Root.Add(emp);
                doc.Save(file);

        }
        public  IEnumerable<Genre> FindAll()
        {
            var bind = doc.Descendants("Genre").Select(p => new Genre
            {
                Id = Convert.ToInt32(p.Element("id").Value),
                Description = p.Element("Description").Value

            });

            return bind;
        }

    }
}