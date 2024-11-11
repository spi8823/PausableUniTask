using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace PausableUniTask
{
    public struct TaskToken
    {
        private TaskTokenSource source { get; }

        public bool isCanceled => source.isCanceled;
        public bool isSkipped => source.isSkipped;
        public bool isPaused => source.isPaused;

        public TaskToken(TaskTokenSource source)
        {
            this.source = source;
        }
    }
}
