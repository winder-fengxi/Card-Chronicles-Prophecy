using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCount : MonoBehaviour
{
    public Text counterText;
    private int counter = 0;

    public bool SetCounter(int _value)
    {
        counter += _value;
        OnCounterChange();

        if (counter == 0)
        {
            Destroy(gameObject);
            return false;
        }
        return true;
    }

    public void OnCounterChange()
    {
        counterText.text = counter.ToString();
    }
}
