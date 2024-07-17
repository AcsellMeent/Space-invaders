using Zenject;

public class InputServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInputService>().To<KeyboardInputService>().AsSingle();
    }
}
