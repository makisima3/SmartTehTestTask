using Code.Enemies;
using Code.Player.Configs;
using Code.Player.Shooting;
using UnityEngine;
using Zenject;

namespace Code.Player.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private Joystick joystick;
        
        //configs
        [SerializeField] private PlayerActionConfig playerActionConfig;
        [SerializeField] private ShootingConfig shootingConfig;
        [SerializeField] private EnemiesConfig enemiesConfig;
        [SerializeField] private BulletCollectablesConfig bulletCollectablesConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<EnemySpawner>().FromInstance(enemySpawner).AsSingle().NonLazy();
            Container.Bind<Joystick>().FromInstance(joystick).AsSingle().NonLazy();
            
            //configs
            Container.Bind<PlayerActionConfig>().FromInstance(playerActionConfig).AsSingle().NonLazy();
            Container.Bind<ShootingConfig>().FromInstance(shootingConfig).AsSingle().NonLazy();
            Container.Bind<EnemiesConfig>().FromInstance(enemiesConfig).AsSingle().NonLazy();
            Container.Bind<BulletCollectablesConfig>().FromInstance(bulletCollectablesConfig).AsSingle().NonLazy();
        }
    }
}