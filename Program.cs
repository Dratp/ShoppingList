using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ShoppingList
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, double> items = new Dictionary<string, double>();
            items["Apples"] = .99;
            items["Bananas"] = .50;
            items["Pears"] = 1.15;
            items["Avacados"] = .70;
            items["Carrots"] = 2.25;
            items["Cucumbers"] = 3.10;
            items["Peaches"] = 1.15;
            items["Strawberries"] = 4.70;
            items["Raspberries"] = 3.20;
            items["Snap Peas"] = .75;
            items["Potatoes"] = 5.00;
            items["Kiwis"] = 2.70;

            ArrayList mycart = new ArrayList();
            ArrayList myCartPrices = new ArrayList();
            bool Proceed;
            string choice;
            string input;
            do
            {
                DisplayItemList(items);
                choice = SelectItem(items);
                //Console.WriteLine(choice);  // Debugging Code
                mycart = AddItemToCart(choice, mycart, GetPrice(choice, items));
                myCartPrices.Insert(0, items[choice]);
                Console.WriteLine();
                do
                {
                    Console.Write("Would you like to order anything else (y/n)");
                    input = Console.ReadLine();
                    input = input.ToUpper();
                } while (input != "Y" && input != "N");
                if (input == "Y")
                {
                    Proceed = false;
                }
                else
                {
                    Proceed = true;
                }

            } while (Proceed == false);
            DisplayCart(mycart);
            CartMath(mycart);

        }

        static void DisplayItemList(Dictionary<string, double> items)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Guenther's Market!");
            Console.WriteLine();
            Console.WriteLine("{0,-18}{1,-7}","Item","Price");
            Console.WriteLine("================================");
            foreach (KeyValuePair<string, double> item in items)
            {
                double price = (double)item.Value;
                string food = item.Key;
//                Console.WriteLine("{0,-20}${1,-7}",item.Key, price);
                Console.WriteLine($"{food,-20}${price,-7:N2}");
            }
        }

        static string SelectItem(Dictionary<string, double> list)
        {
            bool isValid;
            string choice;
            Console.WriteLine();
            do
            {
                Console.Write("What item would you like to add to your cart?: ");
                choice = Console.ReadLine();
                choice = ConvertChoice(choice); // Convert Choice adds "s" to the end if missing; and capitizes the first letters of words
                if (list.ContainsKey(choice))
                {
                    double price = list[choice];
                    Console.WriteLine($"Adding {choice} to your cart at ${price:N2}");
                    isValid = true;
                }
                else
                {
                    DisplayItemList(list);
                    Console.WriteLine();
                    Console.WriteLine("Sorry, we don't have those.  Please try again.");
                    isValid = false;
                }
            } while (!isValid);
            

            return choice;
        }

        static ArrayList AddItemToCart(string choice, ArrayList mycart, double price)
        {
            int prelimindex = mycart.IndexOf(choice);
            if (prelimindex == -1)
            {
                mycart.Add(choice);
                mycart.Add(1);
                mycart.Add(price);
            }
            else
            {
                mycart[prelimindex + 1] = (int)mycart[prelimindex + 1] + 1;
            }
            return mycart;
        } 

        static double GetPrice(string choice, Dictionary<string, double> items)
        {
            double Price = (double)items[choice];
            return Price;
        }

        static void DisplayCart(ArrayList list)
        {
            Console.Clear();
            for (int i = 0; i < list.Count; i = i+3)
            {
                Console.WriteLine($"{list[i+1]} x {list[i]} at the cost of {list[i+2]:C2}.");
            }
            Console.WriteLine();
        }

        static void CartMath(ArrayList list)
        {
            double total = 0;
            int count = 0;
            for (int i = 0; i < list.Count; i = i + 3)
            {
                int qty = (int)list[i+1];
                double price = (double)list[i+2];
                total = total + (qty * price);
                count = count + qty;
            }
            
            Console.WriteLine($"The Total of everything in your cart is: ${total:N2}");
            Console.WriteLine($"The Average price of an item in your cart is ${total/count:N2}");
        }

        static string ConvertChoice(string choice)
        {
            //Console.WriteLine(choice); // Debugging Code
            int length = choice.Length;
            string lastletter = choice.Substring(length - 1, 1);
            //Console.WriteLine(lastletter);  // Debugging Code
            if (!(lastletter == "s" || lastletter == "S"))  // if last letter is not an s add an s to the end
            {
                choice = choice + "s";
            }
            //Console.WriteLine(choice);   // Debugging Code
            
            // ~~~~~~  Breaking the String Apart ~~~~~~

            int space = choice.IndexOf(" ");
            string secondfirstletter = null;
            string secondrest = null;
            if (space == -1)  // if only one word
            {
                space = choice.Length;
            }
            else // else 2 words
            {
                secondfirstletter = choice.Substring(space + 1, 1);
                secondrest = choice.Substring(space + 2, choice.Length - (space + 2));
            }
            //Console.WriteLine(space);  // Debugging Code
            string firstletter = choice.Substring(0, 1);
            string firstrest = choice.Substring(1, space - 1);

            //Console.WriteLine(firstletter);  // Debugging Code
            //Console.WriteLine(firstrest);  // Debugging Code
            //Console.WriteLine(secondfirstletter);  // Debugging Code
            //Console.WriteLine(secondrest);  // Debugging Code
            //Console.WriteLine($"Space = {space}");  // Debugging Code
            
            //  ~~~~~~~~ Putting the String back together all perfect ~~~~~~~~~~
            
            if (space == choice.Length)  
            {
                choice = $"{firstletter.ToUpper()}{firstrest.ToLower()}";
            }
            else
            {
                choice = $"{firstletter.ToUpper()}{firstrest.ToLower()} {secondfirstletter.ToUpper()}{secondrest.ToLower()}";
            }
            //Console.WriteLine(choice);  // Debugging Code
            return choice;
        }
    }
}
