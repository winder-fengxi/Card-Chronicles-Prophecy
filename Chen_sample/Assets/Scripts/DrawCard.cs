using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawCard : MonoBehaviour
{
    public void Scene()
    {
        //切换到卡组场景
        SceneManager.LoadScene("CardStock");
    }
}
