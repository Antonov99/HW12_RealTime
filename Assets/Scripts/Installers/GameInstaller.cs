using Chest;
using DefaultNamespace;
using SaveSystem;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ServerTimeManager>().AsSingle().NonLazy();
        Container.Bind<ChestLoader>().AsSingle().NonLazy();
        
        Container.BindInterfacesTo<TimeSaveLoader>().AsSingle().NonLazy();
        Container.BindInterfacesTo<GameRepository>().AsSingle().NonLazy();
    }
}