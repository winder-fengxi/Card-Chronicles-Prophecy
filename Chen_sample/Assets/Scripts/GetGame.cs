using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetGame : MonoBehaviour
{
    public void Scene()
    {
        //切换到卡组场景
        SceneManager.LoadScene("SampleScene");
    }

    public void StoreButton()
 {
  SceneManager.LoadScene("Store");
 }

    public void StartGameButton()
  {
    SceneManager.LoadScene("Map");
  }
    public void BattleButton()
  {
    SceneManager.LoadScene("BattleScene");
  }
}
