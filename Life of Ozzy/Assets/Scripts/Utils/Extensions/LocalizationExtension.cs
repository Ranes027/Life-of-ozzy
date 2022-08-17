using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeOfOzzy.Model;

namespace LifeOfOzzy.Utils
{
    public static class LocalizationExtension
    {
        public static string Localize(this string key)
        {
            return LocalizationManager.I.Localize(key);
        }
    }

}
