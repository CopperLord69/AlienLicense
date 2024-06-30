using UnityEngine;
using Zenject;

public abstract class SystemInstaller<T> : MonoInstaller
{
    [SerializeField]
    protected T _system;

    public override void InstallBindings()
    {
        Container.Bind<T>().FromInstance(_system).AsSingle().NonLazy();
    }
}