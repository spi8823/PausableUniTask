using System;

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

        public Action OnCanceled { get; set; }
        public Action OnSkipped { get; set; }
        public Action OnPaused { get; set; }
        public Action OnResumed { get; set; }

        public TaskTokenSource() { }

        public void Reset()
        {
            isCanceled = false;
            isSkipped = false;
            isPaused = false;
            OnCanceled = null;
            OnSkipped = null;
            OnPaused = null;
            OnResumed = null;
        }

        public TaskToken CreateToken()
        {
            return new TaskToken(this);
        }

        public void Cancel()
        {
            isCanceled = true;
            OnCanceled?.Invoke();
        }

        public void Skip()
        {
            isSkipped = true;
            OnSkipped?.Invoke();
        }

        public void Pause()
        {
            isPaused = true;
            OnPaused?.Invoke();
        }

        public void Resume()
        {
            isPaused = false;
            OnResumed?.Invoke();
        }

        public ChainScope Chain(TaskTokenSource child)
        {
            OnCanceled += child.Cancel;
            OnSkipped += child.Skip;
            OnPaused += child.Pause;
            OnResumed += child.Resume;
            return new(this, child);
        }

        public void Unchain(TaskTokenSource child)
        {
            OnCanceled -= child.Cancel;
            OnSkipped -= child.Skip;
            OnPaused -= child.Pause;
            OnResumed -= child.Resume;
        }

        public void Dispose()
        {
            Reset();
            ObjectPool<TaskTokenSource>.Push(this);
        }
    }
}
