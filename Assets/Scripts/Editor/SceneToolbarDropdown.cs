using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace Maze.Editor
{
    [InitializeOnLoad]
    public static class SceneToolbarDropdown
    {
        static string[] scenePaths;
        static string[] sceneNames;
        static int selectedSceneIndex = 0;

        static SceneToolbarDropdown()
        {
            LoadAllScenes();
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        private static void LoadAllScenes()
        {
            string[] folders = new[] { "Assets/Scenes" }; // <-- limit search to this folder

            scenePaths = AssetDatabase.FindAssets("t:Scene", folders)
                .Select(AssetDatabase.GUIDToAssetPath)
                .ToArray();

            sceneNames = scenePaths
                .Select(System.IO.Path.GetFileNameWithoutExtension)
                .ToArray();
        }

        private static void OnToolbarGUI()
        {
            if (sceneNames == null || sceneNames.Length == 0)
            {
                GUILayout.Label("No scenes", EditorStyles.boldLabel);
                return;
            }

            GUILayout.Space(10);

            if (GUILayout.Button(sceneNames[selectedSceneIndex], EditorStyles.popup, GUILayout.Width(150)))
            {
                GenericMenu menu = new GenericMenu();

                for (int i = 0; i < sceneNames.Length; i++)
                {
                    int index = i;
                    string sceneName = sceneNames[i];
                    menu.AddItem(new GUIContent(sceneName), false, () =>
                    {
                        // Always ask to save, even if same scene
                        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        {
                            EditorSceneManager.OpenScene(scenePaths[index]);
                            selectedSceneIndex = index;
                        }
                    });
                }

                menu.DropDown(new Rect(Event.current.mousePosition, Vector2.zero));
            }
        }



    }
}