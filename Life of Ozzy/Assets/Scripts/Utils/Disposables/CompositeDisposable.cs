using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeOfOzzy.Utils
{
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public void Retain(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            foreach(var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _disposables.Clear();
        }
    }

}
