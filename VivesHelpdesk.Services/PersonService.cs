using VivesHelpdesk.Data;
using VivesHelpdesk.Model;

namespace VivesHelpdesk.Services
{
    public class PersonService
    {
        private readonly VivesHelpdeskDbContext _dbContext;

        public PersonService(VivesHelpdeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<Person> Find()
        {
            return _dbContext.People.ToList();
        }

        //Get
        public Person? Get(int id)
        {
            return _dbContext.People
                .SingleOrDefault(p => p.Id == id);
        }

        //Create
        public Person Create(Person person)
        {
            _dbContext.Add(person);
            _dbContext.SaveChanges();

            return person;
        }

        //Update
        public Person? Update(int id, Person person)
        {
            var dbPerson = _dbContext.People
                .SingleOrDefault(p => p.Id == id);
            if (dbPerson == null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;

            _dbContext.SaveChanges();

            return person;
        }

        //Delete
        public void Delete(int id)
        {
            var dbPerson = _dbContext.People
                .SingleOrDefault(p => p.Id == id);
            if (dbPerson == null)
            {
                return;
            }

            _dbContext.People.Remove(dbPerson);
            _dbContext.SaveChanges();
        }

    }
}
