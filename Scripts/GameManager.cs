using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //buttons
    public Button HitButton;
    public Button StandButton;
    public Button BetButton;
    public Button DealButton;
    public Button AllInButton;
    //game objects
    public DeckBehavior deck;
    public HandScript player;
    public HandScript dealer;
    public GameObject hideCard;
    public Animator animator;
    //method variables
    int standClicks = 0;
    int playerCardCount = 2;
    int pot = 0;
    //text
    public TextMeshProUGUI dealerText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI betText;
    public TextMeshProUGUI handText;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI standText;
    public TextMeshProUGUI roundText;

    
    // Start is called before the first frame update
    void Start()
    {
        //prep buttons
        HitButton.onClick.AddListener(() => HitClicked());
        StandButton.onClick.AddListener(() => StandClicked());
        BetButton.onClick.AddListener(() => BetClicked());
        DealButton.onClick.AddListener(() => DealClicked());
        AllInButton.onClick.AddListener(() => AllInClicked());
        HitButton.gameObject.SetActive(false);
        StandButton.gameObject.SetActive(false);
        BetButton.gameObject.SetActive(false);
        roundText.gameObject.SetActive(false);
        hideCard.gameObject.SetActive(false);
        AllInButton.gameObject.SetActive(false);
        //text and money
        pot = 40;
        betText.text = "Bet: " + pot.ToString();
        cashText.text = "Cash: " + player.GetMoney().ToString();
        
        

    }
    private void DealClicked() {
        DealButton.gameObject.SetActive(false);
        roundText.gameObject.SetActive(false);
        hideCard.gameObject.SetActive(true);
        //set up cards
        player.ResetHand();
        dealer.ResetHand();
        deck.Shuffle();
        player.StartHand();
        dealer.StartHand();
        hideCard.GetComponent<Renderer>().enabled = true;
        //set up text
        player.money = player.money - 20;
        standText.text = "STAND";
        handText.text = "Hand: " + player.handVal.ToString();
        dealerText.text = "Dealer: " + (dealer.handVal - dealer.hand[0].GetComponent<CardScript>().value) + " + ?";
        betText.text = "Bet: " + pot.ToString();
        cashText.text = "Cash: " + player.GetMoney().ToString();
        //enable buttons
        HitButton.gameObject.SetActive(true);
        StandButton.gameObject.SetActive(true);
        BetButton.gameObject.SetActive(true);
        AllInButton.gameObject.SetActive(true);
    }

    private void HitClicked()
    {
        player.GetCard();
        playerCardCount++;
        if (player.handVal > 20) {RoundOver();}
        if (playerCardCount >= 7) {
            HitButton.interactable = false;
            hitText.text = "HAND FULL";
        }   
        handText.text = "Hand: " + player.handVal.ToString();
            
        
    }
    private void StandClicked()
    {
        standClicks++;
        if (standClicks == 1) {
            standText.text = "CALL";
            HitDealer();
        }
        else if (standClicks > 1) {
            RoundOver();
        }
    }
    private void BetClicked()
    {
        if (player.money > 0) {
            player.money = player.money - 20;
            pot = pot + 40;
            betText.text =  "Bet: " + pot.ToString();
            cashText.text = "Cash: " + player.money.ToString();
        }
    }
    private void AllInClicked() {
        pot = pot + (player.money * 2);
        player.money = 0;
        betText.text =  "Bet: " + pot.ToString();
        cashText.text = "Cash: " + player.money.ToString();
    }
    private void HitDealer()
    {
        while(dealer.handVal < 16 && dealer.cardIndex < 7) {
            dealer.GetCard();
            if (dealer.handVal > 20) {RoundOver();}
        }
        dealerText.text = "Dealer: " + (dealer.handVal - dealer.hand[0].GetComponent<CardScript>().value) + " + ?";
    }
    void RoundOver() {
        bool playerBust = player.handVal > 21;
        bool dealerBust = dealer.handVal > 21;
        bool player21 = player.handVal == 21;
        bool dealer21 = dealer.handVal == 21;
        // If stand has been clicked less than twice, no 21s or busts, quit function
        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;
        bool roundOver = true;
        // All bust, bets returned
        if (playerBust && dealerBust)
        {
            roundText.text = "All Bust: Bets returned";
            player.AdjustMoney(pot / 2);
        }
        // if player busts, dealer didnt, or if dealer has more points, dealer wins
        else if (playerBust || (!dealerBust && dealer.handVal > player.handVal))
        {
            roundText.text = "Dealer wins!";
        }
        // if dealer busts, player didnt, or player has more points, player wins
        else if (dealerBust || player.handVal > dealer.handVal)
        {
            roundText.text = "You win!";
            player.AdjustMoney(pot);
        }
        //Check for tie, return bets
        else if (player.handVal == dealer.handVal)
        {
            roundText.text = "Push: Bets returned";
            player.AdjustMoney(pot / 2);
        }
        else
        {
            roundOver = false;
        }
        // Set ui up for next move / hand / turn
        if (roundOver)
        {
            //buttons
            HitButton.gameObject.SetActive(false);
            StandButton.gameObject.SetActive(false);
            roundText.gameObject.SetActive(true);
            BetButton.gameObject.SetActive(false);
            AllInButton.gameObject.SetActive(false);
            DealButton.gameObject.SetActive(true);

            //reveal info
            dealerText.text = "Dealer: " + dealer.handVal;
            hideCard.GetComponent<Renderer>().enabled = false;
            cashText.text = "$" + player.GetMoney().ToString();
            standClicks = 0;
            playerCardCount = 2;
            pot = 40;
            if (player.money <= 0) {
                roundText.text = "Game Over. Play Again?";
                animator.SetTrigger("GameOver");
                player.money = 1000;
            }
        }
    }
}
