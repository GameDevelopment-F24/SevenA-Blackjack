using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public int value;

    public int GetValue() {
        return value;
    }
    public void SetValue(int newValue) {
        value = newValue;
    }
    public Sprite SetSprite(Sprite newSprite) {
        return gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
    public string GetSpriteName() {
        return GetComponent<SpriteRenderer>().sprite.name;
    }
    public void ResetCard() {
        value = 0;
    }
}
