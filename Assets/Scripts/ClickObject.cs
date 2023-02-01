using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "ClickObject", menuName = "TEST/ClickObject", order = 0)]
    public class ClickObject : ScriptableObject
    {
        public GameObject prefab;
        public int clicksToDestroy = 1;
        public int lifespan = 5;
        public int givePoints = 5;
        public int losePoints = 1;
        public bool threeCoin = false;
        public float fixedExtraTime = 0;
        public float per20PointExtraTime = 0;
        public float duplicateMouseSpeedTime = 0;
        public bool scrambleAll = false;
        public float blockCapabilitiesTime = 0;
        public bool from1RandomPoints = false;
        public float clockSpeed = 1.0f;
        public bool duplicateGivenPoints = false;
        public bool isSpawnPriority = false;
    }
}