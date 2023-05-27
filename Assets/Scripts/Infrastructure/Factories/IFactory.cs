using Enums;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IFactory
    {
        void CreateDice(EDiceType type, Vector3 pos);
    }
}