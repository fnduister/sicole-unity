using System;
using UnityEngine;

public class ProgressBarTimer: MonoBehaviour
{
    public float MinValue = 0;
    public float CurrentPercent => _currentPercent; // Add public accessor
    private float _currentPercent;
    [Range(0, 100)] public float Speed;
    public float ValueLimit = 100;
    public bool IsRunning => _isRunning; // Add public accessor
    private bool _isRunning = false;
    public bool IsOneShot = false;
    
    // event to update currentPercent value
    public delegate void ProgressBarUpdate(float progress);
    public ProgressBarUpdate OnProgressBarUpdate;
    
    // create an event to indicate that the progress bar has reached the limit
    public event Action OnProgressBarLimitReached;
    
    void Update()
    {
        if (!Application.isPlaying || !_isRunning) return;
        
        if (_currentPercent < ValueLimit)
        {
            _currentPercent += Time.deltaTime * Speed;
        }
        else
        {
            _currentPercent = MinValue;

            // if it's one shot, stop the progress bar when it reaches the limit
            if (IsOneShot)
            {
                _isRunning = false;
            }
            
            OnProgressBarLimitReached?.Invoke();
        }
        
        OnProgressBarUpdate?.Invoke(_currentPercent);
    }

    public void Pause()
    {
        _isRunning = false;
    }

    public void Reset()
    {
        _currentPercent = 0;
        _isRunning = true;
    }


    public void Start()
    {
        _isRunning = true;
    }
}