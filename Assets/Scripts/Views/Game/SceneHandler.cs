using System.Linq;
using UnityEngine;

namespace Views.Game
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField] private Transform[] posesToSpawn;
        public Vector3[] Markers => posesToSpawn.Select(t => t.position).ToArray();
    }
}