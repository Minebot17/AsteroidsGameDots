using Asteroids.Utils.Extension_Methods;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Asteroids.DI
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadScene(Scene scene, IInstaller sceneInstaller)
        {
            IObjectResolver container = null;
            await SceneManager.LoadSceneAsync("Loading");
            await UniTask.RunOnThreadPool(() =>
            {
                container = sceneInstaller.BuildContainer(ISceneContext.CurrentContext);
            });
            await UniTask.SwitchToMainThread();
            ISceneContext.Container = container;
            await SceneManager.LoadSceneAsync(scene.ToString("G"), LoadSceneMode.Additive);
            await SceneManager.UnloadSceneAsync("Loading");
        }

        public void LoadSceneWithoutDependencies(Scene scene)
        {
            ISceneContext.Container = null;
            SceneManager.LoadScene(scene.ToString("G"));
        }
    }
}