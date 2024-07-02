using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DhafinFawwaz.Spawner
{
    public abstract class Spawner : MonoBehaviour
    {
        [Tooltip("The prefab to spawn")]
        [SerializeField] protected GameObject _prefab;

        [Tooltip("The amount of objects to spawn")]
        [SerializeField] protected int _amount = 16;

        /// <summary>
        /// Spawns the object
        /// </summary>
        /// <returns></returns>
        public abstract List<GameObject> Spawn();

        public abstract void Reposition();
    }
}
