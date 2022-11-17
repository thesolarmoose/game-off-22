using System.Threading;
using System.Threading.Tasks;

namespace Interactions
{
    public interface IInteractionEvent
    {
        /// <summary>
        /// Async function that returns whether interaction should keep invoking subsequent events
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> ExecuteEvent(CancellationToken ct);
    }
}