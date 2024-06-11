using PlanetMerge.Entities.Splash;
using UnityEngine;

namespace PlanetMerge.Pools
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