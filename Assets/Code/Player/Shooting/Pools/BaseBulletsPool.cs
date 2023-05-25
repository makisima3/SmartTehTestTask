using Code.Player.Shooting.Bullets;
using Code.Player.Shooting.Enums;
using Code.Pooling;
using UnityEngine;
using Zenject;

namespace Code.Player.Shooting.Pools
{
    public class BaseBulletsPool : ObjectPool<Bullet>
    {

        public BaseBulletsPool(Bullet prefab, int initialSize, DiContainer container, Transform holder, BulletType type) : base(prefab, initialSize, container, holder, type)
        {
        }
    }
}