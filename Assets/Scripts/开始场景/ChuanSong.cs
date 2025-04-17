using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuanSong : MonoBehaviour
{
    public GameObject ChuanSongMen;

    void Start()
    {
        ChuanSongMenDisappear();
    }

    public void ChuanSongMenAppear()
    {
        ChuanSongMen.SetActive(true);
    }

    public void ChuanSongMenDisappear()
    {
        ChuanSongMen.SetActive(false);
    }
}
