using UnityEngine;

abstract public class Square : MonoBehaviour
{
    protected abstract void SquareTapped();

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name);
        SquareTapped();
    }
}
