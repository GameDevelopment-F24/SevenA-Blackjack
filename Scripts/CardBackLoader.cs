using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBackLoader : MonoBehaviour
{
    public GameObject cardBack;
    public Sprite Back_red;
    public Sprite Back_green;
    public Sprite Back_yellow;
    public Sprite Back_blue;
        
    // Start is called before the first frame update
    void Start()
    {
        SkinManager.CardBackSkin selectedSkin = SkinManager.LoadSkinChoice();
        SpriteRenderer sr = cardBack.GetComponent<SpriteRenderer>();
        switch (selectedSkin) {
            case SkinManager.CardBackSkin.Red:
                sr.sprite = Back_red;
                break;
            case SkinManager.CardBackSkin.Green:
                sr.sprite = Back_green;
                break;
            case SkinManager.CardBackSkin.Yellow:
                sr.sprite = Back_yellow;
                break;
            case SkinManager.CardBackSkin.Blue:
                sr.sprite = Back_blue;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
