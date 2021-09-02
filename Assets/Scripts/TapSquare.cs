using UnityEngine;

public class TapSquare : MonoBehaviour // INHERITANCE
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name);
        gameManager.TappedGameSquare(gameObject);
    }
}
