using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace LabRab14
{
    public class Program
    {
        static void Main(string[] args)
        {
            Vehicle car = new Vehicle() { wheit = 10 };
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("car.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, car);

                Console.WriteLine("Объект сериализован");
            }
            using (FileStream fs = new FileStream("car.dat", FileMode.OpenOrCreate))
            {
                Vehicle newCar = (Vehicle)formatter.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.WriteLine(newCar.wheit);
            }
            SoapFormatter c = new SoapFormatter();
            using (FileStream fs = new FileStream("car.soap", FileMode.OpenOrCreate))
            {
                c.Serialize(fs, car);

                Console.WriteLine("Объект сериализован");
            }
            using (FileStream fs = new FileStream("car.soap", FileMode.OpenOrCreate))
            {
                Vehicle newCar = (Vehicle)c.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.WriteLine(newCar.wheit);
            }
            XmlSerializer xml = new XmlSerializer(typeof(Vehicle));
            using (FileStream fs = new FileStream("car.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, car);
                Console.WriteLine("Объект сериализован");
            }
            using (FileStream fs = new FileStream("car.xml", FileMode.OpenOrCreate))
            {
                Vehicle newCar = (Vehicle)xml.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.WriteLine(newCar.wheit);
            }

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Vehicle));

            using (FileStream fs = new FileStream("Car.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, car);
                Console.WriteLine("Объект сериализован");
            }

            using (FileStream fs = new FileStream("Car.json", FileMode.OpenOrCreate))
            {
                Vehicle newCar = (Vehicle)jsonFormatter.ReadObject(fs);
                Console.WriteLine("Объект десериализован");
                Console.WriteLine(newCar.wheit);
            }
            XmlSerializer xmlList = new XmlSerializer(typeof(List<Vehicle>));
            List<Vehicle> cars = new List<Vehicle>(3);
            cars.Add(new Vehicle { wheit = 20 });
            cars.Add(new Vehicle { wheit = 30 });
            cars.Add(new Vehicle { wheit = 40 });
            using (FileStream fs = new FileStream("cars.xml", FileMode.OpenOrCreate))
            {

                xmlList.Serialize(fs, cars);


                Console.WriteLine("Объект сериализован");
            }

            Console.WriteLine();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("car.xml");
            XmlElement xRoot = xDoc.DocumentElement;


            XmlNodeList childnodes = xRoot.SelectNodes("*");
            foreach (XmlNode n in childnodes)
                Console.WriteLine(n.OuterXml);

            Console.WriteLine();

            XmlNodeList childnodes_ = xRoot.SelectNodes("//Vehicle/wheit");
            foreach (XmlNode n in childnodes_)
                Console.WriteLine(n.InnerText);


            XDocument xdoc = new XDocument(new XElement("Cars",
                new XElement("car", new XAttribute("name", "Peugeot 3008"),
                new XElement("company", "Peugeot"), new XElement("price", "15000")),
                new XElement("car", new XAttribute("name", "Volkswagen Golf"),
                new XElement("company", "Volkswagen"), new XElement("price", "30000"))));
            xdoc.Save("Cars.xml");
            XDocument xmldoc = XDocument.Load("Cars.xml");
            Console.WriteLine("Первый Linq to Xml запрос:\n");
            foreach (XElement phoneElement in xmldoc.Element("Cars").Elements("car"))
            {
                XAttribute nameAttribute = phoneElement.Attribute("name");
                XElement companyElement = phoneElement.Element("company");
                XElement priceElement = phoneElement.Element("price");
                if (nameAttribute != null && companyElement != null && priceElement != null)
                {
                    Console.WriteLine("Car: {0}", nameAttribute.Value);
                    Console.WriteLine("Company: {0}", companyElement.Value);
                    Console.WriteLine("Price: {0}", priceElement.Value);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nВторой Linq to Xml запрос:\n");
            foreach (XElement phoneElement in xmldoc.Element("Cars").Elements("car"))
            {
                XAttribute nameAttribute = phoneElement.Attribute("name");
                XElement companyElement = phoneElement.Element("company");
                XElement priceElement = phoneElement.Element("price");
                if (nameAttribute != null && companyElement != null && priceElement.Value == "15000")
                {
                    Console.WriteLine("Car: {0}", nameAttribute.Value);
                    Console.WriteLine("Company: {0}", companyElement.Value);
                    Console.WriteLine("Price: {0}", priceElement.Value);
                }
                Console.Read();
            }
        }
        public interface ICarControl
        {
            int grip { get; set; }
            int braking_distances { get; set; }

        }
        [Serializable]
        public class Vehicle : ICarControl
        {
            public int wheit { get; set; }
            public string classification_group { get; set; }
            public int grip { get; set; }
            public int braking_distances { get; set; }
            public override string ToString()
            {
                return "ICarControl.Vechicle";

            }
        }
    }
}

