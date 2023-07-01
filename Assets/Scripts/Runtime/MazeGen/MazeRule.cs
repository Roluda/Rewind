using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Rewind
{
    [CreateAssetMenu(fileName = "MazeRule", menuName = "MazeRule")]
    public class MazeRule : SerializedScriptableObject
    {
        [SerializeField, TableMatrix(DrawElementMethod = nameof(DrawCell), SquareCells = true, ResizableColumns = false)]
        public bool[,] WallMatrix = new bool[3, 3];
        [SerializeField]
        public GameObject Prefab;

        static bool DrawCell(Rect rect, bool value)
        {
#if UNITY_EDITOR
            if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
            {
                value = !value;
                GUI.changed = true;
                Event.current.Use();
            }
            Color color = value ? Color.white : Color.black;
            EditorGUI.DrawRect(rect.Padding(1), color);
#endif
            return value;
        }


        public int Matches(bool[,] maze, int x, int y)
        {
            y = maze.GetLength(1) - 1 - y;
            if (maze[x, y] == false && WallMatrix[1, 1] == false)
            {
                return 10;
            }
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    bool boundsX = x + i >= 0 && x + i < maze.GetLength(0);
                    bool boundsY = y + j >= 0 && y + j < maze.GetLength(1);
                    bool bounds = boundsX && boundsY;
                    bool value = bounds ? maze[x + i, y + j] : false;
                    if (WallMatrix[i + 1, j + 1] == value)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
