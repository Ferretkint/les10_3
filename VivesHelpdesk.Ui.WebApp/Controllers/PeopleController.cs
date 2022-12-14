using Microsoft.AspNetCore.Mvc;
using VivesHelpdesk.Model;
using VivesHelpdesk.Services;

namespace VivesHelpdesk.Ui.WebApp.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PersonService _personService;

        public PeopleController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var people = _personService.Find();
            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm]Person person)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            _personService.Create(person);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var person = _personService.Get(id);

            if(person is null)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, [FromForm]Person person)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            _personService.Update(id, person);

            return RedirectToAction("Index");

        }

        [HttpPost]
        [Route("[controller]/Delete/{id:int?}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var person = _personService.Get(id);

            if(person is null)
            {
                return RedirectToAction("Index");
            }

            _personService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
