using VContainer;

namespace Asteroids.DI
{
    public interface ISceneContext
    {
        public static ISceneContext CurrentContext;
        
        void Initialize(IObjectResolver container);
    }
}