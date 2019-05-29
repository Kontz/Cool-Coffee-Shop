using System;
using System.Collections.Generic;
using System.Text;

namespace Cool_Coffee_Shop
{
    public class Order
    {
        private static int orderCounter = 1000;
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public List<OrderLine> OrderList { get; set; }
        public double SubTotal { get; set; }
        public double TotalOrder { get; set; }
        private static readonly double TaxRate = 0.06;

        public Order()
        {
            orderCounter++;
            OrderID = orderCounter;

            OrderList = new List<OrderLine>();
        }
        public void AddToAnOrder(Product addedProduct, int qty)
        {
            OrderList.Add(new OrderLine(addedProduct, qty));
        }
                //Right now Remove from order does nothing
                //Attempted to print the current order and have the user select what they would like to remove but items in orderlist are not appearring. for and foreach loop
                //Should it be a perimeter of type Product to remove the specific Product Item   
        public void RemoveFromAnOrder() 
        {
            for (var i = 1; i <= OrderList.Count; i++)
            {
                Console.WriteLine($"{ i} - { OrderList[i - 1].Item}");
            }
            /*
            foreach (var item in OrderList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Which product would you like to remove from your order list?");
            var productRemoved = Console.ReadLine();
            */
        }

        public void CalculateTotal(List<OrderLine> OrderList)
        {
            TotalOrder = CalculateSubTotal(OrderList) + CalculateTaxRate(OrderList);
        }
        public double CalculateSubTotal(List<OrderLine>OrderList)
        {
            double subTotal = 0;
            foreach (var itemLine in OrderList)
            {
                double costOfItems = itemLine.Qty * itemLine.Item.Price;
                subTotal += costOfItems;
            }
            return subTotal;
        }
        public double CalculateTaxRate(List<OrderLine> OrderList)
        {
            return CalculateSubTotal(OrderList) * TaxRate;
        }
        public void Pay()
        {
            CalculateTotal(OrderList);

            //***Prompt user to select their payment type of choice
            // Choose Payment type. Switch to Specific payment process.
            while (true)
            {
                Console.WriteLine($"How would you like to pay for your order? Please select options 0-2: \n0 - Cash, 1 - Crdeit/Debit, 2 - Check");
                //*** Maybe no need for Enum Payment Type, just ask for an int and switch  should follow?***

                var paymentType = int.TryParse(Console.ReadLine(), out int result);
                switch (result)
                {
                    case 0:
                        PayCash();
                        return;
                    case 1:
                        PayCredit();
                        return;
                    case 2:
                        PayCheck();
                        return;
                    default:
                        Console.WriteLine("Unknown Payment Type.");
                        break;
                }
                //*** view note above

                //if (Enum.TryParse(typeof(PaymentType), Console.ReadLine(), out PaymentType input))
                // get input of type PaymentType
                var input = PaymentType.Cash;
                {
                    switch (input)
                    {
                        case PaymentType.Cash:
                            PayCash();
                            return;
                        case PaymentType.Credit:
                            PayCredit();
                            return;
                        case PaymentType.Check:
                            PayCheck();
                            return;
                        default:
                            Console.WriteLine("Unknown Payment Type.");
                            break;
                    }
                }
                Console.Write("Input error: Please try again: ");
            }
            //** view note above the switch above
        }
        public void PayCash()
        { 
            double userPayCash, orderChange; // place holder
            while (true)
            {
                Console.Write("Enter the cash received: ");
                userPayCash = GetCash(); // get input from user, cash paid.
                Console.WriteLine($"Cash Received: ${userPayCash:0.00}");

                if (userPayCash >= TotalOrder)
                {
                    orderChange = userPayCash - TotalOrder;
                    Console.WriteLine($"Total Change: ${orderChange:0.00}");
                    return;
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }

            }
        }
        private double GetCash()
        {
            double getCash = double.Parse(Console.ReadLine());
            return getCash;
        }


        public void PayCredit() //need to validate number, date, cvv 
        {
            string userCCNumber, userCCMonth, userCCYear, userCVV;

            Console.Write("Enter Credit Card Number: ");
            userCCNumber = Console.ReadLine();
            int cCnumber = 0;
            while (!int.TryParse(userCCNumber, out cCnumber) && userCCNumber.Length == 15)
            {
                Console.Write("\nInvalid card number. \nEnter the 16 digit card number located on the front:");
                userCCNumber = Console.ReadLine();
            }

            Console.Write("\nEnter Credit Card Experation Month (2 digit): ");//<- still need to validate the date
            userCCMonth = Console.ReadLine();
            int cCMonth = 0;
            while (!int.TryParse(userCCMonth, out cCMonth) && userCCMonth.Length == 1)
            {
                Console.Write("\nInvalid Experation Date.  \nEnter  Experation Month: ");
                userCCMonth = Console.ReadLine();
            }

            Console.Write("\nEnter Credit Card Experation Year (4 digit): ");//<- still need to validate the date
            userCCYear = Console.ReadLine();
            int cCYear = 0;
            while (!int.TryParse(userCCYear, out cCYear) && userCCYear.Length == 3)
            {
                Console.Write("\nInvalid Experation Date.  \nEnter  Experation Year: ");
                userCCYear = Console.ReadLine();
            }

            Console.Write("\nEnter Credit Card CVV: ");
            userCVV = Console.ReadLine();
            int cVV = 0;
            while (!int.TryParse(userCVV, out cVV) && userCVV.Length == 2)  
            {
                Console.Write("\nInvalid CVV.  \nEnter 3 Digit CVV located on the back of the card: ");
                userCVV = Console.ReadLine();
            }
            Console.Write("Confirm total placed on card: ");
            double userCredit;
            userCredit = Convert.ToDouble(Console.ReadLine());
            while(userCredit != TotalOrder)
            {
                Console.WriteLine("Insufficiant funds. Please verify total.");
                userCredit = Convert.ToDouble(Console.ReadLine());
            }


        }
        
        public void PayCheck()
        {
            int checkVerify;
            string checkNumber;
            double checkTotal, orderTotal;
            Console.Write("Please enter the four(4) digit check number: ");
            checkNumber = Console.ReadLine();
            while (!int.TryParse(checkNumber, out checkVerify) && checkNumber.Length == 3)
            {
                Console.WriteLine("Invalid Entry. Please re-enter the check number: ");
                checkNumber = Console.ReadLine();
            }

            checkTotal = Convert.ToDouble(Console.ReadLine()); 
            orderTotal = TotalOrder;
            while(checkTotal != orderTotal)
            {
                Console.WriteLine("Insufficiant funds. Please verify total.");
                checkTotal = Convert.ToDouble(Console.ReadLine());
            }

        }
        public void Cancel()
        {
            Console.WriteLine($"Order {OrderID} has been cancelled. Press any key to return to main menu.");
            Console.ReadKey();
        }
        public void PrintReceipt(List<OrderLine> OrderList, double payment)
        {
            StringBuilder receipt = new StringBuilder("");
            Console.WriteLine("--- Receipt ---");
            foreach (var itemLine in OrderList)
            {
                Console.WriteLine
                (
                    "{0} {1} ${2} ea. ${3}",
                    itemLine.Item.Name,
                    itemLine.Qty,
                    itemLine.Item.Price,
                    itemLine.Item.Price * itemLine.Qty
                );
            }
            //var subTotal = CalculateSubTotal(OrderList);
            //var salesTax = CalculateTaxRate(OrderList);
            //var total = CalculateTotal(OrderList);
            //Console.WriteLine("Subtotal: ${0:0.00}", subTotal);
            //Console.WriteLine("Tax: ${0:0.00}", salesTax);
            //Console.WriteLine("Total: ${0:0.00}", total);
            //Console.WriteLine("Payment: ${0:0.00}", payment);
            //decimal change = processPayment(total, payment);
        }
    }
}
