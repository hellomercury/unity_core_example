using System;
using UniRx;
using Zenject;

namespace Core.Services
{
    public class OnGameStartedSignal { }
      
    /// <summary>
    /// Entry point for the game.
    /// </summary>
    public class CoreFrameworkInstaller : MonoInstaller<CoreFrameworkInstaller>
    {
        [Inject]
        private SignalBus _signalBus;
        
        private readonly Subject<Unit> _onGameStart = new Subject<Unit>();
        private IObservable<Unit> OnGameStarted => _onGameStart;

        public override void InstallBindings()
        {
            Container.BindInstance(OnGameStarted).AsSingle();
        }

        public override void Start()
        {
            _signalBus.Fire<OnGameStartedSignal>();
        }
    }
}