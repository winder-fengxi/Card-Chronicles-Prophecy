using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class StoreManger : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "现有金币数： " + RoleManager.Instance.coin; 

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void Drawcard(int cnt)
    {
        for (int i=0; i<cnt; i++)
        {
            //生成一张卡
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, -420);
            
        }
    }
    private void UpdateCard()
    {
        text.text = "现有金币数： " + RoleManager.Instance.coin;
    }
    private void Buy()
    {
        if(RoleManager.Instance.coin >=5) 
        {
            RoleManager.Instance.coin -= 5;
        }
        UpdateCard();
    }
}
