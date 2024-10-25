using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Card[] allCards;
    private Card firstCard, secondCard;
    private bool canFlip = false;

    private void Start()
    {
        allCards = FindObjectsOfType<Card>();
        StartCoroutine(RevealAllCards());
    }

    private IEnumerator RevealAllCards()
    {
        foreach (var card in allCards) card.FlipCard();
        yield return new WaitForSeconds(2);
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
