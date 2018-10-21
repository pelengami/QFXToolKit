using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public sealed class RayCastCollisionDetector : ControlledObject, ICollisionsProvider
    {
        public float RayCastRange = 100;
        public float Distance;

        public event Action<CollisionPoint> OnCollision;

        private Transform _transform;
        private bool _wasCollisionDetected;

        private void Start()
        {
            _transform = transform;
        }

        private void LateUpdate()
        {
            if (_wasCollisionDetected)
                return;

            RaycastHit hit;
            if (!Physics.Raycast(_transform.position + transform.forward, transform.forward, out hit, RayCastRange))
                return;

            var point = hit.point;
            var distance = Vector3.Distance(point, _transform.position);
            if (distance > Distance)
                return;

            _wasCollisionDetected = true;

            if (OnCollision != null)
                OnCollision.Invoke(new CollisionPoint
                {
                    Point = hit.point,
                    Normal = hit.normal
                });
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position + transform.forward, transform.forward * 2);
        }
    }
}