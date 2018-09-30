using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class PhysicsProjectileLauncher : MonoBehaviour
    {
        public PhysicsMotion Projectile;
        public float RotationSpeed;

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var targetRotation = Quaternion.LookRotation(hit.point - transform.position);
                transform.rotation =
                    Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            }


            if (Input.GetMouseButtonDown(0))
            {
                var projectile = Instantiate(Projectile, transform.position, transform.rotation);
                projectile.Setup();
                projectile.Run();
            }
        }
    }
}