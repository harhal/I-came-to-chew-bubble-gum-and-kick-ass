using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

namespace Core
{
    public static class GameState
    {
        public enum GameStage
        {
            Start,
            Decisionmaking,
            Input,
            PrePlayerActions,
            PlayerAction,
            PostPlayerActions,
            AreaCleared,
            Spawn,
            CharacterDead,
            HitProcessing
        }

        public static Random StableRandomGenerator = new Random(26122025);

        public static int Turn = 0;

        private const GameStage TurnIncreaseStage = GameStage.Spawn;

        public static GameStage CurrentGameStage { get; private set; } = GameStage.Start;

        private static Dictionary<GameStage, Queue<IGameStagePipelineItem>> _gameStagePipelines = 
            new Dictionary<GameStage, Queue<IGameStagePipelineItem>>();

        private static Queue<IGameStagePipelineItem> _runningPipeline = new Queue<IGameStagePipelineItem>();

        private static readonly Dictionary<GameStage, GameStage> GameStageTransitions = 
            new Dictionary<GameStage, GameStage>
            {
                { GameStage.Start, GameStage.Spawn },
                { GameStage.Spawn, GameStage.Decisionmaking },
                { GameStage.Decisionmaking, GameStage.Input },
                { GameStage.Input, GameStage.PrePlayerActions },
                { GameStage.PrePlayerActions, GameStage.PlayerAction },
                { GameStage.PlayerAction, GameStage.PostPlayerActions },
                { GameStage.PostPlayerActions, GameStage.HitProcessing },
                { GameStage.HitProcessing, GameStage.Spawn }
            };

        public static void Reset()
        {
            CurrentGameStage = GameStage.Start;
            _gameStagePipelines = new Dictionary<GameStage, Queue<IGameStagePipelineItem>>();
            StableRandomGenerator = new Random(26122025);
            Turn = 0;
        }

        public static void RegisterPipelineItem(IGameStagePipelineItem gameStagePipelineItem, GameStage stage)
        {
            if (!_gameStagePipelines.ContainsKey(stage))
            {
                _gameStagePipelines.Add(stage, new Queue<IGameStagePipelineItem>());
            }
            
            _gameStagePipelines[stage].Enqueue(gameStagePipelineItem);
        }

        public static void PipelineItemProcessed()
        {
            int loopBreaker = 0;
            while (_runningPipeline.Count == 0)
            {
                if (!GameStageTransitions.TryGetValue(CurrentGameStage, out var transition))
                {
                    return;
                }
            
                CurrentGameStage = transition;

                if (CurrentGameStage == TurnIncreaseStage)
                {
                    Turn++;
                }
                
                TransferStagePipelineToCurrent(CurrentGameStage);

                loopBreaker++;
                if (loopBreaker >= 10)
                {
                    break;
                }
            }

            if (_runningPipeline.Count > 0)
            {
                _runningPipeline.Dequeue().Trigger();
            }
        }

        public static void SetState(GameStage gameState)
        {
            CurrentGameStage = gameState;

            TransferStagePipelineToCurrent(CurrentGameStage);

            if (_runningPipeline is { Count: > 0 })
            {
                _runningPipeline.Dequeue().Trigger();
            }
        }

        private static void TransferStagePipelineToCurrent(GameStage stage)
        {
            // ReSharper disable once CanSimplifyDictionaryLookupWithTryGetValue
            if (!_gameStagePipelines.ContainsKey(stage))
            {
                return;
            }

            _runningPipeline = new Queue<IGameStagePipelineItem>(_gameStagePipelines[stage].Where(
                item => item != null && item.IsAlive()));
        }
    }
}