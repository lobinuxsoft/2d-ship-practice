using UnityEngine;
using UnityEngine.Events;

public class ScreenLimit : MonoBehaviour
{
    Renderer rendererComponent;

    public UnityEvent<Vector3> onScreenLimit;

    private void Awake()
    {
        rendererComponent = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (OutOfScreen(out Vector3 pos)) onScreenLimit?.Invoke(pos);
    }

    bool OutOfScreen(out Vector3 pos)
    {
        pos = Vector3.zero;

        Vector3 pointMax = Camera.main.WorldToViewportPoint(rendererComponent.bounds.max);
        Vector3 pointMin = Camera.main.WorldToViewportPoint(rendererComponent.bounds.min);
        Vector3 lastPos = Camera.main.WorldToViewportPoint(transform.position);

        bool right = pointMax.x > 1;
        bool up = pointMax.y > 1;
        bool left = pointMin.x < 0;
        bool down = pointMin.y < 0;

        bool rightUp = right || up;
        bool leftDown = left || down;

        if (rightUp)
        {
            pos = Camera.main.ViewportToWorldPoint(new Vector3(right ? .9f : pointMax.x, up ? .9f : pointMax.y, 0));
        }
        else if (leftDown)
        {
            pos = Camera.main.ViewportToWorldPoint(new Vector3(left ? .1f : pointMin.x, down ? .1f : pointMin.y, 0));
        }

        return rightUp || leftDown;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!rendererComponent) rendererComponent = GetComponent<Renderer>();

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(rendererComponent.bounds.min, .1f);
        Gizmos.DrawSphere(rendererComponent.bounds.max, .1f);
    }
#endif
}
