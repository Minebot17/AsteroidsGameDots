using Asteroids.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.ECS.Bakers
{
    public class ScoreAuthoring : MonoBehaviour
    {
        public class ScoreBaker : Baker<ScoreAuthoring>
        {
            public override void Bake(ScoreAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent<ScoreData>(entity);
            }
        }
    }
}