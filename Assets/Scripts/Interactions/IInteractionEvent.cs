
using System;
using System.Threading;
using System.Threading.Tasks;
using Items;

namespace Interactions
{
    public interface IInteractionEvent
    {
        /// <summary>
        /// Async function that returns whether interaction should keep invoking subsequent events
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<bool> ExecuteEvent(Item item, CancellationToken ct);
    }
}