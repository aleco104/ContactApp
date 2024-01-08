using ContactApp.Models;
using ContactApp.Models.Responses;

namespace ContactApp.Interfaces;

public interface IContactService
{
    /// <summary>
    /// adds a contact to the contact list
    /// </summary>
    /// <param name="contact">the contact that should be added to the list</param>
    /// <returns>a service result</returns>
    IServiceResult AddContactToList(IContact contact);


    /// <summary>
    /// Gets a contact from the contact list with a requested Email
    /// </summary>
    /// <param name="email">The email to look for</param>
    /// <returns>A message (successed or failed) and the requested contact</returns>
    IServiceResult GetContactFromList(string email);


    /// <summary>
    /// Gets all contacts from the list
    /// </summary>
    /// <returns>A message (succesed or failed) and all contacts in list</returns>
    IServiceResult GetContactsFromList();


    /// <summary>
    /// Deletes a contact in list by using Email
    /// </summary>
    /// <param name="email">The email to look for</param>
    /// <returns>A status of the operation (successed or failed)</returns>
    IServiceResult DeleteContactFromList(string email);
}
