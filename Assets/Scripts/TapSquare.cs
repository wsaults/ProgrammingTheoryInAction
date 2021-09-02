using UnityEngine;

public class TapSquare : Square // INHERITANCE
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // POLYMORPHISM
    protected override void SquareTapped()
    {
        gameManager.TappedGameSquare(gameObject);
    }
}
