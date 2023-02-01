using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "GameDifficultSetting", menuName = "TEST/GameDifficultSetting", order = 0)]
    public class GameDifficultSetting : ScriptableObject
    {
        public string ID;
        public float minSpawnTime;
        public float maxSpawnTime;
        public int minScreenObjects;
        public int maxScreenObjects;
    }
}