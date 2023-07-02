using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Rewind
{
    [CreateAssetMenu(fileName = "MazeMap", menuName = "Maze")]
    public class MazeMap : SerializedScriptableObject
    {
        [Button]
        public void Clear()
        {
            Maze = new bool[0, 0];
        }

        [SerializeField, TableMatrix(DrawElementMethod = nameof(DrawCell), SquareCells = true)]
        public bool[,] Maze;

        [Button]
        public void SetSize(int width, int height)
        {
            Maze = new bool[width, height];
        }

        [Button, Tooltip("wall is black")]
        public void FromTexture(int width, int height, Texture2D texture)
        {
            Maze = new bool[width, height];
            var pixels = texture.GetPixels();
            for(int x = 0; x < texture.width; x++)
            {
                for(int y = 0; y <texture.height; y++)
                {
                    int index = y * texture.width + x;
                    var col = pixels[index];
                    if(col.r * col.g * col.b < 0.5f)
                    {
                        var pos = new Vector2((float)x / texture.width, (float)y / texture.height);
                        int mazeX = Mathf.Clamp(Mathf.RoundToInt(pos.x * width), 0, width-1);
                        int mazeY = Mathf.Clamp(Mathf.RoundToInt(pos.y * height), 0, height-1);
                        mazeY = height - 1 - mazeY;
                        Maze[mazeX, mazeY] = true;
                    }
                }
            }
        }

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
    }
}
