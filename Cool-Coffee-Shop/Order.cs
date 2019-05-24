﻿using System;
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

        //public Product RemoveFromAnOrder()
        //{

        //}


        public double CalculateTotal(List<OrderLine> OrderList)
        {
            double total = CalculateSubTotal(OrderList) + CalculateTaxRate(OrderList);
            return total;
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

            // Choose Payment type. Switch to Specific payment process.
            while (true)
            {
                //if (Enum.TryParse(typeof(PaymentType), Console.ReadLine(), out PaymentType input))
                // get input of type PaymentType
                var input = PaymentType.Credit;
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
        }
        public void PayCash()
        { 
            double userPayCash,orderTotal, orderChange; // place holder
            userPayCash = 4 ;
            orderTotal = 0; //<- pull total from elsewhere and add here
            Console.WriteLine($"Cash Received: {userPayCash}");
            
            if (userPayCash > orderTotal)
            {
                orderChange = userPayCash - orderTotal;
                Console.WriteLine($"Total Change: " + orderChange);
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }

        public void PayCredit() //need to validate number, date, cvv 
        {
            string userCCNumber, userCVV, userCCMonth,userCCYear;

            Console.Write("Enter Credit Card Number: ");
            userCCNumber = Console.ReadLine();
            int cCnumber = 0;
            while (!int.TryParse(userCCNumber, out cCnumber) && userCCNumber.Length == 15) 
            {
                Console.Write("\nInvalid card number. \nEnter the 16 digit card number located on the front:");
                userCCNumber = Console.ReadLine();
            }

            Console.Write("\nEnter Credit Card Experation Date");//<- still need to validate the date
            Console.Write("\nEnter the month(mm): ");
            userCCMonth = Console.ReadLine();
            int cCMonth = 0;
            while (!int.TryParse(userCCMonth, out cCMonth)&& cCMonth > 0 && cCMonth < 13)
            {
                Console.Write("\nInvalid month.  \nEnter  Enter the month(mm): ");
                userCCMonth = Console.ReadLine();
            }
            
            Console.Write("\nEnter Year (yyyy): " );
            userCCYear = Console.ReadLine();
            int cCYear = 0;
           
            while (!int.TryParse(userCCYear, out cCYear) && cCYear > 2000)  
            {
                Console.Write("\nInvalid year.  \nEnter Year (yyyy): ");
                userCCYear = Console.ReadLine();
            }

            Console.Write("\nEnter Credit Card CVV: ");
            userCVV = Console.ReadLine();
            int cVV = 0;
            while (!int.TryParse(userCVV, out cVV) && userCVV.Length == 2)  //Also need to check for 3 digit intiger
            {
                Console.Write("\nInvalid CVV.  \nEnter 3 Digit CVV located on the back of the card: ");
                userCVV = Console.ReadLine();
            }

            double userCredit, orderTotal;
            userCredit = Convert.ToDouble(Console.ReadLine());
            orderTotal =0;  //<- pull total from elsewhere and add here 
            while(userCredit != orderTotal)
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
            while (!int.TryParse(checkNumber, out checkVerify) && checkNumber.Length != 3)
            {
                Console.WriteLine("Invalid Entry. Please re-enter the check number: ");
                checkNumber = Console.ReadLine();
            }

            checkTotal = Convert.ToDouble(Console.ReadLine()); //<- Place holder
            orderTotal = 0; //<- Place holder Pull total from elsewhere. 
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
            // return to main menu
        }
        //public void PrintReceipt()
        //{
        //    //At the end, display a receipt with all items ordered, subtotal, grand total, and
        //    //appropriate payment info.
        //    Console.WriteLine($"");
        //}

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
