using UnityEditor;
using UnityEngine;

namespace Maze.Editor
{
    public static class ColliderUtilityEditor
    {
        [MenuItem("Tools/Convert all to mesh collider")]
        public static void ConvertAllColliderToMeshCollider()
        {
            var allColliders = Object.FindObjectsByType<Collider>((FindObjectsSortMode)FindObjectsInactive.Include);
            foreach (var collider in allColliders)
            {
                if (collider is not MeshCollider)
                {
                    if (collider.gameObject.GetComponent<Renderer>())
                    {
                        var meshCollider = collider.gameObject.AddComponent<MeshCollider>();
                        Object.DestroyImmediate(collider);
                        EditorUtility.SetDirty(meshCollider);
                        EditorUtility.SetDirty(meshCollider.gameObject);
                    }
                    else
                    {
                        Debug.LogError($"No renderer component found on {collider.gameObject.name}");
                    }
     
                }
            }
        }
    }
}