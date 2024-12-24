using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PausableUniTask
{
    public struct ChainScope : IDisposable
    {
        private TaskTokenSource parent;
        private TaskTokenSource child;
        
        public ChainScope(TaskTokenSource parent, TaskTokenSource child)
        {
            this.parent = parent;
            this.child = child;
        }

        public void Dispose()
        {
            parent.Unchain(child);
        }
    }
}
