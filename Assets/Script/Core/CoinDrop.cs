using ArcheroClone.pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TestTask.Core
{
    public class CoinDrop : MonoBehaviour
    {
        public static event Action<CoinDrop> OnCoinDrop;
          private void Awake()
        {
            if (OnCoinDrop != null)
                OnCoinDrop(this);
        }

        internal void Remove()
        {
            Invoke(nameof( RemoveCoin), .55f);
        }

        private void RemoveCoin()
        {
            GameHandler.instance.CoinPool.Return(this);
        }
    }
}
