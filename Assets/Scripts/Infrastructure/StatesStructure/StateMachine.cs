using System;
using System.Collections;
using System.Collections.Generic;
using Enums;

namespace Infrastructure.StatesStructure
{
    public class StateMachine
    {
        private readonly List<BaseState> states = new List<BaseState>();
        
        private BaseState _currentBaseState;

        public StateMachine(params BaseState[] states)
        {
            for (var i = 0; i < states.Length; i++)
            {
                this.states.Add(states[i]);    
            }
        }
        
        public IEnumerator ChangeState(EGameState gameState)
        {
            if (_currentBaseState != null)
                yield return _currentBaseState.Exit();

            _currentBaseState = GetState(gameState);
            
            if (_currentBaseState != null)
                yield return _currentBaseState.Enter();
        }

        private BaseState GetState(EGameState gameState)
        {
            for (var i = 0; i < states.Count; i++)
            {
                if (states[i].gameState == gameState)
                    return states[i];
            }

            throw new NullReferenceException($"StateMachine doesn't have a {gameState} state");
        }
    }
}