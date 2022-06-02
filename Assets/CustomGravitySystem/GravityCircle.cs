#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace CryingOnionTools.GravitySystem
{
    public class GravityCircle : GravitySource
    {
        [SerializeField] float gravity = 9.81f;

        [SerializeField, Min(0f)] private float outerRadius = 22f, outerFalloffRadius = 22f;
        
        float outerFalloffFactor;

        void Awake() => OnValidate();

        void OnValidate ()
        {
            outerFalloffRadius = Mathf.Max(outerFalloffRadius, outerRadius);
            
            outerFalloffFactor = 1f / (outerFalloffRadius - outerRadius);
        }

        public override Vector3 GetGravity (Vector3 position)
        {
            Vector3 vector = transform.position - position;
            float distance = vector.magnitude;
            
            if (distance > outerFalloffRadius) return Vector3.zero;

            float g = gravity / distance;

            if (distance > outerRadius)
            {
                g *= 1f - (distance - outerRadius) * outerFalloffFactor;
            }
            
            return g * vector;
        }
        
#if UNITY_EDITOR
        void OnDrawGizmos () 
        {
            Vector3 p = transform.position;
            
            
            Handles.color = Color.yellow;
            
            Handles.DrawWireDisc(p, Vector3.back, outerRadius);
            Handles.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, .15f); 
            Handles.DrawSolidDisc(p, Vector3.back, outerRadius);
            
            if (outerFalloffRadius > outerRadius)
            {
                Handles.color = Color.cyan;
                Handles.DrawWireDisc(p, Vector3.back, outerFalloffRadius);
                Handles.color = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, .15f);
                Handles.DrawSolidDisc(p, Vector3.back, outerFalloffRadius);
            }
        }
#endif
    }
}
