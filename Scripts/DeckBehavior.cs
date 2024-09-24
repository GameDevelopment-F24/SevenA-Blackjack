using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeckBehavior : MonoBehaviour
{
    //use 53 instead of 52 so that we can mod card values, 0th object is irrelevant
    public Sprite[] cardSprites;
    public int[] cardValues = new int[53];
    int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetCardValues();
    }

    // Update is called once per frame
    void GetCardValues()
    {
        int val;
        for (int i = 0; i < cardSprites.Length; i++) {
            val = i;
            val %= 13;
            if (val > 10 || val == 0) {
                val = 10;
            }
            cardValues[i] = val++;
        }
    }
    public void Shuffle() {
        for(int i = cardSprites.Length - 1; i > 1; --i) {
            int tmp = Mathf.FloorToInt(UnityEngine.Random.Range(0.0f, 1.0f) * cardSprites.Length - 1) + 1;

            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[tmp];
            cardSprites[tmp] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[tmp];
            cardValues[tmp] = value;
        }
        currentIndex = 1;

    }
    public int DealCard(CardScript cs) {
        if (currentIndex >= cardSprites.Length) {
            currentIndex = 1;
        }

        cs.SetSprite(cardSprites[currentIndex]);
        cs.SetValue(cardValues[currentIndex]);
        currentIndex++;
        return cs.GetValue();
    }
    public Sprite GetCardBack() {
        return cardSprites[1];
    }
}
