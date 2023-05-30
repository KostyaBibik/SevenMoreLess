using System.Collections;
using Enums;

namespace Infrastructure.StatesStructure
{
    public abstract class BaseState
    {
        public readonly EGameState gameState;
        
        protected readonly GameInstance.GameInstance context;

        protected BaseState(
            GameInstance.GameInstance gameInstance,
            EGameState gameState
        )
        {
            context = gameInstance;
            this.gameState = gameState;
        }
        
        public virtual IEnumerator Enter()
        {
            yield break;
        }
        
        public virtual IEnumerator Exit()
        {
            yield break;
        }
    }
}