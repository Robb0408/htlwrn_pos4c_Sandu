using ContactList.Logic.Models;

namespace ContactList.Logic.Services
{
    public interface IContactService
    {
        /// <summary>
        /// Gets all contacts from in-memory list
        /// </summary>
        /// <returns></returns>
        IEnumerable<Person> GetContacts();

        /// <summary>
        /// Adds a new contact to in-memory list
        /// </summary>
        /// <param name="person">
        /// Person to add to contact list
        /// </param>
        Person AddContact(Person person);

        /// <summary>
        /// Deletes a contact from in-memory list
        /// </summary>
        /// <param name="id">
        /// Id from contact to delete
        /// </param>
        bool DeleteContact(int id);

        /// <summary>
        /// Gets a contact by a provided name filter
        /// </summary>
        /// <param name="name">
        /// Name from contact to search after
        /// </param>
        /// <returns></returns>
        Person? GetContact(string name);
    }
}
