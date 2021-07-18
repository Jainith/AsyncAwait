using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sorting.Api
{
    public class SortJobProcessor : ISortJobProcessor
    {
        private readonly ILogger<SortJobProcessor> _logger; 
        private readonly List<SortJob> jobs = new List<SortJob>();
        public SortJobProcessor(ILogger<SortJobProcessor> logger)
        { 
               _logger = logger;
        }

        public async Task<SortJob> Process(SortJob job)
        {
             jobs.Add(job); ////// for pending jobs 
            _logger.LogInformation("Processing job with ID '{JobId}'.", job.Id);
           
            var stopwatch = Stopwatch.StartNew();

            var output = job.Input.OrderBy(n => n).ToArray();
            await Task.Delay(5000); // NOTE: This is just to simulate a more expensive operation

            var duration = stopwatch.Elapsed;

            _logger.LogInformation("Completed processing job with ID '{JobId}'. Duration: '{Duration}'.", job.Id, duration); 

            var completedJob = new SortJob(
                   id: job.Id,
                   status: SortJobStatus.Completed,
                   duration: duration,
                   input: job.Input,
                   output: output);

            jobs.RemoveAll(r => r.Id == job.Id);
            jobs.Add(completedJob);   
            return completedJob;
        }
        public async Task<SortJob[]> AllJobs()
        {
            ////await Task.CompletedTask;
            ////return  jobs.ToArray();
            return await Task.FromResult(jobs.ToArray());
        }
        public async Task<SortJob> JobById(System.Guid Id)
        {
            return await Task.FromResult(jobs.FirstOrDefault(x => x.Id == Id)); 
        } 
    }
}
