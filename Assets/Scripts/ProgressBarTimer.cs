﻿using System;
using UnityEngine;

public class ProgressBarTimer: MonoBehaviour
{
    public float MinValue = 0;
    private float _currentPercent;
    [Range(0, 100)] public float Speed;
    public float ValueLimit = 100;
    private bool _isRunning = false;
    public bool IsOneShot = false;
    
    // event to update currentPercent value
    public delegate void ProgressBarUpdate(float progress);
    public ProgressBarUpdate OnProgressBarUpdate;
    
    // update the currentPercent value
    void Update()
    {
        if (!Application.isPlaying || !_isRunning) return;
        
        if (_currentPercent < ValueLimit)
        {
            _currentPercent += Time.deltaTime * Speed;
        }
        else
        {
            _currentPercent = ValueLimit;
            _currentPercent = MinValue;

            // if it's one shot, stop the progress bar when it reaches the limit
            if (IsOneShot)
            {
                _isRunning = false;
            }
        }
        
        OnProgressBarUpdate?.Invoke(_currentPercent);
    }


    public void Start()
    {
        _isRunning = true;
    }
}