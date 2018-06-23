using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace QuartzAdmin.web.Controllers
{
    public class TriggerController : Controller
    {
        Models.InstanceRepository instanceRepo = new Models.InstanceRepository();

        //
        // GET: /Trigger/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string instanceName, string groupName, string itemName)
        {
            Models.InstanceModel instance = instanceRepo.GetInstance(instanceName);
            Models.TriggerRepository trigRepo = new Models.TriggerRepository(instance);

            Models.TriggerFireTimesModel m = new Models.TriggerFireTimesModel();
            m.Trigger = trigRepo.GetTrigger(itemName, groupName).Result;

            if (!String.IsNullOrWhiteSpace(m.Trigger.CalendarName))
            {
                Models.CalendarRepository calRepo = new Models.CalendarRepository(instance);
                m.Calendar = calRepo.GetCalendar(m.Trigger.CalendarName);
            }
            m.Instance = instance;

            ViewData["groupName"] = groupName;

            if (m.Trigger == null)
            {
                ViewData["triggerName"] = itemName;
                return View("NotFound");
            }
            else
            {
                return View(m);
            }
        }

        public ActionResult FireTimes(string instanceName, string groupName, string itemName)
        {
            Models.InstanceModel instance = instanceRepo.GetInstance(instanceName);
            Models.TriggerRepository trigRepo = new Models.TriggerRepository(instance);

            Models.TriggerFireTimesModel m = new Models.TriggerFireTimesModel();
            m.Trigger = trigRepo.GetTrigger(itemName, groupName).Result;

            Models.CalendarRepository calRepo = new Models.CalendarRepository(instance);
            m.Calendar = calRepo.GetCalendar(m.Trigger.CalendarName);

            ViewData["groupName"] = groupName;

            if (m.Trigger == null)
            {
                ViewData["triggerName"] = itemName;
                return View("NotFound");
            }
            else
            {
                return View(m);
            }

        }

    }
}
