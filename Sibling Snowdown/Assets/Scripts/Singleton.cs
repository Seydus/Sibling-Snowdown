using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    public ObjectPool ObjectPool;
    public UIManager UIManager;
    public GameManager GameManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
