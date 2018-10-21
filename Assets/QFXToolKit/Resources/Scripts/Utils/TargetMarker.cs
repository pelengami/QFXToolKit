using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    [RequireComponent(typeof(MaterialAdder))]
    public class TargetMarker : MonoBehaviour
    {
        public float LifeTime;

        public FxObject MarkFx;

        public MarkMode MarkTargetMode;

        public int MaxTargets;

        private readonly Dictionary<int, GameObject> _markedObjects = new Dictionary<int, GameObject>();
        private readonly List<Vector3> _markedPositions = new List<Vector3>();

        private MaterialAdder _materialAdder;

        public List<GameObject> MarkedGameObjects
        {
            get { return _markedObjects.Values.ToList(); }
        }

        public List<Vector3> MarkedPositions
        {
            get { return new List<Vector3>(_markedPositions); }
        }

        private void Awake()
        {
            _materialAdder = GetComponent<MaterialAdder>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (MarkFx != null)
                    {
                        MarkFx.TargetPosition = hit.point;
                        FxObjectInstancer.InstantiateOnCollisionFx(MarkFx, new CollisionPoint
                        {
                            Point = hit.point,
                            Normal = hit.normal
                        });
                    }

                    if (MarkTargetMode == MarkMode.GameObject)
                    {
                        if (_markedObjects.Count >= MaxTargets)
                            return;

                        var gameObjectId = hit.transform.gameObject.GetInstanceID();
                        if (_markedObjects.ContainsKey(gameObjectId))
                            return;

                        _markedObjects[gameObjectId] = hit.transform.gameObject;
                        var materialAdded = hit.transform.gameObject.AddComponent<MaterialAdder>();
                        materialAdded.Material = _materialAdder.Material;
                        materialAdded.LifeTime = _materialAdder.LifeTime;
                        materialAdded.Run();

                        InvokeUtil.RunLater(this, () =>
                        {
                            Destroy(materialAdded);
                            _markedObjects.Remove(gameObjectId);
                        }, LifeTime);
                    }
                    else
                    {
                        if (_markedPositions.Count >= MaxTargets)
                            return;
                        _markedPositions.Add(hit.point);
                        InvokeUtil.RunLater(this, () => { _markedPositions.Remove(hit.point); }, LifeTime);
                    }
                }
            }
        }

        public enum MarkMode
        {
            GameObject = 0,
            Position = 1
        }
    }
}