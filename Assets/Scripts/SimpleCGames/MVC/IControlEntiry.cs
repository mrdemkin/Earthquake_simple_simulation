using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleCGames.FSM
{
    public interface IControlEntiry
    {
        OperationStatus PreInit();
        OperationStatus ReadyToInit();
        OperationStatus Initing();
        OperationStatus PostInit();
        OperationStatus Finished();
    }

    public struct OperationStatus
    {
        public bool IsSuccess;
        public bool IsAsync;

        public OperationStatus(bool isSuccess, bool isMayAsync) // bool isRuinningNow)
        {
            this.IsSuccess= isSuccess;
            this.IsAsync= isMayAsync;
        }
    }
}