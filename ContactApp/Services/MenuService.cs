using ContactApp.Enums;
using ContactApp.Interfaces;
using ContactApp.Models;

namespace ContactApp.Services;

public class MenuService : IMenuService
{
    private readonly IContactService _contactService = new ContactService();

    public void MainMenu()
    {
        while (true)
        {
            DisplayMenuTitle("MENU OPTIONS");
            Console.WriteLine($"{"1.", -4} Add New Contact");
            Console.WriteLine($"{"2.",-4} View Contact List");
            Console.WriteLine($"{"3.",-4} Delete Contact");
            Console.WriteLine($"{"4.",-4} View Contact");
            Console.WriteLine($"{"0.",-4} Exit Application");
            Console.WriteLine();
            Console.Write("Enter Menu Option: ");
            var option = Console.ReadLine();

            switch(option)
            {
                case "1":
                    AddContact();
                    break;

                case "2":
                    ViewContactList();
                    break;

                case "3":
                    DeleteContact();
                    break;

                case "4":
                    ViewContact();
                    break;

                case "0":
                    ExitApplication();
                    break;

                default:
                    Console.WriteLine("\nInvalid Option Selected. Press any key to try agan.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void ExitApplication()
    {
        Console.Clear();
        Console.WriteLine("Are you sure you want to close this application? (y/n)");
        var option = Console.ReadLine() ?? "";

        if (option.ToLower() == "y")
            Environment.Exit(0);
    }

    private void AddContact()
    {
        IContact contact = new Contact();

        DisplayMenuTitle("Add New Contact");

        Console.Write("First Name: ");
        contact.FirstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        contact.LastName = Console.ReadLine()!;

        Console.Write("Email: ");
        contact.Email = Console.ReadLine()!;

        Console.Write("Phone Number: ");
        contact.PhoneNumber = Console.ReadLine()!;

        Console.Write("Adress: ");
        contact.Address = Console.ReadLine()!;

        var response =  _contactService.AddContactToList(contact);
      
        switch(response.Status)
        {
            case ServiceStatus.SUCCESSED:
                Console.WriteLine("The contact was added successfully.");
                break;

            case ServiceStatus.ALREADY_EXIST:
                Console.WriteLine("The contact already exists.");
                break;

            case ServiceStatus.FAILED:
                Console.WriteLine("Failed when trying to add the contact to contact list.");
                Console.WriteLine("See error message :: " + response.Result.ToString());
                break;
        }

        DisplayPressAnyKey();
    }

    private void DeleteContact()
    {
        DisplayMenuTitle("Delete Contact"); 
        Console.Write("Enter the email of the contact to delete: ");
        string emailToDelete = Console.ReadLine()!;

        var response = _contactService.DeleteContactFromList(emailToDelete);

        if (response.Status == ServiceStatus.SUCCESSED)
        {
            Console.WriteLine("The contact was deleted.");
        }
        else
        {
            Console.WriteLine("Could not delete contact.");
        }

        DisplayPressAnyKey();
    }

    private void ViewContact()
    {
        DisplayMenuTitle("View contact");
        Console.Write("Enter the email of the contact to display: ");
        string emailToView = Console.ReadLine()!;

        var response = _contactService.GetContactFromList(emailToView);

        if (response.Result is IContact contact)
        {
            Console.WriteLine();
            Console.WriteLine(contact.FirstName + " " + contact.LastName);
            Console.WriteLine(contact.Email);
            Console.WriteLine(contact.PhoneNumber);
            Console.WriteLine(contact.Address);
        }
        else
        {
            Console.WriteLine("The contact was not found.");
        }

        DisplayPressAnyKey();
    }


    private void ViewContactList()
    {
        DisplayMenuTitle("Contact List");
        var response = _contactService.GetContactsFromList();

        if (response.Status == ServiceStatus.SUCCESSED)
        {
            if (response.Result is List<IContact> contactList)
            {
                if (!contactList.Any())
                {
                    Console.WriteLine("No contacts found.");
                }
                else
                {
                    foreach (var contact in contactList)
                    {
                       Console.WriteLine($"{contact.FirstName} {contact.LastName} <{contact.Email}>");
                    }
                } 
            }
        }

        DisplayPressAnyKey();
    }

    private void DisplayMenuTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"### {title} ###");
        Console.WriteLine();
    }

    private void DisplayPressAnyKey() 
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
}
