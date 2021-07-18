using System.Threading.Tasks;

namespace Sorting.Api
{
    public interface ISortJobProcessor
    {
        Task<SortJob> Process(SortJob job);
        Task<SortJob[]> AllJobs();

        Task<SortJob> JobById(System.Guid Id);
    }
}