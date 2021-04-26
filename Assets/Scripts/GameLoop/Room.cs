using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string roomName;

    private void Start()
    {
        StartCoroutine(WaitAndShowName());
    }

    private IEnumerator WaitAndShowName()
    {
        yield return new WaitForSeconds(3);
        var ui = FindObjectOfType<MainUI>();
        ui.ShowRoomName(roomName);
    }
}
