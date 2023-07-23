using System;
using Cysharp.Threading.Tasks;
using Unity.Entities;

namespace Asteroids.Utils.Extension_Methods
{
    public static class UniTaskExtensions
    {
        public static async UniTaskVoid SwitchToMainThread(this Action callback)
        {
            await UniTask.SwitchToMainThread();
            callback();
        }

        public static async UniTask WaitUntilWorldInited()
        {
            await UniTask.WaitUntil(() =>
                World.DefaultGameObjectInjectionWorld != null && World.DefaultGameObjectInjectionWorld.IsCreated);
        }

        public static async UniTask<Entity> WaitUntilSingletonEntityCreated(this EntityQuery entityQuery)
        {
            await UniTask.WaitUntil(() => entityQuery.CalculateEntityCount() != 0);
            return entityQuery.GetSingletonEntity();
        }
        
        public static async UniTask<T> WaitUntilSingletonCreated<T>(this EntityQuery entityQuery) where T : unmanaged, IComponentData
        {
            await UniTask.WaitUntil(() => entityQuery.CalculateEntityCount() != 0);
            return entityQuery.GetSingleton<T>();
        }
    }
}