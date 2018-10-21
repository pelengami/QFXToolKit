using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class MaterialReplacer : ControlledObject
    {
        public Material Material;
        public float LifeTime;

        private Dictionary<Renderer, Material[]> _originalMaterials;

        public override void Setup()
        {
            base.Setup();
            
            _originalMaterials = MaterialUtil.GetOriginalMaterials(gameObject);
            MaterialUtil.ReplaceMaterial(gameObject, Material);
        }

        public override void Run()
        {
            base.Run();
            
            //Revert
            InvokeUtil.RunLater(this, delegate
            {
                MaterialUtil.ReplaceMaterial(_originalMaterials);
                _originalMaterials.Clear();
            }, LifeTime);
        }
    }
}