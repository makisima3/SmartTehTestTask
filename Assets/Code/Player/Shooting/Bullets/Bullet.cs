using Code.Pooling;
using UnityEngine;

namespace Code.Player.Shooting.Bullets
{
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected int damage;
        
        public abstract void Init(Transform spawnPoint, Vector3 rotation,ObjectPool<Bullet> pool);
        protected abstract void Move();
    }
}