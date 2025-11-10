using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;

namespace Maze.Editor
{
#if UNITY_EDITOR
    [InitializeOnLoad]
    public class PlayFromSceneToolbar
    {
        static PlayFromSceneToolbar()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            if (EditorApplication.isPlaying) return;

            if (GUILayout.Button(new GUIContent("▶️ PLAY", "Play from MAIN MENU"), GUILayout.Width(80)))
            {
                PlayFromScene(SceneManager.GetSceneAt(0).path);
            }
        }

        private static void PlayFromScene(string scenePath)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scenePath);
                EditorApplication.EnterPlaymode();
            }
        }
    }
#endif

}