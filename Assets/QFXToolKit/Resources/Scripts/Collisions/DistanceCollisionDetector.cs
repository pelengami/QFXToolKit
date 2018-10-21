using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class DistanceCollisionDetector : ControlledObject, ICollisionsProvider
    {
        public Vector3 TargetPosition;
        public float CollisionDistance;

        private bool _wasCollided;

        public event Action<CollisionPoint> OnCollision;

        private void Update()
        {
            if (!IsRunning)
                return;

            if (_wasCollided)
                return;

            var distance = Vector3.Distance(transform.position, TargetPosition);
            if (distance <= CollisionDistance)
            {
                RaycastHit hit;
                if (!Physics.Raycast(transform.position, transform.forward, out hit, CollisionDistance))
                    return;
                
                Debug.Log(hit.transform.name);

                if (!_wasCollided)
                    _wasCollided = true;

                if (OnCollision != null)
                    OnCollision.Invoke(new CollisionPoint
                    {
                        Point = hit.point,
                        Normal = hit.normal
                    });
            }
        }
    }
}