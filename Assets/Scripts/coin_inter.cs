using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class coin_inter : MonoBehaviour
{
    public GameObject _player;
    public GameObject num_Coin;
    public GameObject openChest;
    public GameObject closeChest;



    GameObject[] coin;
    List<GameObject> listCoin = new List<GameObject>();
    float coRotAng = 1.0f;

    private bool keyFDown = false;
    private int numOfCo = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 10; i++)
        {
            listCoin.Add(GameObject.Find("set_coins/CP" + i));
        }
        coin = listCoin.ToArray();
        coin[9].SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            keyFDown = true;

        if (numOfCo >= 10)
            SceneManager.LoadScene("End");
    }

    private void FixedUpdate()
    {
        UI_calcNumOfCoin();
        if (numOfCo >= 9)
        {
            coin[9].SetActive(true);
            openChest.SetActive(true);
            closeChest.SetActive(false);
        }
            
        for (int i = 0; i < listCoin.Count; i++)
        {
            coin[i].transform.rotation = Quaternion.Euler(0.0f, coinRotation() , 0.0f);
        }

        isNearCoin(false);

        if (keyFDown)
        {
            keyFDown = false;
            int coinIndex = isNearCoin(false);
            if(coinIndex != -1)
            {
                if(coin[coinIndex].activeSelf) {
                    coin[coinIndex].SetActive(false);
                    numOfCo++; 
                }
            }
        }
    }

    Vector3 midPoint(GameObject obj0, GameObject obj1)
    {
        return (obj1.transform.position + obj0.transform.position) / 2;
    }

    float calcDistance(Vector3 vec1, Vector3 vec2)
    {
        return (vec2 - vec1).sqrMagnitude;
    }

    float coinRotation()
    {
        coRotAng += (coRotAng < 360) ? 0.5f : -359.0f;
        return coRotAng;
    }

    int isNearCoin(bool isDebugging)
    {
        int coinSetInd = -1;
        for (int i = 0; i < listCoin.Count; i++)
        {
            
            if (calcDistance(_player.transform.position, coin[i].transform.position) <= 0.5f)
            {
                coinSetInd = i;
                if (isDebugging)
                    Debug.Log("coinSetInd: " + coinSetInd);
                return coinSetInd;
            }
            
        }

        if (isDebugging)
            Debug.Log("coinSetInd: " + coinSetInd);
        return coinSetInd;
    }

    void UI_calcNumOfCoin()
    {
        Text numOfC = num_Coin.GetComponent<Text>();
        numOfC.text = "Coins: " + numOfCo;
    }
}
