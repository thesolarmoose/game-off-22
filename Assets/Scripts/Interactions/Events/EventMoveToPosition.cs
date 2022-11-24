using System.Threading;
using System.Threading.Tasks;
using Items;
using Movement;
using Pathfinding;
using UnityEngine;
using Utils.Attributes;

namespace Interactions.Events
{
    public class EventMoveToPosition : MonoBehaviour, IInteractionEvent
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private OnClickDestinationSetter _clickMovementController;

        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private AIPath _characterMover;

        [SerializeField] private Transform _leftPosition;
        [SerializeField] private Transform _rightPosition;

        public async Task<bool> ExecuteEvent(Item item, CancellationToken ct)
        {
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var linkedCt = linkedCts.Token;

            try
            {
                var moveTask = WaitForDestinationReached(linkedCt);
                var waitMoveRequestTask = _clickMovementController.WaitForMovementRequest(linkedCt);

                var finishedTask = await Task.WhenAny(moveTask, waitMoveRequestTask);
                await finishedTask; // propagate exceptions hack

                linkedCts.Cancel();
                
                bool shouldContinue = finishedTask != waitMoveRequestTask;

                return shouldContinue;
            }
            finally
            {
                linkedCts.Dispose();
            }
        }

        private async Task WaitForDestinationReached(CancellationToken ct)
        {
            // move the character to desired position
            var positionToMove = GetPositionToMove();
            _clickMovementController.transform.position = positionToMove;
            
            // wait to for path recalculation
            while (_characterMover.reachedEndOfPath && !ct.IsCancellationRequested)
            {
                await Task.Yield();
            }
            
            // wait to reach position
            while (!_characterMover.reachedEndOfPath && !ct.IsCancellationRequested)
            {
                await Task.Yield();
            }
        }

        private Vector2 GetPositionToMove()
        {
            var position = transform.position;
            var characterPosition = _characterMover.transform.position;

            bool comesFromRight = position.x < characterPosition.x;

            return comesFromRight ? _rightPosition.position : _leftPosition.position;
        }
    }
}