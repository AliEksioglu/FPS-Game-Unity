using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    Picker picker { get; set; }
    bool isCarried { get; set; }
    Rigidbody rigidbody { get; }
}
