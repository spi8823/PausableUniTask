using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace PausableUniTask
{
    public class TaskTokenSource : IPoolable<TaskTokenSource>
    {
        public static TaskTokenSource Create()
        {
            return ObjectPool<TaskTokenSource>.Pop();
        }

        public bool isCanceled { get; private set; }
        public bool isSkipped { get; private set; }
        public bool isPaused { get; private set; }
        public bool isAlive => !isCanceled && !isSkipped;

        public TaskTokenSource() { }

        public void Reset()
        {
            isCanceled = false;
            isSkipped = false;
            isPaused = false;
        }

        public TaskToken CreateToken()
        {
            return new TaskToken(this);
        }

        public void Cancel()
        {
            isCanceled = true;
        }

        public void Skip()
        {
            isSkipped = true;
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Resume()
        {
            isPaused = false;
        }

        public void Dispose()
        {
            Reset();
            ObjectPool<TaskTokenSource>.Push(this);
        }
    }
}
