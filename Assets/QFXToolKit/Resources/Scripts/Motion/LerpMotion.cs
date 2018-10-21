using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class LerpMotion : ControlledObject
    {
        public float Speed = 1f;
        public float TurningSpeed = 1f;
        public Vector3 StartPosition;
        public Vector3 EndPosition;

        private bool _isLerping;
        private float _timeStartedLerping;

        public override void Run()
        {
            base.Run();

            _isLerping = true;
            _timeStartedLerping = Time.time;
        }

        private void FixedUpdate()
        {
            if (!IsRunning || !_isLerping)
                return;

            var timeSinceStarted = Time.time - _timeStartedLerping;
            var percentageComplete = timeSinceStarted / Speed;

            transform.position = Vector3.Lerp(StartPosition, EndPosition, percentageComplete);

            if (percentageComplete >= 1.0f)
                _isLerping = false;
        }
    }
}