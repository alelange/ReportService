using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Plain.RabbitMQ;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReportService
{
    public class ReportDataCollector : IHostedService
    {
        private readonly ISubscriber subscriber;
        private readonly IMemoryReportStorage memoryReportStorage;

        public ReportDataCollector(ISubscriber subscriber, IMemoryReportStorage memoryReportStorage)
        {
            this.subscriber = subscriber;
            this.memoryReportStorage = memoryReportStorage;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            subscriber.Subscribe(ProcessMessage);
            return Task.CompletedTask;
        }

        private bool ProcessMessage(string message, IDictionary<string, object> headers)
        {
            var book = JsonConvert.DeserializeObject<Book>(message);

            memoryReportStorage.Add(new Report
            {
                Id = book.Id,
                Author = book.Author,
                Title = book.Title
            });

            return true;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
