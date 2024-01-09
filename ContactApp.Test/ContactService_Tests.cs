using ContactApp.Interfaces;
using ContactApp.Models;
using ContactApp.Services;
using ContactApp.Enums;

namespace ContactApp.Test;

public class ContactService_Tests
{
    private readonly IContactService _contactService = new ContactService();
    
    [Fact]
    public void AddContactToListShould_AddOneContactToList_ThenReturnSuccessed()
    {
        // Arrange
        _contactService.DeleteContactFromList("test@domain.com");
        IContact contact = new Contact
        {
            FirstName = "Test",
            LastName = "Test",
            PhoneNumber = "Test",
            Email = "test@domain.com",
            Address = "Test"
        };
        
        // Act
        var response = _contactService.AddContactToList(contact);

        //Assert
        Assert.True(response.Status == ServiceStatus.SUCCESSED);
        Assert.True(response.Result is IContact);
        Assert.True((response.Result as IContact)!.FirstName == "Test");
    }

    [Fact]
    public void AddContactToListShould_FailToAddContact_IfEmailExists()
    {
        // Arrange
        _contactService.DeleteContactFromList("test@domain.com");
        IContact contact1 = new Contact
        {
            FirstName = "Test",
            LastName = "Test",
            PhoneNumber = "Test",
            Email = "test@domain.com",
            Address = "Test"
        };

        var response = _contactService.AddContactToList(contact1);

        IContact contact2 = new Contact
        {
            FirstName = "Test",
            LastName = "Test",
            PhoneNumber = "Test",
            Email = "test@domain.com",
            Address = "Test"
        };

        // Act
        response = _contactService.AddContactToList(contact2);

        //Assert
        Assert.True(response.Status == ServiceStatus.ALREADY_EXIST);
        Assert.True((response.Result as String)! == "Contact with the same email alreday exists");
    }


    [Fact]
    public void GetContactFromListShould_ReturnContact_IfContactExists()
    {
        // Arrange
        _contactService.DeleteContactFromList("test@domain.com");
        IContact contact = new Contact
        {
            FirstName = "Test",
            LastName = "Test",
            PhoneNumber = "Test",
            Email = "test@domain.com",
            Address = "Test"
        };
        _contactService.AddContactToList(contact);

        // Act
        var response = _contactService.GetContactFromList("test@domain.com");

        //Assert
        Assert.True(response.Status == ServiceStatus.SUCCESSED);
        Assert.True(response.Result is IContact);
        Assert.True((response.Result as IContact)!.FirstName == "Test");
    }

    [Fact]
    public void GetContactFromListShould_Fail_IfContactDoesntExist()
    {
        // Arrange
        _contactService.DeleteContactFromList("test@domain.com");

        // Act
        var response = _contactService.GetContactFromList("test@domain.com");

        //Assert
        Assert.True(response.Status == ServiceStatus.NOT_FOUND);
        Assert.True((response.Result as String)! == "Contact not found.");
    }
}

            

            








//    

