using System;
using Cysharp.Threading.Tasks;

namespace Asteroids.Utils.Extension_Methods
{
    public static class UniTaskExtensions
    {
        public static async UniTaskVoid SwitchToMainThread(this Action callback)
        {
            await UniTask.SwitchToMainThread();
            callback();
        }
    }
}