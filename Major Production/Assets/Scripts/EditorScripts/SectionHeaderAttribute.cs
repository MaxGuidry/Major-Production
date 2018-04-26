using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class SectionHeaderAttribute : PropertyAttribute
{
    public readonly string text;

    public SectionHeaderAttribute(string text)
    {
        this.text = text;
    }
}
