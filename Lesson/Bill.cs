namespace Lesson
{
    public class Bill
    {
        public string BillName { get; set; }

        private List<(int, string, decimal)> items = new();
        private decimal tip;

        public Bill(string name) 
        {
            BillName = name;
        }

        public void AddItem(string name, decimal price) 
        {
            bool found = false;

            for (int i = 0; i < items.Count; i++) 
            {
                if (items[i].Item2 == name) 
                {
                    items[i] = (items[i].Item1 + 1, name, price);
                    found = true;
                }
            }

            if (!found) items.Add((1, name, price));
        }

        public decimal GetTotal() 
        {
            decimal total = 0;

            foreach (var item in items) 
            {
                total += item.Item1 * item.Item3;
            }

            return total;
        }

        public void UpdateTip(decimal tip) 
        {
            this.tip = tip;
        }

        public string Format() 
        {
            string outputBill = $"Bill {BillName}\n";

            //list items and prices
            foreach (var item in items) 
            {
                outputBill += $"{item.Item2} x {item.Item1}: R{item.Item3}\n";
            }

            //display tip and total
            outputBill += $"Tip: R{tip}\nTotal: R{GetTotal() + tip}";

            return outputBill;
        }
    }
}
