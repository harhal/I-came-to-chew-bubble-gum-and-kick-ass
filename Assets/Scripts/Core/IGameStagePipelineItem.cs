using Enemies;

namespace Core
{
    public interface IGameStagePipelineItem
    {
        public void Trigger();

        public bool IsAlive();
    }
}