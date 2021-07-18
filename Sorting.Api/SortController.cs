using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sorting.Api.Controllers
{
    [ApiController]
    [Route("sort")]
    public class SortController : ControllerBase
    {
        private readonly ISortJobProcessor _sortJobProcessor;


        public SortController(ISortJobProcessor sortJobProcessor)
        {
            _sortJobProcessor = sortJobProcessor;
        }

        [HttpPost("run")]
        [Obsolete("This executes the sort job asynchronously. Use the asynchronous 'EnqueueJob' instead.")]
        public async Task<ActionResult<SortJob>> EnqueueAndRunJob(int[] values)
        {
            var pendingJob = new SortJob(
                id: Guid.NewGuid(),
                status: SortJobStatus.Pending,
                duration: null,
                input: values,
                output: null);

            var completedJob = await _sortJobProcessor.Process(pendingJob);

            return Ok(completedJob);
        }

        [HttpPost]
        public async Task<ActionResult<SortJob>> EnqueueJob(int[] values)
        {
            Console.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);
            //// TODO: Should enqueue a job to be processed in the background.
            ////throw new NotImplementedException();
            var pendingJob = new SortJob(
               id: Guid.NewGuid(),
               status: SortJobStatus.Pending,
               duration: null,
               input: values,
               output: null);

            #region tried
            //////var completedJob = await _sortJobProcessor.Process(pendingJob).ConfigureAwait(continueOnCapturedContext: false);
            ///////var completedJob =await _sortJobProcessor.Process(pendingJob).ConfigureAwait(continueOnCapturedContext:true);
            ////// Task.Run(() => _sortJobProcessor.Process(pendingJob));
            //////   Task.Factory.StartNew(() => _sortJobProcessor.Process(pendingJob)
            //////  await Task.Run(() => _sortJobProcessor.Process(pendingJob), CancellationToken.None);
            ////// Task.Factory.StartNew(() => _sortJobProcessor.Process(pendingJob));
            /////  return OK(Task.FromResult(pendingJob));
            /////  return Task.FromResult(OpendingJob);
            ///////  await _sortJobProcessor.Process(pendingJob);
            ////// var completedJob = _sortJobProcessor.Process(pendingJob).ConfigureAwait(false);
            #endregion

            var completedJob = _sortJobProcessor.Process(pendingJob);
            return Ok(await Task.FromResult(pendingJob));
        }

        [HttpGet]
        public async Task<ActionResult<SortJob[]>> GetJobs()
        {
            ///// TODO: Should return all jobs that have been enqueued (both pending and completed).
            ////// throw new NotImplementedException();  
            var completedJob = await _sortJobProcessor.AllJobs();
            return Ok(completedJob);
        }

        [HttpGet("{jobId}")]
        public async Task<ActionResult<SortJob>> GetJob(Guid jobId)
        {
            // TODO: Should return a specific job by ID.
            //////throw new NotImplementedException();
            var completedJob = await _sortJobProcessor.JobById(jobId);
            return Ok(completedJob);
        }
    }
}
