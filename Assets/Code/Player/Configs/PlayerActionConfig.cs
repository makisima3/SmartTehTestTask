using UnityEngine;

namespace Code.Player.Configs
{
    [CreateAssetMenu(fileName = "PlayerActionConfig", menuName = "ScriptableObjects/Player/PlayerActionConfig", order = 0)]
    public class PlayerActionConfig : ScriptableObject
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float inertia = 0.1f;
        [SerializeField] private float shootRate = 1;

        public float Speed => speed;

        public float Inertia => inertia;

        public float ShootRate => shootRate;
    }
}