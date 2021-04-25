using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string roomName;

    private void Start()
    {
        var ui = FindObjectOfType<MainUI>();
        ui.ShowRoomName(roomName);
    }
}
