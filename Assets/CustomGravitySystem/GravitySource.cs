using UnityEngine;

namespace CryingOnionTools.GravitySystem
{
    public abstract class GravitySource : MonoBehaviour
    {
        private void OnEnable() => CustomGravity.Register(this);

        private void OnDisable() => CustomGravity.Unregister(this);

        public abstract Vector3 GetGravity(Vector3 position);
    }
}
