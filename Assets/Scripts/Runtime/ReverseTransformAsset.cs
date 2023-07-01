using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Rewind
{
    public class ReverseTransformAsset : ScriptableObject
    {
        const string STORE_PATH = "Assets/Resources/TransformTimes/";


        [SerializeField]
        public TimedTransform[] timedTransform;


        public static void StoreToAsset(Stack<TimedTransform> stack, string name)
        {
#if UNITY_EDITOR
            var asset = ScriptableObject.CreateInstance<ReverseTransformAsset>();
            asset.timedTransform = stack.ToArray();
            AssetDatabase.CreateAsset(asset, $"{STORE_PATH}{name}.asset");
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
#endif
        }
    }
}
