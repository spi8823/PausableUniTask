using Cysharp.Threading.Tasks;
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
        public bool isAlive => source.isAlive;

        public TaskToken(TaskTokenSource source)
        {
            this.source = source;
        }

        public async UniTask WaitWhilePaused()
        {
            while (isAlive && isPaused)
                await UniTask.Yield();
        }

        public ChainScope Chain(TaskTokenSource child)
        {
            return source.Chain(child);
        }

        public void Unchain(TaskTokenSource child)
        {
            source.Unchain(child);
        }
    }
}
