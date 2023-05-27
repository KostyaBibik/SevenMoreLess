using System.Collections;
using Enums;

namespace Infrastructure.StatesStructure
{
    public abstract class BaseState
    {
        public EGameState gameState;
        
        protected GameInstance.GameInstance context;

        public BaseState(
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