using System.Collections.Generic;
using System.Linq;

namespace ReportService
{
    public class MemoryReportStorage : IMemoryReportStorage
    {
        private readonly IList<Report> reports = new List<Report>();
        public void Add(Report report)
        {
            var obj = reports.FirstOrDefault(x => x.Id == report.Id);
            if (obj == null)
            {
                reports.Add(report);
            }
            else
            {
                obj.Author = report.Author;
                obj.Title = report.Title;
            }
        }

        public IEnumerable<Report> Get()
        {
            return reports;
        }
    }
}
