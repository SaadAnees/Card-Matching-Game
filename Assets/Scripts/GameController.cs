using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Card firstCard;
    private Card secondCard;
    private bool canFlip = true;

    public void CheckMatch(Card card)
    {
        Debug.Log(card);
        if (!canFlip) return;

        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
        }

        if (firstCard.cardFront.name == secondCard.cardFront.name)
        {
            Debug.Log("Its a match!");
        }
    }

  
}
