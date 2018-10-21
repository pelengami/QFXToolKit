using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    [RequireComponent(typeof(ColliderCollisionDetector))]
    public class PhysicsMotion : ControlledObject, ICollisionsProvider
    {
        public float ColliderRadius = 0.1f;
        public float Speed;
        public float Mass;
        public float Drag;
        public bool UseGravity;
        public ForceMode ForceMode;
        public Transform Target;

        public bool DestroyAfterCollision;
        public float DestroyAfterCollisionTimeout;

        private SphereCollider _sphereCollider;
        private Rigidbody _rigidbody;
        private bool _wasCollided;

        public event Action<CollisionPoint> OnCollision;

        public override void Run()
        {
            var targetDirection =
                Target != null ? (Target.position - transform.position).normalized : transform.forward;

            _rigidbody.AddForce(targetDirection * Speed, ForceMode);

            base.Run();
        }

        private void Awake()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _rigidbody.mass = Mass;
            _rigidbody.drag = Drag;
            _rigidbody.useGravity = UseGravity;

            _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            _sphereCollider = gameObject.AddComponent<SphereCollider>();
            _sphereCollider.radius = ColliderRadius;

            var colliderCollisionDetector = GetComponent<ColliderCollisionDetector>();
            colliderCollisionDetector.OnCollision += delegate
            {
                if (DestroyAfterCollision)
                    Destroy(gameObject, DestroyAfterCollisionTimeout);
            };
        }
    }
}