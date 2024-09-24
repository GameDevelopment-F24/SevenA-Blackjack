using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    //this is for player/dealer
    public CardScript cs;
    public DeckBehavior db;
    public int handVal = 0;
    public int money;
    public GameObject[] hand;
    public int cardIndex = 0;
    List<CardScript> aceList = new List<CardScript>();
    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    public int GetCard() {
        //get card, use to deal to assign sprite/val and place on table
        int cardValue = db.DealCard(hand[cardIndex].GetComponent<CardScript>());
        if (hand[cardIndex].GetComponent<CardScript>().GetSpriteName() == "card_dummy") {
            Debug.Log("blank drawn, skipping!");
            return GetCard();
        }
        //actually display card
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        // Add card value to running total of the hand
        handVal += cardValue;
        // If value is 1, it is an ace
        if(cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }
        // Cehck if we should use an 11 instead of a 1
        AceCheck();
        cardIndex++;
        return handVal;
    }

    // Search for needed ace conversions, 1 to 11 or vice versa
    public void AceCheck()
    {
        // for each ace in the lsit check
        foreach (CardScript ace in aceList)
        {
            if(handVal + 10 < 22 && ace.GetValue() == 1)
            {
                // if converting, adjust card object value and hand
                ace.SetValue(11);
                handVal += 10;
            } else if (handVal > 21 && ace.GetValue() == 11)
            {
                // if converting, adjust gameobject value and hand value
                ace.SetValue(1);
                handVal -= 10;
            }
        }
    }

    // Add or subtract from money, for bets
    public void AdjustMoney(int amount)
    {
        money += amount;
    }

    // Output players current money amount
    public int GetMoney()
    {
        return money;
    }

    // Hides all cards, resets the needed variables
    public void ResetHand()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handVal = 0;
        aceList = new List<CardScript>();
    }
}
