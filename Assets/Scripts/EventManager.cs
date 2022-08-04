using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent OnGrassBlockCollected = new UnityEvent();
    public static UnityEvent OnGrassBlockSold = new UnityEvent();

    public static void SendGrassBlockCollected()
    {
        OnGrassBlockCollected.Invoke();
    }

    public static void SendGrassBlockSold()
    {
        OnGrassBlockSold.Invoke();
    }
}
