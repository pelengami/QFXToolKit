using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    [RequireComponent(typeof(PhysicsMotion))]
    public class CollisionBasedFxInstancer : ControlledObject
    {
        public FxObject[] FxObjects;

        private void Awake()
        {
            var physicsMotion = GetComponent<PhysicsMotion>();

            physicsMotion.OnCollision += delegate(ContactPoint contactPoint)
            {
                foreach (var fxObject in FxObjects)
                {
                    fxObject.TargetPosition = contactPoint.point;
                    FxObjectInstancer.InstantiateOnCollisionFx(fxObject, contactPoint);
                }
            };
        }
    }
}