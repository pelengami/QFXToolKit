using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class PhysicsMotion : ControlledObject
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

        public event Action<ContactPoint> OnCollision;

        public override void Setup()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _rigidbody.mass = Mass;
            _rigidbody.drag = Drag;
            _rigidbody.useGravity = UseGravity;

            _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            base.Setup();
        }

        public override void Run()
        {
            var targetDirection =
                Target != null ? (Target.position - transform.position).normalized : transform.forward;

            _rigidbody.AddForce(targetDirection * Speed, ForceMode);

            base.Run();
        }

        private void Awake()
        {
            _sphereCollider = gameObject.AddComponent<SphereCollider>();
            _sphereCollider.radius = ColliderRadius;
        }

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
                    OnCollision.Invoke(contact);
            }

            if (DestroyAfterCollision && _wasCollided)
                Destroy(gameObject, DestroyAfterCollisionTimeout);
        }
    }
}