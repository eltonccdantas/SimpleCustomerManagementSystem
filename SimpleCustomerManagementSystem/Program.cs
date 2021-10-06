using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SimpleCustomerManagementSystem
{
    class Program
    {
        [System.Serializable]
        struct Customer
        {
            public string name;
            public string email;
            public string ssn;
        }

        static List<Customer> customers = new List<Customer>();

        enum Menu { List = 1, Add = 2, Drop = 3, Quit = 4 }
        static void Main(string[] args)
        {
            Load();

            bool wannaquit = false;

            while(!wannaquit)//Repeats the menu while user does not decide to quit.

            {

                Console.WriteLine("Welcome to the Customer Management System!");
                Console.WriteLine("1-List\n2-Add\n3-Drop\n4-Quit");
                int intOp = int.Parse(Console.ReadLine());
                Menu option = (Menu)intOp;

                switch (option)
                {
                    case Menu.List:
                        ListAll();
                        break;
                    case Menu.Add:
                        Add();
                        break;
                    case Menu.Drop:
                        drop();
                        break;
                    case Menu.Quit:
                        wannaquit = true;
                        break;
                }
                Console.Clear();
            }
        }

        static void Add() //Collects data from console to add a customer to the list
        {
            Customer customer = new Customer();
            Console.WriteLine("Customer Registration");

            Console.WriteLine("Customer's Name: ");
            customer.name = Console.ReadLine();

            Console.WriteLine("Customer's e-mail address: ");
            customer.email = Console.ReadLine();

            Console.WriteLine("Customer's SSN");
            customer.ssn = Console.ReadLine();

            customers.Add(customer);
            Save();
            Console.WriteLine("Customer registration succeeded!\nPress enter to return to the menu.");
            Console.ReadLine();
        }

        static void ListAll()
        {

            if (customers.Count > 0)
            {

                Console.WriteLine("List of all customers registered: ");

                int i = 0;
                foreach (Customer customer in customers)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Name: {customer.name}");
                    Console.WriteLine($"Email: {customer.email}");
                    Console.WriteLine($"SSN {customer.ssn}");
                    Console.WriteLine("=====================================");
                    i++;

                }


            }//end of "if"

            else
            {
                Console.WriteLine("Not a single registration yet.");
            }

            Console.WriteLine("Press enter to return to the menu.");
            Console.ReadLine();
        }//end of "ListAll"

        static void drop()
        {
            ListAll();
            Console.WriteLine("Which ID do you want to drop from the list?");
            int id = int.Parse(Console.ReadLine());
            if(id >= 0 && id< customers.Count)
            {

                customers.RemoveAt(id);
                Save();

            }
            else
            {
                Console.WriteLine("Invalid ID, please select a valid one.");
                Console.ReadLine();

            }
        }

        static void Save()//it Saves the data in ROM, not in RAM.
        {
            FileStream stream = new FileStream("listOfCustomer.dat",FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, customers);
            stream.Close();
           
        }

        static void Load()//it Saves the data in ROM, not in RAM.
        {
            FileStream stream = new FileStream("listOfCustomer.dat", FileMode.OpenOrCreate);
            try
            {
             
                BinaryFormatter enconder = new BinaryFormatter();

                customers = (List<Customer>)enconder.Deserialize(stream);

                if(customers == null)
                {
                    customers = new List<Customer>();
                }


                
            }
            catch(Exception e)
            {
                customers = new List<Customer>();
            }
            stream.Close();
        }

    }//End of "class program".
}//End of "namespace".
