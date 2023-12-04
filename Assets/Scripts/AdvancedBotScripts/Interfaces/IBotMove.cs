using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IBotMove
{
    void Move(Vector3 direction, float speed);
}
