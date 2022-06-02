using UnityEditor;
using UnityEngine;

namespace CryingOnionTools.GravitySystem
{
   [RequireComponent(typeof(Rigidbody2D))]
   public class CustomGravityRigidbody2D : MonoBehaviour
   {
      private Rigidbody2D body;
      private float floatDelay;

      private void Awake()
      {
         body = GetComponent<Rigidbody2D>();
         body.gravityScale = 0;
      }

      private void FixedUpdate()
      {
         if (body.IsSleeping())
         {
            floatDelay = 0;
            return;
         }

         if (body.velocity.sqrMagnitude < 0.01f)
         {
            floatDelay += Time.fixedDeltaTime;
            if(floatDelay >= .5f) return;
         }
         else
         {
            floatDelay = 0;
         }
         
         body.AddForce(CustomGravity.GetGravity(body.position));
      }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.yellow;
            Handles.ArrowHandleCap(0, transform.position, Quaternion.LookRotation(-CustomGravity.GetUpAxis(transform.position), Vector3.up), 1f, EventType.Repaint);

            if (body)
            {
                Handles.color = Color.blue;
                Vector3 dir = Vector3.ClampMagnitude(body.velocity, 1);
                Handles.ArrowHandleCap(0, body.position, Quaternion.LookRotation(dir, Vector3.up), dir.magnitude, EventType.Repaint);
            }
        }
#endif
    }
}
