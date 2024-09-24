using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public enum CardBackSkin {Red, Green, Blue, Yellow};
    
    // Save the skin choice using PlayerPrefs
    public void SaveSkinChoice(int skinIndex)
    {
        PlayerPrefs.SetInt("CardBackSkin", skinIndex);
        PlayerPrefs.Save(); // Make sure it's saved immediately
    }

    // Load the saved skin choice
    public static CardBackSkin LoadSkinChoice()
    {
        int skinIndex = PlayerPrefs.GetInt("CardBackSkin", 0); // Default to Red (0)
        return (CardBackSkin)skinIndex;
    }
}
