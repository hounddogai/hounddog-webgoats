using System;
using System.IO;

class Customer
{
    public string Name { get; set; }
    public string BankAccount { get; set; }
    public string PhoneNumber { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Bank Account: {BankAccount}, Phone Number: {PhoneNumber}";
    }
}

class Program
{
    static void Main()
    {
        // Create a sample customer
        Customer customer = new Customer
        {
            Name = "John Doe",
            BankAccount = "123456789",
            PhoneNumber = "555-1234"
        };

        // Specify the path to the text file
        string filePath = "customer_data.txt";

        // Save customer data to the text file
        SaveCustomerData(filePath, customer);

        Console.WriteLine("Customer data saved to the text file.");

        // Read and display customer data from the text file
        Customer loadedCustomer = ReadCustomerData(filePath);
        Console.WriteLine("Customer data read from the text file:");
        Console.WriteLine(loadedCustomer);
    }

    static void SaveCustomerData(string filePath, Customer customer)
    {
        // Serialize customer data to a string
        string customerData = $"{customer.Name},{customer.BankAccount},{customer.PhoneNumber}";

        // Write the serialized data to the text file
        File.WriteAllText(filePath, customerData);
    }
    static void BadCode2(string bankAccountNumber){
        StreamWriter wr = new StreamWriter("a.txt");
        string x = bankAccountNumber;
        string y = x;
        string w = "g";
        string z = w + y;
        wr.Write(y);
        wr.Write(z);

        using(StreamWriter sw  = new StreamWriter("b.txt")){
        sw.WriteLine(PhoneNumber);
        }
        
    
    }
    static void BadCode(string bankAccountNumber){
        File.WriteAllText("bank.txt", bankAccountNumber);
    }
    static Customer ReadCustomerData(string filePath)
    {
        // Read the serialized data from the text file
        string customerData = File.ReadAllText(filePath);

        string t = customerData;
        // Split the data into individual fields
        string[] fields = customerData.Split(',');

        // Create a new Customer object and populate its properties
        Customer loadedCustomer = new Customer
        {
            Name = fields[0],
            BankAccount = fields[1],
            PhoneNumber = fields[2]
        };
        HttpCookie chocoChipCookie = new HttpCookie();
        chocoChipCookie["User Name"] = userName;
        chocoChipCookie["BankAccount"] = bankAccountNumber;
        
        return loadedCustomer;
    }
}
