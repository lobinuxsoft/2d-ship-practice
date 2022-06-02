using UnityEngine;

public class LookToMouse : MonoBehaviour
{
    Vector3 screenPoint = Vector3.zero;

    // Update is called once per frame
    void Update() 
    {
        screenPoint = Input.mousePosition;
        screenPoint.z = -Camera.main.transform.position.z;

        transform.up = (Camera.main.ScreenToWorldPoint(screenPoint) - transform.position).normalized;

        transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(screenPoint), .25f);
    }
#endif
}
