using BookApp.AbstractBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BookApp.ManagerImpl
{
    public class EditorialImpXMLManager : EditorialManager
    {
        private XDocument doc;
        private const String file = "C:\\Users\\20151303\\Desktop\\BookApp\\BookApp\\editorial.xml";


        public EditorialImpXMLManager()
        {
            doc = XDocument.Load(file);


        }
        public void Add(Editorial editorial)
        {

            XElement element = new XElement("Editorial", new XElement("id", new Random().Next()), new XElement("Name", editorial.Name));

            doc.Root.Add(element);

            doc.Save(file);
        
        }

        public void Delete(Editorial editorial)
        {
            XElement genrDoc = doc.Descendants("Editorial").FirstOrDefault(p => p.Element("id").Value == Convert.ToString(editorial.Id));
            genrDoc.Remove();
            doc.Save(file);
        
        }

        public Editorial FindOne(long id)
        {
            Editorial editorial = new Editorial();
            XElement emp = doc.Descendants("Editorial").FirstOrDefault(p => p.Element("id").Value == Convert.ToString(id));
            editorial.Id = Convert.ToInt32(emp.Element("id").Value);
            editorial.Name = emp.Element("Name").Value;

            return editorial;
        }

        public void Update(int id, Editorial editorial)
        {
            XElement emp = doc.Descendants("Editorial").FirstOrDefault(p => p.Element("id").Value == Convert.ToString(editorial.Id));

            emp.Element("id").Value = Convert.ToString(editorial.Id);
            emp.Element("Name").Value = editorial.Name;
            emp.Remove();
            doc.Save(file);
            doc.Root.Add(emp);
            doc.Save(file);
        }

        public IEnumerable<Editorial> FindAll()
        {
            var bind = doc.Descendants("Editorial").Select(p => new Editorial
            {
                Id = Convert.ToInt32(p.Element("id").Value),
                Name = p.Element("Name").Value

            });

            return bind;
        }
    }
}