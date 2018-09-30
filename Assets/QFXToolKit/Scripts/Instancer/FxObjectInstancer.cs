using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public static class FxObjectInstancer
    {
        public static void InstantiateOnCollisionFx(FxObject fxObject, ContactPoint contactPoint)
        {
            var go = Object.Instantiate(fxObject.Fx);
            go.transform.position = fxObject.TargetPosition;

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (fxObject.FxRotation == FxRotationType.Normal)
            {
                go.transform.rotation = Quaternion.FromToRotation(go.transform.up, contactPoint.normal) *
                                        go.transform.rotation;
            }
            else
            {
                go.transform.rotation = Quaternion.identity;
            }

            go.SetActive(true);
        }
    }
}