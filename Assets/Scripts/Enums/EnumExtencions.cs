using UnityEngine;
using UnityEditor;
using System;

public static class EnumExtencions {


    public static int  Count( Type type) {

        int count = Enum.GetValues(type).Length;

        return count;
    }


  

}