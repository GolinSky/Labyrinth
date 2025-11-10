using System.Collections.Generic;
using Fps.MVP.Model;
using UnityEngine;

namespace Maze.Services.Scenes
{
    public class SceneModel: Model
    {
        private Dictionary<SceneType, SceneReference> SceneDictionary { get; }
        
        public SceneModel(SceneData sceneData)
        {
            SceneDictionary = sceneData.Scenes.Dictionary;
        }

        public SceneReference GetScene(SceneType sceneType)
        {
            if (SceneDictionary.TryGetValue(sceneType, out var scene))
            {
                return scene;
            }

            Debug.Log($"No scene found with type {sceneType}");
            return null;
        }
    }
}