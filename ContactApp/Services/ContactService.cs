using ContactApp.Enums;
using ContactApp.Interfaces;
using ContactApp.Models.Responses;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ContactApp.Services;

public class ContactService : IContactService
{
    private readonly IFileService _fileService = new FileService();

    private static List<IContact> _contacts = [];

    public ContactService()
    {
        string contactListString = _fileService.GetContentFromFile();
        _contacts = JsonConvert.DeserializeObject<List<IContact>>(contactListString, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        })!;
    }

    public IServiceResult AddContactToList(IContact contact)
    {
        IServiceResult response = new ServiceResult();

        try
        {
            if (!_contacts.Any(x => x.Email == contact.Email))
            {
                _contacts.Add(contact);

                _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts, new JsonSerializerSettings { 
                    TypeNameHandling = TypeNameHandling.Objects, Formatting = Formatting.Indented
                }));

                response.Status = ServiceStatus.SUCCESSED;
                response.Result = contact;
            }
            else
            {
                response.Status = ServiceStatus.ALREADY_EXIST;
                response.Result = "Contact with the same email alreday exists";
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
                _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts, new JsonSerializerSettings { 
                    TypeNameHandling = TypeNameHandling.Objects, Formatting = Formatting.Indented
                }));
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
        if (_contacts.Any(x => x.Email == email)) { 
            try
            {
                IContact contact = _contacts.FirstOrDefault(x => x.Email == email)!;
                response.Status = ServiceStatus.SUCCESSED;
                response.Result = contact;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.Status = ServiceStatus.NOT_FOUND;
                response.Result = "Contact not found.";
            }
        }
        else
        {
            response.Status = ServiceStatus.NOT_FOUND;
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