using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LifeOfOzzy.Utils
{
    public static class SerializedPropertyExtansions
    {
        public static bool GetEnum<TEnumType>(this SerializedProperty property, out TEnumType retValue) where TEnumType : Enum
        {
            retValue = default;
            var names = property.enumNames;

            if (names == null || names.Length == 0)
                return false;

            var enumName = names[property.enumValueIndex];
            retValue = (TEnumType)Enum.Parse(typeof(TEnumType), enumName);

            return true;
        }
    }

}


