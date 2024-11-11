using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace PausableUniTask
{
    public static class Pausable
    {
        public static async UniTask WaitForSeconds(float duration, TaskToken token, PlayerLoopTiming timing = PlayerLoopTiming.Update)
        {
            var time = 0f;
            while (time < duration)
            {
                if (token.isCanceled || token.isSkipped)
                    break;

                if (!token.isPaused)
                    time += Time.deltaTime;

                await UniTask.Yield(timing);
            }
        }
    }
}
