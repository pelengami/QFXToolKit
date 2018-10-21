using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class PhysicsHomingMissileLauncher : ControlledObject
    {
        public bool FollowTarget;
        public Transform LaunchTarget;

        public PhysicsHomingMissile Missile;
        public float MissileVelocity;
        public float MissileLaunchForce;
        public float MissileTurn;
        public int MissilesCount = 1;
        public float InstantiateMissileDelay;
        public float FocusOnTargetDelay;
        public float RandomSphereSize;

        private TargetMarker _targetMarker;

        public override void Run()
        {
            base.Run();

            StartCoroutine("InstantiateProjectiles");
        }

        private void Awake()
        {
            _targetMarker = GetComponent<TargetMarker>();
        }

        private IEnumerator InstantiateProjectiles()
        {
            int targetCounts = 0;
            int targetIdx = 0;
            var targetsGameObjects = new List<GameObject>();
            var targetsPositions = new List<Vector3>();

            if (_targetMarker != null)
            {
                if (_targetMarker.MarkTargetMode == TargetMarker.MarkMode.GameObject)
                {
                    targetsGameObjects = _targetMarker.MarkedGameObjects;
                    targetCounts = targetsGameObjects.Count;
                }
                else
                {
                    targetsPositions = _targetMarker.MarkedPositions;
                    targetCounts = targetsPositions.Count;
                }
            }

            for (int i = 0; i < MissilesCount; i++)
            {
                var randomPosition = LaunchTarget.position + Random.insideUnitSphere * RandomSphereSize;

                var homingMissile = Instantiate(Missile, transform.position, transform.rotation);

                homingMissile.FollowTarget = FollowTarget;

                if (_targetMarker != null)
                {
                    if (targetCounts > 0)
                    {
                        if (targetIdx >= targetCounts)
                            targetIdx = 0;

                        if (_targetMarker.MarkTargetMode == TargetMarker.MarkMode.GameObject)
                            homingMissile.TargetTransform = targetsGameObjects[targetIdx++].transform;
                        else homingMissile.TargetPosition = targetsPositions[targetIdx++];
                    }
                }

                homingMissile.FocusOnTargetDelay = FocusOnTargetDelay;
                homingMissile.LaunchPosition = randomPosition;

                homingMissile.Velocity = MissileVelocity;
                homingMissile.Turn = MissileTurn;
                homingMissile.LaunchForce = MissileLaunchForce;

                homingMissile.Run();

                yield return new WaitForSeconds(InstantiateMissileDelay);
            }
        }
    }
}