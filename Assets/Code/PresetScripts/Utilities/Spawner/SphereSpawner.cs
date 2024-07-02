using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DhafinFawwaz.Spawner
{
    public class SphereSpawner : Spawner
    {
        [SerializeField] float _radius = 5;

        public SphereSpawner SetRadius(float radius)
        {
            _radius = radius;
            return this;
        }

        public override Vector3 GetRandomPosition()
        {
            return Random.insideUnitSphere * _radius;
        }

    #if UNITY_EDITOR

        void OnDrawGizmosSelected()
        {
            if(!_drawGizmos) return;
            if(_predictedPositions.Count != _amount) RefreshPredictedPositions();

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radius);

            Gizmos.color = Color.red;
            foreach(Vector3 pos in _predictedPositions)
            {
                Gizmos.DrawSphere(transform.position + pos, 0.1f);
            }
        }
    #endif
    }
}
