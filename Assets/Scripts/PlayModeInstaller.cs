using UnityEngine;
using Zenject;

public class PlayModeInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var playerInput = new PlayerInput();
        playerInput.Enable();

        Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle();
    }
}