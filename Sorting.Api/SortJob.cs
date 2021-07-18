using System;
using System.Collections.Generic;

namespace Sorting.Api
{
    public class SortJob
    {
      
        public SortJob() : this(Guid.NewGuid(), SortJobStatus.Pending, null, Array.Empty<int>(), null)
        { 
        }
        public SortJob(Guid id, SortJobStatus status, TimeSpan? duration, IReadOnlyCollection<int> input, IReadOnlyCollection<int>? output)
        {
            Id = id;
            Status = status;
            Duration = duration;
            Input = input;
            Output = output;
        }

        public Guid Id { get; }
        public SortJobStatus Status { get; }
        public TimeSpan? Duration { get; }
        public IReadOnlyCollection<int> Input { get; }
        public IReadOnlyCollection<int>? Output { get; }
    } 
}
