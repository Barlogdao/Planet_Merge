using PlanetMerge.Planets;
using UnityEngine;

namespace PlanetMerge.Services.Pools
{
    public class CollisionSplashPool : EntityPool<CollisionSplash>, IReleasePool<CollisionSplash>
    {
        public CollisionSplashPool(CollisionSplash prefab, Transform parent) : base(prefab, parent) { }

        protected override void OnCreateAction(CollisionSplash entity)
        {
            base.OnCreateAction(entity);

            entity.Initialize(this);
        }
    }
}