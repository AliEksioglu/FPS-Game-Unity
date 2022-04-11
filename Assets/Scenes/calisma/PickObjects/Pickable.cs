#pragma warning disable CS0108
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pickable :MonoBehaviour, IPickable
{
    public bool isCarried { get { return _isCarried; } set{ _isCarried = value; } }
    bool _isCarried=false;

    public Rigidbody rigidbody { get { return _rigidbody; } }

    public Picker picker { get { return _picker; } set { _picker = value; } }
    Picker _picker=null;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

}
