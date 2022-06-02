using UnityEngine;

public class ChangeBackgroundColor : MonoBehaviour
{
    [SerializeField] Color backgroundColor = Color.black;

    public void UpdateColor() => Camera.main.backgroundColor = backgroundColor;
}
