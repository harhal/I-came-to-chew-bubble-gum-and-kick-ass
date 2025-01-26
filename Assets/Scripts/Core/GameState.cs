using System.Collections.Generic;

namespace Core
{
    public static class GameState
    {
        public enum GameStage
        {
            Start,
            Input,
            PrePlayerActions,
            PlayerAction,
            PostPlayerActions,
            AreaCleared,
            OutOfBubbleGum,
            CharacterDead
        }

        public static GameStage CurrentGameStage { get; private set; } = GameStage.Start;

        private static Dictionary<GameStage, Queue<IGameStagePipelineItem>> _gameStagePipelines = 
            new Dictionary<GameStage, Queue<IGameStagePipelineItem>>();

        private static Queue<IGameStagePipelineItem> _runningPipeline = new Queue<IGameStagePipelineItem>();

        private static readonly Dictionary<GameStage, GameStage> GameStageTransitions = 
            new Dictionary<GameStage, GameStage>
            {
                { GameStage.Start, GameStage.Input },
                { GameStage.Input, GameStage.PrePlayerActions },
                { GameStage.PrePlayerActions, GameStage.PlayerAction },
                { GameStage.PlayerAction, GameStage.PostPlayerActions },
                { GameStage.PostPlayerActions, GameStage.Input }
            };

        public static void Reset()
        {
            CurrentGameStage = GameStage.Start;
            _gameStagePipelines = new Dictionary<GameStage, Queue<IGameStagePipelineItem>>();
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
                
                // ReSharper disable once CanSimplifyDictionaryLookupWithTryGetValue
                if (_gameStagePipelines.ContainsKey(CurrentGameStage))
                {
                    _runningPipeline = new Queue<IGameStagePipelineItem>(_gameStagePipelines[CurrentGameStage]);
                }

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
            
            // ReSharper disable once CanSimplifyDictionaryLookupWithTryGetValue
            if (_gameStagePipelines.ContainsKey(CurrentGameStage))
            {
                _runningPipeline = new Queue<IGameStagePipelineItem>(_gameStagePipelines[CurrentGameStage]);
            }

            if (_runningPipeline is { Count: > 0 })
            {
                _runningPipeline.Dequeue().Trigger();
            }
        }
    }
}