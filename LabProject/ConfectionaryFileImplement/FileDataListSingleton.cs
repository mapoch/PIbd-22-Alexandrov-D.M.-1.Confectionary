﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ConfectionaryContracts.Enums;
using ConfectionaryFileImplement.Models;

namespace ConfectionaryFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string PastryFileName = "Pastry.xml";
        private readonly string WarehouseFileName = "Warehouse.xml";

        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Pastry> Pastries { get; set; }
        public List<Warehouse> Warehouses { get; set; }

        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Pastries = LoadPastries();
            Warehouses = LoadWarehouses();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null) instance = new FileDataListSingleton();
            return instance;
        }

        public void SaveData()
        {
            SaveComponents();
            SaveOrders();
            SavePastries();
            SaveWarehouses();
        }

        private List<Component> LoadComponents()
        {
            var list = new List<Component>();

            if (File.Exists(ComponentFileName))
            {
                var xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();

            if (File.Exists(OrderFileName))
            {
                var xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                
                foreach (var elem in xElements)
                {
                    if (!Enum.TryParse(elem.Element("Status").Value, out OrderStatus orderStatus))
                    {
                        orderStatus = OrderStatus.Принят;
                    }

                    DateTime? orderDateImplement; 
                    if (elem.Element("DateImplement").Value == null || elem.Element("DateImplement").Value == "") 
                    { 
                        orderDateImplement = null; 
                    } 
                    else 
                    { 
                        orderDateImplement = Convert.ToDateTime(elem.Element("DateImplement").Value); 
                    }

                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PastryId = Convert.ToInt32(elem.Element("PastryId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = orderStatus,
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = orderDateImplement
                    });
                }
            }
            return list;
        }

        private List<Pastry> LoadPastries()
        {
            var list = new List<Pastry>();

            if (File.Exists(PastryFileName))
            {
                var xDocument = XDocument.Load(PastryFileName);
                var xElements = xDocument.Root.Elements("Pastry").ToList();

                foreach (var elem in xElements)
                {
                    var pastryComp = new Dictionary<int, int>();
                    foreach (var component in 
                        elem.Element("PastryComponents").Elements("PastryComponent").ToList())
                    {
                        pastryComp.Add(Convert.ToInt32(component.Element("Key").Value),
                            Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Models.Pastry
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PastryName = elem.Element("PastryName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        PastryComponents = pastryComp
                    });
                }
            }
            return list;
        }

        private List<Warehouse> LoadWarehouses()
        {
            var list = new List<Warehouse>();

            if (File.Exists(WarehouseFileName))
            {
                var xDocument = XDocument.Load(WarehouseFileName);
                var xElements = xDocument.Root.Elements("Warehouse").ToList();

                foreach (var elem in xElements)
                {
                    var storedComponents = new Dictionary<int, int>();

                    foreach (var component in
                        elem.Element("StoredComponents").Elements("StoredComponent").ToList())
                    {
                        storedComponents.Add(Convert.ToInt32(component.Element("Key").Value),
                            Convert.ToInt32(component.Element("Value").Value));
                    }

                    list.Add(new Warehouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        Name = elem.Element("Name").Value,
                        Manager = elem.Element("Manager").Value,
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        StoredComponents = storedComponents
                    });
                }
            }
            return list;
        }

        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                        new XAttribute("Id", component.Id),
                        new XElement("ComponentName", component.ComponentName)));
                }

                var xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }

        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                        new XAttribute("Id", order.Id),
                        new XElement("PastryId", order.PastryId),
                        new XElement("Count", order.Count),
                        new XElement("Sum", order.Sum),
                        new XElement("Status", order.Status),
                        new XElement("DateCreate", order.DateCreate),
                        new XElement("DateImplement", order.DateImplement)));
                }

                var xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }

        private void SavePastries()
        {
            if (Pastries != null)
            {
                var xElement = new XElement("Pastries");
                foreach (var pastry in Pastries)
                {
                    var compElement = new XElement("PastryComponents");
                    foreach (var component in pastry.PastryComponents)
                    {
                        compElement.Add(new XElement("PastryComponent",
                            new XElement("Key", component.Key),
                            new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Pastry",
                        new XAttribute("Id", pastry.Id),
                        new XElement("PastryName", pastry.PastryName),
                        new XElement("Price", pastry.Price),
                        compElement));
                }

                var xDocument = new XDocument(xElement);
                xDocument.Save(PastryFileName);
            }
        }

        private void SaveWarehouses()
        {
            if (Warehouses != null)
            {
                var xElement = new XElement("Warehouse");
                foreach (var warehouse in Warehouses)
                {
                    var storedComponents = new XElement("StoredComponents");
                    foreach (var component in warehouse.StoredComponents)
                    {
                        storedComponents.Add(new XElement("StoredComponent",
                            new XElement("Key", component.Key),
                            new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Warehouse",
                        new XAttribute("Id", warehouse.Id),
                        new XElement("Name", warehouse.Name),
                        new XElement("Manager", warehouse.Manager),
                        new XElement("DateCreate", warehouse.DateCreate),
                        storedComponents));
                }

                var xDocument = new XDocument(xElement);
                xDocument.Save(WarehouseFileName);
            }
        }
    }
}
