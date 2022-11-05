namespace Lesson
{
    public class Program
    {
        public static Dictionary<string, decimal> menuItems = new Dictionary<string, decimal> 
        {
            { "Cheesecake", 300 },
            { "Frozen Yoghurt", 29 },
            { "Pizza", 0 },
            { "Black Label", 19 },
            { "Bioplus", 14 },
            { "Carrot cake", 170 }
        };

        private static Bill? bill;

        public static void Main() 
        {
            Console.WriteLine("Welcome to Bill Manager!");
            Console.Write("Enter Bill Name: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("that is not a name.");
            }
            else 
            {
                bill = new Bill(name);

                if (bill != null) PromtOptions();
            }
        }

        public static void PromtOptions()
        {
            Console.Write("Choose an option (a - add item / t - update tip / s - save bill): ");
            string? option = Console.ReadLine();

            switch (option) 
            {
                case "a":
                    AddItem();
                    break;
                case "t":
                    UpdateTip();
                    break;
                case "s":
                    SaveBill();
                    break;
                default:
                    Console.WriteLine($"{option} is not a valid option!");
                    Console.Clear();
                    PromtOptions();
                    break;
            }
        }

        #region Options
        private static void SaveBill()
        {
            if (bill != null)
            {
                string output = bill.Format();
                Console.WriteLine(output);

                File.WriteAllText(Environment.CurrentDirectory + $"/{bill.BillName}.txt", output);
            }
        }

        private static void UpdateTip()
        {
            Console.WriteLine("Choose tip amount:");
            Console.WriteLine("1) 0%\n2) 10%\n3) 15%\n4) 20%\n5) Custom percentage");

            decimal percent = 0;
            string? op = Console.ReadLine();

            switch (op) 
            {
                case "1":
                    percent = 0;
                    break;
                case "2":
                    percent = 10;
                    break;
                case "3":
                    percent = 15;
                    break;
                case "4":
                    percent = 20;
                    break;
                case "5":
                    percent = PromptDecimal("Enter percentage: ");
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    UpdateTip();
                    break;
            }

            decimal total = bill.GetTotal();
            decimal tipAmount = percent / 100 * total;
            Console.WriteLine($"Your total tip amount is R{tipAmount}");
            bill.UpdateTip(tipAmount);
            PromtOptions();
        }

        private static void AddItem()
        {
            //prompt the user with all the items available
            //allow the user to select an item with a number
            //eg.
            //1) Cheesecake - R300
            //2) Frozen Yoghurt - R29

            Console.WriteLine("Choose an item to add:");
            for (int i = 0; i < menuItems.Count; i++) 
            {
                var pair = menuItems.ElementAt(i);
                Console.WriteLine($"{i + 1}) {pair.Key} - R{pair.Value}");
            }
            //Console.Write("Enter item number: ");
            //int choice = int.Parse(Console.ReadLine()) - 1;
            int choice = PromptInt("Enter item number: ") - 1;

            if (choice >= 0 && choice < menuItems.Count)
            {
                //this is a valid index
                var item = menuItems.ElementAt(choice);
                bill.AddItem(item.Key, item.Value);
                Console.WriteLine($"Added {item.Key} to bill.");

                Console.Write("Would you like to add another item? (Y/N): ");
                string? op = Console.ReadLine().ToLower();

                if (op == "y") AddItem();
                else PromtOptions();
            }
            else 
            {
                //this is not a valid index
                Console.WriteLine("Invalid entry.");
                Console.Clear();
                AddItem();
            }
        }
        #endregion

        public static int PromptInt(string prompt) 
        {
            bool isNumber = false;
            int input = 0;

            while (!isNumber)
            {
                Console.Write(prompt);
                try
                {
                    input = int.Parse(Console.ReadLine()!);
                }
                catch
                {
                    Console.WriteLine("That's not a number! Please try again.");
                    continue;
                }

                isNumber = true;
            }

            return input;
        }

        public static decimal PromptDecimal(string prompt)
        {
            bool isNumber = false;
            decimal input = 0;

            while (!isNumber)
            {
                Console.Write(prompt);
                isNumber = decimal.TryParse(Console.ReadLine(), out input);
            }

            return input;
        }
    }
}
