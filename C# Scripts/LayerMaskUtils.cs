using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskUtils
{
    public static bool LayerInMask(int layer, LayerMask layerMask)
    {
        LayerMask layerMaskFromLayer = 1 << layer;
        return ((layerMask & layerMaskFromLayer) != 0);
    }
}
