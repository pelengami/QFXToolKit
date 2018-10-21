using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class MouseControlledObjectLauncher : MonoBehaviour
    {
        public ControlledObject ControlledObject;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ControlledObject.Setup();
                ControlledObject.Run();
            }
        }
    }
}