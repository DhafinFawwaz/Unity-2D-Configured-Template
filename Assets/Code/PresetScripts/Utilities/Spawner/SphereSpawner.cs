using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DhafinFawwaz.Spawner
{
    public class SphereSpawner : Spawner
    {
        [SerializeField] float _radius = 5;

        public override List<GameObject> Spawn()
        {
            List<GameObject> spawnedObjects = new List<GameObject>();
            for(int i = 0; i < _amount; i++)
            {
                Vector3 spawnPosition = GetRandomPosition();
                GameObject spawnedObject = Instantiate(_prefab, transform.position + spawnPosition, Quaternion.identity);
                spawnedObjects.Add(spawnedObject);
            }
            return spawnedObjects;
        }

        public override void Reposition()
        {
            foreach(Transform child in transform)
            {
                Vector3 spawnPosition = GetRandomPosition();
                child.position = transform.position + spawnPosition;
            }
        }

        protected virtual Vector3 GetRandomPosition()
        {
            return Random.insideUnitSphere * _radius;
        }

    #if UNITY_EDITOR
        List<Vector3> _predictedPositions = new List<Vector3>();
        [SerializeField] bool _drawGizmos = true;

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

        void RefreshPredictedPositions()
        {
            _predictedPositions.Clear();
            for(int i = 0; i < _amount; i++)
            {
                _predictedPositions.Add(GetRandomPosition());
            }
        }

        void OnValidate()
        {
            RefreshPredictedPositions();
        }
    #endif
    }
}
