using ContactList.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Logic.Services
{
    public class ContactService : IContactService
    {
        private IList<Person> contacts;

        public ContactService(IList<Person> contacts)
        {
            this.contacts = contacts;
        }

        public Person AddContact(Person person)
        {
            contacts.Add(person);
            return person;
        }

        public bool DeleteContact(int id)
        {
            return contacts.Remove(contacts.FirstOrDefault(c => c.Id == id)!);
        }

        public Person? GetContact(string name)
        {
            return contacts.FirstOrDefault(c => c.LastName.Contains(name) || c.FirstName.Contains(name));
        }

        public IEnumerable<Person> GetContacts()
        {
            return contacts;
        }
    }
}
