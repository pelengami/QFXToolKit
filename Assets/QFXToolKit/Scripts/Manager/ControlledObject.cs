using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class ControlledObject : MonoBehaviour, IControlledObject
    {
        public bool RunAtStart;
        public float Delay;

        public bool IsRunning { get; private set; }

        private void OnEnable()
        {
            if (RunAtStart)
            {
                InvokeUtil.RunLater(this, delegate
                {
                    Setup();
                    Run();
                }, Delay);
            }
        }

        public virtual void Setup()
        {
        }

        public virtual void Run()
        {
            IsRunning = true;

            gameObject.SetActive(true);
        }

        public virtual void Stop()
        {
            IsRunning = false;

            gameObject.SetActive(false);
        }
    }
}