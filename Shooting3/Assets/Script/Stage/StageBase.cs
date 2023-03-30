using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBase : MonoBehaviour
{
    public List<Func<IEnumerator>> stage = new List<Func<IEnumerator>>();
}
