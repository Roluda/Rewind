using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Rewind
{
    public class MazeRunner : MonoBehaviour
    {
        [SerializeField, AssetList]
        MazeRule[] mazeRules;
        [SerializeField]
        MazeMap mazeMap;
        [SerializeField]
        float spacing;
        [SerializeField]
        Transform context;

        [Button]
        public void Generate()
        {
            int maxX = mazeMap.Maze.GetLength(0);
            int maxY = mazeMap.Maze.GetLength(1);


            for (int x = 0; x<maxX; x++)
            {
                for(int y = 0; y<maxY; y++)
                {
                    var bestRule = mazeRules.OrderByDescending(rule => rule.Matches(mazeMap.Maze, x, y)).First();
                    if (bestRule.Prefab)
                    {
                        var offset = new Vector3(-maxX / 2, 0, -maxY / 2) * spacing;
                        var pos = offset + new Vector3(x, 0, y) * spacing;
                        var spawn = PrefabUtility.InstantiatePrefab(bestRule.Prefab) as GameObject;
                        spawn.transform.position = pos;
                        spawn.transform.SetParent(context, true);
                    }
                }
            }
        }

        [Button]
        public void Clear()
        {
            for(int i = context.childCount -1; i>=0; i--)
            {
                DestroyImmediate(context.GetChild(i).gameObject);
            }
        }
    }
}
