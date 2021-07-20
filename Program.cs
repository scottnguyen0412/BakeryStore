
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using BakeryStore;

namespace BakeryStore
{
    class Bread
    {
        private int size1;
        private double price1;
        private bool cheese1; 
        private bool beef1;
        private bool salad1;
        private bool opinion1;
        public Bread(BreadBuilder builder)
        {
            this.size1 = builder.Size;
            this.price1 = builder.Price;
            this.cheese1 = builder.Cheese;
            this.beef1 = builder.Beef;
            this.salad1 = builder.Salad;
            this.opinion1 = builder.Opinion;
        }
        public double GetPrice()
        {
            return price1;
        }
        public string GetDescription()
        {
            var description = new StringBuilder();
            description.Append($"{this.size1} inch Bread. ");
            if (cheese1 || beef1 || salad1 || opinion1)
            {
                description.Append($"( With "); if (cheese1) description.Append($"Cheese ");
                if (beef1) description.Append($"Beef ");
                if (salad1) description.Append($"Salad ");
                if (opinion1) description.Append($"Opinion ");
                description.Append($"). ");
            }
            description.Append($"\nPrice {this.price1:c}");
            return description.ToString();
        }
    }
    class BreadBuilder
    {
        public int Size;
        public double Price;
        public bool Cheese;
        public bool Beef;
        public bool Salad;
        public bool Opinion;
        public BreadBuilder(int size)
        {
            this.Size = size;
            this.Price = 5 + 0.90 * size;
        }
        public BreadBuilder AddCheese()
        {
            this.Cheese = true;
            this.Price += 0.60;
            return this;
        }
        public BreadBuilder AddBeef()
        {
            this.Beef = true;
            this.Price += 0.99;
            return this;
        }
        public BreadBuilder AddSalad()
        {
            this.Salad = true;
            return this;
        }
        public BreadBuilder AddOpinion()
        {
            this.Opinion = true; return this;
        }
        public Bread Build()
        {
            return new Bread(this);
        }
    }
}

    abstract class Drink
    {
        protected double price;
        protected string name;
        public double getPrice()
        {
            return price;
        }
        public string getDescription()
        {
            return $"{this.name} {this.price:c}";
        }
    }
    class Coca : Drink
    {
        public Coca()
        {
            price = 6;
            name = "Coca";
        }
    }
    class Pepsi : Drink
    {
        public Pepsi()
        {
            price = 7;
            name = "Pepsi";
        }
    }
    class Soda : Drink
    {
        public Soda()
        {
            price = 10;
            name = "Soda";
        }
    }
    class Aquafina: Drink
    {
    public Aquafina()
        {
            price = 4;
            name = "Aquafina";
        }

    }
    static class DrinkFactory
    {
        public static Drink MakeDrink(string name)
        {
            switch (name.ToLower())
            {
                case "coca":
                    return new Coca();
                case "pepsi":
                    return new Pepsi();
                case "soda":
                    return new Soda();
                case "aquafina":
                    return new Aquafina();
                default:
                    throw new ArgumentNullException("Drink", "Drink name should not be null");
            }
        }
    }



    class Order
    {
        private List<Bread> breads;
        private List<Drink> drinks;
        public Order()
        {
            breads = new List<Bread>();
            drinks = new List<Drink>();
        }
        public bool addBread(Bread bread)
        {
            if (bread == null) return false;
            else
            {
                breads.Add(bread);
                return true;
            }
        }
        public bool addDrink(Drink drink)
        {
            if (drink == null) return false;
            else
            {
                drinks.Add(drink);
                return true;
            }
        }
        public string getDescription()
        {
            var description = new StringBuilder();
            if (drinks.Count == 0 && breads.Count == 0) description.Append($"Thanks for visiting. Welcome back soon!\n");
            else
            {
                description.Append($"Your bill: \n");
                double total = 0;
                if (breads.Count != 0) foreach (Bread bread in breads)
                    {
                        description.Append($"{bread.GetDescription()}\n");
                        total += bread.GetPrice();
                    }
                if (drinks.Count != 0)
                    foreach (Drink drink in drinks)
                    {
                        description.Append($"{drink.getDescription()}\n");
                        total += drink.getPrice();
                    }
                description.Append($"Total: {total:c}\n");
                description.Append($"Thanks for buying!\n");
            }
            return description.ToString();
        }
        public Bread orderBread()
        {
            BreadBuilder breadBuilder;
            Console.Write("Input your size: ");
            int size = -1;
            while (size < 0)
            {
                try
                {
                    size = Int16.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.Write("Invalid size!");
                }
            }
            breadBuilder = new BreadBuilder(size);
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Do you want to add anything?");
            Console.WriteLine("1) Cheese");
            Console.WriteLine("2) Beef");
            Console.WriteLine("3) Salad");
            Console.WriteLine("4) Opinion");
            Console.WriteLine("5) Thank you, accomplish!");
            Console.WriteLine("-----------------------------");

        int choice = -1;
            while (choice != 5)
            {
                Console.Write("Your add choice: ");
                try
                {
                    choice = Int16.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    choice = -1;
                }
                switch (choice)
                {
                    case 1:
                        breadBuilder.AddCheese();
                        break;
                    case 2:
                        breadBuilder.AddBeef();
                        break;
                    case 3:
                        breadBuilder.AddSalad();
                        break;
                    case 4:
                        breadBuilder.AddOpinion();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Your choice do not inside menu. Please try again !!!!");
                        break;
                }
            }
            return breadBuilder.Build();
        }
        public Drink orderDrink()
        {
            Console.WriteLine("1) Coca");
            Console.WriteLine("2) Pepsi");
            Console.WriteLine("3) Soda");
            Console.WriteLine("4) Aquafina");
            Console.Write("Please, Choose your drink you want: ");
            int choice = 1;
            try
            {
                choice = Int16.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid choice! Order Coca automatically!");
            }
            switch (choice)
            {
                case 2:
                    return DrinkFactory.MakeDrink("Pepsi");
                case 3:
                    return DrinkFactory.MakeDrink("Soda");
                case 4:
                return DrinkFactory.MakeDrink("Aquafina");
                default: return DrinkFactory.MakeDrink("Coca");
            }
        }
        public void GetOrder()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("| Welcome to Khoi Bread Store           |");
            Console.WriteLine("|Do you wanna order burger or drink?    |");
            Console.WriteLine("|1. Bread                               |");
            Console.WriteLine("|2. Drink                               |");
            Console.WriteLine("|3. Get bill and pay!!                  |");
            Console.WriteLine("=========================================");

        int choice = -1;
            while (choice != 3)
            {
                Console.Write("Your menu choice: ");
                try
                {
                    choice = Int16.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    choice = -1;
                }
                switch (choice)
                {
                    case 1:
                        addBread(orderBread());
                        break;
                    case 2:
                        addDrink(orderDrink());
                        break;
                    case 3:
                        break;
                    default:
                        Console.Write("Your choice is not in menu!");
                        break;
                }
            }
            Console.Write(this.getDescription());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
           Order customerOrder = new Order();
           customerOrder.GetOrder();
            Console.ReadKey();
        }
    }

