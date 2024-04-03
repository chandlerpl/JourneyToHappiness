using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ScoringSystem
{
    public UnityEvent<float> valueChanged;
    public UnityEvent<string> valueChangedAsString;

    [SerializeField]
    private float _value;
    [SerializeField]
    private Vector2 _minMaxValue;

    public float Value {  get { return _value; }
    set
        {
            if(_minMaxValue.y > _minMaxValue.x)
            {
                _value = Mathf.Min(Mathf.Max(value, _minMaxValue.x), _minMaxValue.y);
            } else
            {
                _value = value;
            }

            valueChanged?.Invoke(_value);
            valueChangedAsString?.Invoke(_value.ToString());
        }
    }

    public static implicit operator float(ScoringSystem s) => s.Value;
}
