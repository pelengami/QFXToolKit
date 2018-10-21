using System;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public interface ICollisionsProvider
    {
        event Action<CollisionPoint> OnCollision;
    }
}