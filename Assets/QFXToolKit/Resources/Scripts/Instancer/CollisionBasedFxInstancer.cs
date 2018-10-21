using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    [RequireComponent(typeof(ICollisionsProvider))]
    public class CollisionBasedFxInstancer : ControlledObject
    {
        public FxObject[] FxObjects;

        private bool _wasCollided;

        private void Awake()
        {
            var collisionProviders = GetComponents<ICollisionsProvider>();
            foreach (var collisionsProvider in collisionProviders)
            {
                collisionsProvider.OnCollision += delegate(CollisionPoint collisionPoint)
                {
                    if (_wasCollided)
                        return;

                    foreach (var fxObject in FxObjects)
                    {
                        fxObject.TargetPosition = collisionPoint.Point;
                        FxObjectInstancer.InstantiateOnCollisionFx(fxObject, collisionPoint);
                    }

                    _wasCollided = true;
                };
            }
        }
    }
}