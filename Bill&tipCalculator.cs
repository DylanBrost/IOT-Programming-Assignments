using System.ComponentModel.Design;
using System.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Please, Enter Bill amount: ");           //Prompt user for there bill amount
        decimal totalbill = decimal.Parse(Console.ReadLine());

        if (totalbill < 0)
        {
            Console.WriteLine("Error, enter valid number"); //Error, if user does not enter a valid number
            return;
        }

        Console.Write("Please, Enter tip amount: ");                    //Prompt user for amount they want to tip then divide that by 100
        decimal tipamount = decimal.Parse(Console.ReadLine()) / 100;

        if (tipamount < 0)
        {
            Console.WriteLine("Error, Enter valid number");
            return;
        }

        decimal totaltip = totalbill * tipamount;       //Formulas for determining final price with tip added on
        decimal finalprice = totalbill + totaltip;


        Console.WriteLine($"You should leave a tip of: ${totaltip}");
        Console.WriteLine($"Your total bill is ${finalprice}");

        Console.Write("Please, Enter amount of people: ");      //ask for the amount of people splitting the bill
        int totalpeople = int.Parse(Console.ReadLine());

        if (totalpeople <= 0)
        {
            Console.WriteLine("Error, Enter valid number");
            return;
        }

        decimal billperperson = finalprice / totalpeople;    // the formula for what each person owes

        Console.WriteLine($"Your share of the bill is ${billperperson}");

        string folderPath = "C:\\IOT";

        Directory.CreateDirectory(folderPath);

        for (int i = 0; i < totalpeople; i++)
        {
            Console.Write($"Enter name for person {i + 1}: ");
            string name = Console.ReadLine();

            string fullPath = folderPath + "\\" + name + ".txt";

            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine(name);
                writer.WriteLine("Total: " + finalprice);
                writer.WriteLine("split into " + totalpeople + " persons. Share amount: " + billperperson);
            }

            Console.WriteLine($"Invoice generated at: {fullPath}");
        }


    }
}