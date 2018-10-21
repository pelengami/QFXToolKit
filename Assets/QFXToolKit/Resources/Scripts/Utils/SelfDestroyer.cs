// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class SelfDestroyer : ControlledObject
    {
        public float LifeTime;

        public override void Run()
        {
            base.Run();
            Destroy(gameObject, LifeTime);
        }
    }
}