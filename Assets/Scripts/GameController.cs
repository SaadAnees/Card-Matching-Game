using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject cardPrefab;
    public GridLayoutGroup gridLayout;
    public Sprite[] cardFrontSprites;
    public Sprite cardBackSprite;

    public int rows = 2;
    public int columns = 3;


    public Card[] allCards;
    private Card firstCard, secondCard;
    private bool canFlip = false;

    private void Start()
    {
        SetupGrid();
        //allCards = FindObjectsOfType<Card>();
       
    }

    void SetupGrid()
    {
        RectTransform rt = gridLayout.GetComponent<RectTransform>();
        float width = rt.rect.width / columns;
        float height = rt.rect.height / rows;
        gridLayout.cellSize = new Vector2(Mathf.Min(width, height), Mathf.Min(width, height));

        for (int i = 0; i < rows * columns; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, gridLayout.transform);
            Card card = newCard.GetComponent<Card>();

            card.cardFront = cardFrontSprites[i / 2]; 
            card.cardBack = cardBackSprite;

        }

        allCards = FindObjectsOfType<Card>();

        StartCoroutine(RevealAllCards());
    }

    private IEnumerator RevealAllCards()
    {
        yield return new WaitForSeconds(0.9f);
        foreach (var card in allCards) card.FlipCard();
        yield return new WaitForSeconds(1f);
        foreach (var card in allCards) card.FlipCard();
        canFlip = true;
    }

    public void CheckMatch(Card card)
    {
        if (!canFlip) return;

        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(CheckCards());
        }
    }

    private IEnumerator CheckCards()
    {
        canFlip = false;
        yield return new WaitForSeconds(1);

        if (firstCard.cardFront.name == secondCard.cardFront.name)
        {
            Destroy(firstCard.gameObject);
            Destroy(secondCard.gameObject);
        }
        else
        {
            firstCard.FlipCard();
            secondCard.FlipCard();
        }

        firstCard = secondCard = null;
        canFlip = true;
    }
}
