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
   }
}
