using ContactApp.Enums;
using ContactApp.Interfaces;
using ContactApp.Models.Responses;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ContactApp.Services;

//All logik med att lägga till användare i listan, den mer rena c# koden ligger i ContactService!!!

public class ContactService : IContactService
{
    private readonly IFileService _fileService = new FileService();

    private static readonly List<IContact> _contacts = [];

    public IServiceResult AddContactToList(IContact contact)
    {
        IServiceResult response = new ServiceResult();

        try
        {
            if (!_contacts.Any(x => x.Email == contact.Email))
            {
                _contacts.Add(contact);
               
                _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts));

                response.Status = ServiceStatus.SUCCESSED;
            }
            else
            {
                response.Status = ServiceStatus.ALREADY_EXIST;
            }
        
        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);
            response.Status = ServiceStatus.FAILED;
            response.Result = ex.Message;
        }

        return response;
    }

    public IServiceResult DeleteContactFromList(string email)
    {
        IServiceResult response = new ServiceResult();

        if (_contacts.Any(x => x.Email == email))
        {
            IContact contact = _contacts.FirstOrDefault(x => x.Email == email)!;

            try
            {
                _contacts.Remove(contact);
                response.Status = ServiceStatus.SUCCESSED;
                response.Result = "Contact deleted";
                return response;
            }
            catch
            {
                response.Status = ServiceStatus.FAILED;
                response.Result = "Contact not found";
                return response;
            }
        }
        else
        {
            response.Status = ServiceStatus.FAILED;
            response.Result = "Contact not found";
            return response;
        }
        
    }

    public IServiceResult GetContactFromList(string email)
    {
        IServiceResult response = new ServiceResult();

        try
        {
            IContact contact = _contacts.FirstOrDefault(x => x.Email == email)!;
            response.Status = ServiceStatus.SUCCESSED;
            response.Result = contact;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = ServiceStatus.FAILED;
            response.Result = "Contact not found.";
        }

        return response;
    }

    public IServiceResult GetContactsFromList()
    {
        IServiceResult response = new ServiceResult();

        try
        {
            response.Status = ServiceStatus.SUCCESSED;
            response.Result = _contacts;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = ServiceStatus.FAILED;
            response.Result = ex.Message;
        }

        return response;
    }
}