using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Card firstCard, secondCard;
    private bool canFlip = true;

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
