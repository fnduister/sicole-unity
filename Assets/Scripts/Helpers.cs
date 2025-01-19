using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Helpers
{
 
    public static int[] GenerateDistinctIntegers(int n, int minValue = 0, int maxValue = 0)
    {
        var max = maxValue == 0 ? n : maxValue;
        var uniqueIntegers = new HashSet<int>();
    
        while (uniqueIntegers.Count < n)
        {
            int newInt = Random.Range(minValue, max);
            uniqueIntegers.Add(newInt);
        }
    
        return uniqueIntegers.ToArray();
    }
}    

public static class ColorExtensions
{
     public static Color ToColor(this string hex)
     {
         Color color;
         if (ColorUtility.TryParseHtmlString(hex, out color))
         {
             return color;
         }
         else
         {
             Debug.LogWarning($"Invalid hex color string: {hex}");
             return Color.white; // Return white as a fallback color
         }
     }
 }