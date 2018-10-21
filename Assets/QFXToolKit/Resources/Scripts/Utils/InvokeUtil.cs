using System;
using System.Collections;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public static class InvokeUtil
    {
        public static void RunLater(MonoBehaviour monoBehaviour, Action method, float waitSeconds)
        {
            if (waitSeconds < 0 || method == null)
            {
                return;
            }

            monoBehaviour.StartCoroutine(RunLaterCoroutine(method, waitSeconds));
        }

        public static IEnumerator RunLaterCoroutine(Action method, float waitSeconds)
        {
            yield return new WaitForSeconds(waitSeconds);
            method();
        }
    }
}