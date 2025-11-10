using Mvp.Presenter;
using VContainer.Unity;

namespace Maze.Entities.Labyrinth
{
    public interface ILabyrinthPresenter : IPresenter
    {
        
    }
    public class LabyrinthPresenter: Presenter<LabyrinthModel, LabyrinthView>, ILabyrinthPresenter, IInitializable
    {
        private readonly ILabyrinthGenerator _labyrinthGenerator;

        public LabyrinthPresenter(LabyrinthModel model, LabyrinthView view, ILabyrinthGenerator labyrinthGenerator) : base(model, view)
        {
            _labyrinthGenerator = labyrinthGenerator;
        }

        public void Initialize()
        {
            _labyrinthGenerator.GenerateMaze(
                Model.Width,
                Model.Height,
                Model.Complexity,
                Model.Density, 
                Model.ExitCount);
            View.DrawMaze(_labyrinthGenerator.Maze, _labyrinthGenerator.Exits);
        }
    }
}