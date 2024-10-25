using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private SoundManager soundManager;
    private bool canFlip = false;
    private int turnsCount, matchCount;
    private int matchedCards = 0;
    private int totalCards = 0;

    [SerializeField] TextMeshProUGUI turnsText;
    [SerializeField] TextMeshProUGUI matchText;

    private void Start()
    {
        SetupGrid();
        soundManager = GetComponent<SoundManager>();
        //allCards = FindObjectsOfType<Card>();

    }

    void SetupGrid()
    {
        totalCards = rows * columns;
        if (totalCards % 2 != 0)
        {
            Debug.LogError("Total cards must be even!");
            return;
        }

        List<Sprite> cardPool = new List<Sprite>();

      
        for (int i = 0; i < totalCards / 2; i++)
        {
            Sprite selectedSprite = cardFrontSprites[i % cardFrontSprites.Length];
            cardPool.Add(selectedSprite);
            cardPool.Add(selectedSprite);  
        }

        Shuffle(cardPool);

        RectTransform rt = gridLayout.GetComponent<RectTransform>();
        float width = rt.rect.width / columns;
        float height = rt.rect.height / rows;
        gridLayout.cellSize = new Vector2(Mathf.Min(width, height), Mathf.Min(width, height));

        for (int i = 0; i < totalCards; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, gridLayout.transform);
            Card cardScript = newCard.GetComponent<Card>();
            cardScript.cardFront = cardPool[i];
            cardScript.cardBack = cardBackSprite;
        }

        allCards = FindObjectsOfType<Card>();

        StartCoroutine(RevealAllCards());
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
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
        soundManager.PlayFlipSound();
        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(CheckCards());
        }

        if (matchedCards >= totalCards)
        {
            soundManager.PlayGameOverSound();
            Debug.Log("Game Over! All cards matched.");
        }
    }

    private IEnumerator CheckCards()
    {
        canFlip = false;
        yield return new WaitForSeconds(1);

        if (firstCard.cardFront.name == secondCard.cardFront.name)
        {
            matchedCards += 2;
            soundManager.PlayMatchSound();
            firstCard.HideCard();
            secondCard.HideCard();
            matchCount += 1;
            matchText.text = "Matches: " + matchCount.ToString();
        }
        else
        {
            soundManager.PlayMismatchSound();
            turnsCount += 1;
            turnsText.text = "Turns: " + turnsCount.ToString();
            firstCard.FlipCard();
            secondCard.FlipCard();
        }

        firstCard = secondCard = null;
        canFlip = true;

        yield return new WaitForSeconds(0.25f);
        if (matchedCards >= totalCards)
        {
            soundManager.PlayGameOverSound();
            Debug.Log("Game Over! All cards matched.");
        }

        Debug.Log(matchedCards + " " + totalCards);
    }
}
