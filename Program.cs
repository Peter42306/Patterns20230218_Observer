using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns20230218_Observer
{
    // Интерфейс наблюдателя
    interface IObserver
    {
        void Update(string productName);
    }

    // Конкретный наблюдатель (покупатель)    
    class Customer : IObserver
    {
        private string _name;

        public Customer(string name)
        {
            _name = name;
        }

        public void Update(string productName)
        {
            Console.WriteLine($"Customer {_name}: Product '{productName}' is now available. Going to buy it!");
        }
    }

    // Интерфейс субъекта (магазин)
    interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string productName);
    }

    // Конкретный субъект (магазин)
    class Store : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        private string _productName;

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string productName)
        {
            foreach (var observer in _observers)
            {
                observer.Update(productName);
            }
        }

        public void NewProductArrived(string productName)
        {
            _productName = productName;
            Console.WriteLine($"New product '{productName}' has arrived in the store.");
            Notify(productName);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();

            Customer customer1 = new Customer("John");
            Customer customer2 = new Customer("Mary");

            store.Attach(customer1);
            store.Attach(customer2);

            store.NewProductArrived("Printer");

            store.Detach(customer1);

            store.NewProductArrived("Monitor");
        }
    }
}
