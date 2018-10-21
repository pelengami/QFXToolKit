using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public static class MaterialUtil
    {
        public static void ReplaceMaterial(GameObject targetGo, Material templateMaterial)
        {
            var newMaterial = new Material(templateMaterial);

            var rends = targetGo.GetComponentsInChildren<Renderer>();

            foreach (var rend in rends)
            {
                var materialsLength = rend.sharedMaterials.Length;
                var materials = new Material[materialsLength];
                for (int i = 0; i < materialsLength; i++)
                    materials[i] = newMaterial;
                rend.sharedMaterials = materials;
            }
        }

        public static void AddMaterial(GameObject targetGo, Material templateMaterial)
        {
            var newMaterial = new Material(templateMaterial);

            var rends = targetGo.GetComponentsInChildren<Renderer>();

            foreach (var rend in rends)
            {
                var materialsLength = rend.sharedMaterials.Length + 1;
                var materials = new Material[materialsLength];
                for (int i = 0; i < rend.sharedMaterials.Length; i++)
                    materials[i] = rend.sharedMaterials[i];
                materials[materialsLength - 1] = newMaterial;
                rend.sharedMaterials = materials;
            }
        }

        public static void ReplaceMaterial(Dictionary<Renderer, Material[]> rendererToMaterials)
        {
            foreach (var rendToMaterials in rendererToMaterials)
                rendToMaterials.Key.sharedMaterials = rendToMaterials.Value;
        }

        public static Dictionary<Renderer, Material[]> GetOriginalMaterials(GameObject targetGo)
        {
            var rendererToMaterials = new Dictionary<Renderer, Material[]>();
            var rends = targetGo.GetComponentsInChildren<Renderer>();
            foreach (var rend in rends)
                rendererToMaterials[rend] = rend.sharedMaterials;
            return rendererToMaterials;
        }
    }
}