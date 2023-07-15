using VContainer;

namespace Asteroids.DI
{
    public interface ISceneContext
    {
        public static ISceneContext CurrentContext { get; set; }
        public static IObjectResolver Container { get; set; }

        void ManualInitialize(IObjectResolver container);
    }
}