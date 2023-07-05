using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Asteroids.DI
{
    public interface ISceneLoader
    {
        UniTask LoadScene(Scene scene, IInstaller sceneInstaller);
        void LoadSceneWithoutDependencies(Scene scene);
    }

    public enum Scene
    {
        MainMenu,
        Game
    }
}