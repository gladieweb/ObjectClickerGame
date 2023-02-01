using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class ClickObjectFactory : Singleton<ClickObjectFactory>
    {
        public GameObject clickObjectPrefab;
        public ClickObject[] AllClickObjectsData;

        private HashSet<ClickObjectBehaviour> Objects = new HashSet<ClickObjectBehaviour>();

        public void RemoveAllObjects()
        {
            foreach (var obj in Objects)
            {
                Destroy(obj.gameObject);
            }
            Objects.Clear();
        }
        public ClickObjectBehaviour CreateRandomClickObject(Rect spawnSpace)
        {
            var rnd = Random.Range(0, AllClickObjectsData.Length);
            var data = AllClickObjectsData[rnd];
            
            return SpawnObject(spawnSpace, data);
        }

        private ClickObjectBehaviour SpawnObject(Rect spawnSpace, ClickObject data)
        {
            var go = GameObject.Instantiate(clickObjectPrefab);
            go.transform.position = GetNewPosition(spawnSpace);

            SpawnRenderObject(data, go);

            var behaviour = go.GetComponent<ClickObjectBehaviour>();
            Objects.Add(behaviour);

            behaviour.Initialize(data);
            return behaviour;
        }

        public ClickObjectBehaviour CreatePriorityClickObject(Rect spawnSpace)
        {
            var data = AllClickObjectsData.FirstOrDefault(x => x.isSpawnPriority);
            return SpawnObject(spawnSpace, data);
        }

        private static void SpawnRenderObject(ClickObject data, GameObject go)
        {
            var goRender = GameObject.Instantiate(data.prefab, go.transform);
            goRender.transform.localPosition = Vector3.zero;
            goRender.transform.localScale = Vector3.one;
        }

        private Vector3 GetNewPosition(Rect spawnSpace)
        {
            var x = Random.Range(spawnSpace.x, spawnSpace.x + spawnSpace.width);
            var y = Random.Range(spawnSpace.y, spawnSpace.y + spawnSpace.height);
            return Vector3.right * x + Vector3.up * y + Vector3.forward * 0;
        }

        public void Remove(ClickObjectBehaviour behaviour)
        {
            Objects.Remove(behaviour);
            GameObject.Destroy(behaviour.gameObject);
        }

        public int GetObjectsAmount()
        {
            return Objects.Count;
        }

        public void Scramble(Rect spawnSpace)
        {
            foreach (var obj in Objects)
            {
                if (!obj.data.scrambleAll)
                {
                    obj.transform.position = GetNewPosition(spawnSpace);
                }
            }
        }
    }
    
}