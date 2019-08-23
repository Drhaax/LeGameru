using ByteSerializer;
using FixMath.NET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ExtensionMethods
{
    public static CustomVector ToCustomVector(this Vector3 vector){
        return new CustomVector((Fix64)vector.x,(Fix64)vector.y,(Fix64)vector.z);
    }
    public static Vector3 ToVector3(this CustomVector vector) {
        return new Vector3((float)vector.x, (float)vector.y, (float)vector.z);
    }
}
