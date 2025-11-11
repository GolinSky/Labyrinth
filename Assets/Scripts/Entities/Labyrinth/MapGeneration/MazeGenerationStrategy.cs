namespace Maze.Entities.Labyrinth
{
    public class MazeGenerationStrategy:ILabyrinthGenerator
    {
        private readonly ILabyrinthGenerator _labyrinthGenerator;

        public MazeGenerationStrategy(IGenerationContext generationContext)
        {
            switch (generationContext.GenerationStrategyType)
            {
                case MazeGenerationStrategyType.Kruskal:
                    _labyrinthGenerator = new LabyrinthGenerator();
                    break;
                case MazeGenerationStrategyType.Prim:
                    _labyrinthGenerator = new PrimLabyrinthGenerator();
                    break;
            
            }
        }
        
        public GeneratedLabyrinthData GenerateMaze(int width, int height, float complexity, float density, int exitCount)
        {
            return _labyrinthGenerator.GenerateMaze(width, height, complexity, density, exitCount);
        }
    }
}