using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class MaterialAdder : ControlledObject
    {
        public Material Material;
        public float LifeTime;

        private Dictionary<Renderer, Material[]> _rendToMaterialsMap;

        public override void Run()
        {
            _rendToMaterialsMap = MaterialUtil.GetOriginalMaterials(gameObject);

            MaterialUtil.AddMaterial(gameObject, Material);

            InvokeUtil.RunLater(this, Revert, LifeTime);
        }

        public void Revert()
        {
            MaterialUtil.ReplaceMaterial(_rendToMaterialsMap);
            _rendToMaterialsMap.Clear();
        }

        private void OnEnable()
        {
            if (RunAtStart)
            {
                InvokeUtil.RunLater(this, delegate
                {
                    Setup();
                    Run();
                }, Delay);
            }
        }
    }
}