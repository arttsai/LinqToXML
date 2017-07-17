using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace xml_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. writing XML literally 
//            LiteralXML();
            // 2. reading data using LinQ and writing XML
//            FromStudentToXML();
            // 3. reading XML using LinQ 
            ReadXmlByLinq();
            // 4. adding element by LinQ 
            // 5. modifying XML using LinQ, and output XML without format 
        }

        private static void LiteralXML()
        {
            var xml = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("This is a comment"),
                new XElement("Students",
                    new XElement("Student", new XAttribute("Id", "101"),
                        new XElement("Name", "Mark"),
                        new XElement("Gender", "Male"),
                        new XElement("TotalMarks", "800")
                    ),
                    new XElement("Student", new XAttribute("Id", "102"),
                        new XElement("Name", "Rosy"),
                        new XElement("Gender", "Female"),
                        new XElement("TotalMarks", "900")
                    )
                )
            );

            xml.Save("data.xml");
        }

        public static void FromStudentToXML()
        {
            var xml = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"), 
                new XComment("This is a comment!"), 
                new XElement("Students",
                    from s in Student.GetAllStudents()
                    select new XElement("Student", new XAttribute("Id", s.Id),
                        new XElement("Name", s.Name), 
                        new XElement("Gender", s.Gender), 
                        new XElement("TotalMarks", s.TotalMarks)
                        )
                    )
                );
            xml.Save("students.xml");
        }

        public static void ReadXmlByLinq()
        {
            // read all names from xml data file 

            IEnumerable<string> names = from student in XDocument
                    .Load(
                        @"C:\Users\art.tsai35\Documents\Visual Studio 2013\Projects\xml practice\xml practice\bin\Debug\data.xml")
                    .Descendants("Student")
                where (int) student.Element("TotalMarks") > 800
                orderby (int) student.Element("TotalMarks") descending
                select student.Element("Name").Value;

            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}

