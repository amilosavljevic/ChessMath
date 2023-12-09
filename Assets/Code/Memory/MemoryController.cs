using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryController: MonoBehaviour
{
    [SerializeField] private Button tileButtonPrefab;
    [SerializeField] private Transform tileHolder;
    private List<Button> buttons = new List<Button>();
    
    private void Start()
    {
        for (int i = 0; i < 16; i++)
        {
            var button = Instantiate(tileButtonPrefab, tileHolder);
            buttons.Add(button);
        }
    }
    
}