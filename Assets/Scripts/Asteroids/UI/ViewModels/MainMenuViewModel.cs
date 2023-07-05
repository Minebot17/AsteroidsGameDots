using Asteroids.DI;
using Asteroids.DI.Installers;
using Asteroids.Utils.Extension_Methods.RX;
using UniRx;
using UnityEngine;

namespace Asteroids.UI.ViewModels
{
    public class MainMenuViewModel : DisposableObject
    {
        public ReactiveCommand PlayCommand { get; } = new();
        public ReactiveCommand ExitCommand { get; } = new();

        private readonly ISceneLoader _sceneLoader;
        
        public MainMenuViewModel(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            
            PlayCommand.Subscribe(OnPlay).AddTo(this);
            ExitCommand.Subscribe(OnExit).AddTo(this);
        }

        private void OnPlay(Unit _)
        {
            _sceneLoader.LoadScene(Scene.Game, new GameInstaller());
        }

        private void OnExit(Unit _)
        {
            Application.Quit();
        }
    }
}