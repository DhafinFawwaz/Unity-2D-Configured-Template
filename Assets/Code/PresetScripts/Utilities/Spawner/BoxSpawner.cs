using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DhafinFawwaz.Spawner
{
    public class BoxSpawner : Spawner
    {
        [SerializeField] Vector3 _spawnArea = new Vector3(5, 5, 5);
        [SerializeField] Vector3 _spawnRotation = new Vector3(0, 0, 0);
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

        Vector3 GetRandomPosition()
        {
            Vector3 v = new Vector3(
                Random.Range(-_spawnArea.x/2, _spawnArea.x/2),
                Random.Range(-_spawnArea.y/2, _spawnArea.y/2),
                Random.Range(-_spawnArea.z/2, _spawnArea.z/2)
            );
            v = Quaternion.Euler(_spawnRotation) * v;
            return v;
        }

    #if UNITY_EDITOR
        List<Vector3> _predictedPositions = new List<Vector3>();
        [SerializeField] bool _drawGizmos = true;
        void OnDrawGizmosSelected()
        {
            if(!_drawGizmos) return;
            if(_predictedPositions.Count != _amount) RefreshPredictedPositions();

            Gizmos.color = Color.green;
            // handle rotation
            Vector3[] corners = new Vector3[8];
            corners[0] = new Vector3(-_spawnArea.x/2, -_spawnArea.y/2, -_spawnArea.z/2);
            corners[1] = new Vector3(-_spawnArea.x/2, -_spawnArea.y/2, _spawnArea.z/2);
            corners[2] = new Vector3(-_spawnArea.x/2, _spawnArea.y/2, -_spawnArea.z/2);
            corners[3] = new Vector3(-_spawnArea.x/2, _spawnArea.y/2, _spawnArea.z/2);
            corners[4] = new Vector3(_spawnArea.x/2, -_spawnArea.y/2, -_spawnArea.z/2);
            corners[5] = new Vector3(_spawnArea.x/2, -_spawnArea.y/2, _spawnArea.z/2);
            corners[6] = new Vector3(_spawnArea.x/2, _spawnArea.y/2, -_spawnArea.z/2);
            corners[7] = new Vector3(_spawnArea.x/2, _spawnArea.y/2, _spawnArea.z/2);
            for(int i = 0; i < 8; i++)
            {
                corners[i] = Quaternion.Euler(_spawnRotation) * corners[i];
            }
            Gizmos.DrawLine(transform.position + corners[0], transform.position + corners[1]);
            Gizmos.DrawLine(transform.position + corners[1], transform.position + corners[3]);
            Gizmos.DrawLine(transform.position + corners[3], transform.position + corners[2]);
            Gizmos.DrawLine(transform.position + corners[2], transform.position + corners[0]);
            Gizmos.DrawLine(transform.position + corners[4], transform.position + corners[5]);
            Gizmos.DrawLine(transform.position + corners[5], transform.position + corners[7]);
            Gizmos.DrawLine(transform.position + corners[7], transform.position + corners[6]);
            Gizmos.DrawLine(transform.position + corners[6], transform.position + corners[4]);
            Gizmos.DrawLine(transform.position + corners[0], transform.position + corners[4]);
            Gizmos.DrawLine(transform.position + corners[1], transform.position + corners[5]);
            Gizmos.DrawLine(transform.position + corners[2], transform.position + corners[6]);
            Gizmos.DrawLine(transform.position + corners[3], transform.position + corners[7]);



            Gizmos.color = Color.red;
            foreach(Vector3 position in _predictedPositions)
            {
                Gizmos.DrawSphere(transform.position + position, 0.1f);
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
