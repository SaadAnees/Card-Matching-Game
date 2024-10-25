using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Sprite cardFront;
    public Sprite cardBack;
    private Image cardImage;
    private bool isFlipped = false;
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        cardImage = GetComponent<Image>();
        cardImage.sprite = cardBack;
    }

    public void OnCardClick()
    {
        if (!isFlipped) FlipCard();
    }

    public void FlipCard()
    {
        isFlipped = !isFlipped;

        if (isFlipped)
            cardImage.sprite = cardFront;
        else
            cardImage.sprite = cardBack;
        gameController.CheckMatch(this);
    }
}
