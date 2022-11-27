
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;


#nullable enable
namespace DynamicDateTime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
          
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return (IActionResult)this.View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            DateTime start = new DateTime(2022, 1, 1);
            DateTime end = new DateTime(2022, 12, 1);
            var result  = new ResourcePlanningViewModel()
            {
                Headers = this.GetAllHeaders(start, end),
                Rows = this.GetAllRows(start, end)
            };
            return Json(result);
        }

        private List<Rows> GetAllRows(DateTime start, DateTime end)
        {
            List<Rows> rowsList = new List<Rows>();
            List<Resource> resourceList = this.Resources();
            List<Absence> list1 = this.Absences().ToList<Absence>();
            foreach (Resource resource1 in resourceList)
            {
                Resource resource = resource1;
                Rows rows = new Rows();
                rows.ResourceId = resource.Id;
                rows.IndividualName = resource.IndividualName;
                List<DateTime> list2 = list1.Where<Absence>((Func<Absence, bool>)(d => d.ResourceId == resource.Id)).Select<Absence, DateTime>((Func<Absence, DateTime>)(x => x.AbsenceDate.Date)).ToList<DateTime>();
                for (DateTime dateTime = start; dateTime <= end; dateTime = dateTime.AddMonths(1))
                {
                    Column column = new Column();
                    column.Name = dateTime.Date.ToString("MMM yy");
                    int num = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
                    for (int day = 1; day <= num; ++day)
                    {
                        DateTimeColor dateTimeColor = new DateTimeColor();
                        dateTimeColor.Date = new DateTime(dateTime.Year, dateTime.Month, day);
                        if (dateTimeColor.Date >= resource.CurrentFromDate && dateTime <= resource.CurrentToDate)
                            dateTimeColor.Color = AppConstants.CurrentColor;
                        if (dateTimeColor.Date >= resource.NextFromDate && dateTime <= resource.NextToDate)
                            dateTimeColor.Color = AppConstants.NextColor;
                        if (list2.Any<DateTime>((Func<DateTime, bool>)(x => x == dateTimeColor.Date)))
                            dateTimeColor.Color = AppConstants.AbsenceColor;
                        if (string.IsNullOrEmpty(dateTimeColor.Color))
                            dateTimeColor.Color = AppConstants.EmptyColor;
                        column.DateTimeColors.Add(dateTimeColor);
                    }
                    rows.Colls.Add(column);
                }
                rowsList.Add(rows);
            }
            return rowsList;
        }

        private List<string> GetAllHeaders(DateTime start, DateTime end)
        {
            List<string> stringList = new List<string>();
            for (DateTime dateTime = start; dateTime <= end; dateTime = dateTime.AddMonths(1))
                stringList.Add(dateTime.Date.ToString("MMM yy"));
            return stringList;
        }

        private List<Resource> Resources()
        {
            return new List<Resource>()
      {
        new Resource()
        {
          Id = 1,
          IndividualName = "Anower",
          CurrentFromDate = new DateTime(2022, 1, 1),
          CurrentToDate = new DateTime(2022, 3, 15),
          NextFromDate = new DateTime(2022, 3, 16),
          NextToDate = new DateTime(2022, 4, 30)
        },
        new Resource()
        {
          Id = 3,
          IndividualName = "Jahed",
          CurrentFromDate = new DateTime(2022, 1, 1),
          CurrentToDate = new DateTime(2022, 2, 15),
          NextFromDate = new DateTime(2022, 2, 16),
          NextToDate = new DateTime(2022, 3, 31)
        }
      };
        }

        private List<Absence> Absences()
        {
            return new List<Absence>()
      {
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 1, 15)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 1, 16)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 1, 17)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 1, 18)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 1, 19)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 1, 20)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 3, 10)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 3, 11)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 3, 12)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 3, 13)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 3, 14)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 3, 15)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 11, 10)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 11, 11)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 11, 12)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 11, 13)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 11, 14)
        },
        new Absence()
        {
          ResourceId = 1,
          AbsenceDate = new DateTime(2022, 11, 15)
        },
        new Absence()
        {
          ResourceId = 3,
          AbsenceDate = new DateTime(2022, 11, 12)
        },
        new Absence()
        {
          ResourceId = 3,
          AbsenceDate = new DateTime(2022, 11, 13)
        },
        new Absence()
        {
          ResourceId = 3,
          AbsenceDate = new DateTime(2022, 11, 14)
        },
        new Absence()
        {
          ResourceId = 3,
          AbsenceDate = new DateTime(2022, 11, 15)
        }
      };
        }
    }

    public class Absence
    {
        public int ResourceId { get; set; }

        public DateTime AbsenceDate { get; set; }
    }
    public class Column
    {
        public string Name { get; set; }

        public List<DateTimeColor> DateTimeColors { get; set; } = new List<DateTimeColor>();
    }
    public class DateTimeColor
    {
        public DateTime Date { get; set; }

        public string Color { get; set; }
    }
    public class Resource
    {
        public int Id { get; set; }

        public string IndividualName { get; set; }

        public DateTime CurrentFromDate { get; set; }

        public DateTime CurrentToDate { get; set; }

        public DateTime NextFromDate { get; set; }

        public DateTime NextToDate { get; set; }
    }
    public class Rows
    {
        public List<Column> Colls { get; set; } = new List<Column>();

        public string IndividualName { get; set; }

        public int ResourceId { get; set; }

    }
    public class ResourcePlanningViewModel
    {
        public List<string> Headers { get; set; }

        public List<Rows> Rows { get; set; } = new List<Rows>();

    }
}
