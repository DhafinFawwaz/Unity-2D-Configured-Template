using UnityEngine;

namespace DhafinFawwaz.Spawner
{
    public class CircleSpawner : SphereSpawner
    {
        public override Vector3 GetRandomPosition()
        {
            Vector3 v = base.GetRandomPosition();
            v.z = 0;
            return v;
        }
    }
}