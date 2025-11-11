using Maze.Entities.Labyrinth;
using Maze.Services.Game;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace Maze.Ui.MazeSetUp
{
    public class MazeSetUpUi: BaseUi<ICoreGamePresenter>, IInitializable, ILabyrinthContext
    {
        //todo: store constrains in maze data instead 
        private const float MIN_WIDTH = 3;
        private const float MAX_WIDTH = 50;
        private const float MIN_HEIGHT = 3;
        private const float MAX_HEIGHT = 50;
        private const float MIN_EXIT_COUNT = 1;
        private const float MAX_EXIT_COUNT = 5;
        private const float MIN_COMPLEXITY = 0;
        private const float MAX_COMPLEXITY = 1;
        private const float MIN_DENSITY = 0;
        private const float MAX_DENSITY = 1;

        [SerializeField] protected SliderFieldUi widthSlider;
        [SerializeField] protected SliderFieldUi heightSlider;
        [SerializeField] protected SliderFieldUi exitCountSlider;
        [SerializeField] protected SliderFieldUi complexitySlider;
        [SerializeField] protected SliderFieldUi densitySlider;

        [SerializeField] protected Button startButton;

        public int Width => widthSlider.ValueInt;
        public int Height => heightSlider.ValueInt;
        public int ExitCount => exitCountSlider.ValueInt;
        public float Complexity => complexitySlider.Value;
        public float Density => densitySlider.Value;
        

        public void Initialize()
        {
            widthSlider.SetLimits(MIN_WIDTH, MAX_WIDTH);
            heightSlider.SetLimits(MIN_HEIGHT, MAX_HEIGHT);
            exitCountSlider.SetLimits(MIN_EXIT_COUNT, MAX_EXIT_COUNT);
            complexitySlider.SetLimits(MIN_COMPLEXITY, MAX_COMPLEXITY);
            densitySlider.SetLimits(MIN_DENSITY, MAX_DENSITY);
            
            startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            if (Presenter == null)
            {
                Debug.LogError($"Presenter is null in {nameof(MazeSetUpUi)}");
            }
            else
            {
                Presenter.StartGame(this);
            }
        }
    }
}