// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public interface IControlledObject
    {
        bool IsRunning { get; }
        void Setup();
        void Run();
        void Stop();
    }
}