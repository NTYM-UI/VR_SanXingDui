using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuanSongMen : MonoBehaviour
{
    public GameObject ChuanSongmen;

    void Start()
    {
        ChuanSongMenDisappear();
    }

    public void ChuanSongMenAppear()
    {
        ChuanSongmen.SetActive(true);
    }

    public void ChuanSongMenDisappear()
    {
        ChuanSongmen.SetActive(false);
    }
}
