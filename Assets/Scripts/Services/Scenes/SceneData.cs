using Mvp.Model;
using UnityEngine;
using Utilities.ScriptUtils.EditorSerialization;

namespace Maze.Services.Scenes
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "Data/SceneData")]
    public class SceneData : Data
    {
        [field: SerializeField] public DictionaryWrapper<SceneType, SceneReference> Scenes { get; private set; }
    }
}