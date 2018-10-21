using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class ColliderCollisionDetector : ControlledObject, ICollisionsProvider
    {
        private bool _wasCollided;

        public event Action<CollisionPoint> OnCollision;

        private void OnCollisionEnter(Collision collision)
        {
            if (!IsRunning)
                return;

            if (_wasCollided)
                return;

            foreach (var contact in collision.contacts)
            {
                if (!_wasCollided)
                    _wasCollided = true;

                if (OnCollision != null)
                    OnCollision.Invoke(new CollisionPoint
                    {
                        Point = contact.point,
                        Normal = contact.normal
                    });
            }
        }
    }
}