﻿using System.Collections.Generic;
using UnityEngine;

public static class UnityComponentExtensions
{
    /// <summary>
    /// Destroy component
    /// </summary>
    /// <param name="component"></param>
    public static void Destroy(this Component component)
    {
        Object.Destroy(component);
    }

    /// <summary>
    /// Destroy gameObject
    /// </summary>
    /// <param name="gameObject"></param>
    public static void Destroy(this GameObject gameObject)
    {
        Object.Destroy(gameObject);
    }

    /// <summary>
    /// Add a child to the gameobject. Same as SetParent on other.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="other"></param>
    /// <param name="worldPositionStays">If true, the parent-relative position, scale and
    /// rotation are modified such that the object keeps the same world space position,
    /// rotation and scale as before.</param>
    public static void AddChild(this GameObject gameObject, GameObject other,bool worldPositionStays = true)
    {
        gameObject.transform.AddChild(other.transform, worldPositionStays);
    }

    /// <summary>
    /// Add a child to the gameobject. Same as SetParent on other.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="other"></param>
    /// <param name="worldPositionStays">If true, the parent-relative position, scale and
    /// rotation are modified such that the object keeps the same world space position,
    /// rotation and scale as before.</param>
    public static void AddChild(this GameObject gameObject, Component other, bool worldPositionStays = true)
    {
        gameObject.transform.AddChild(other, worldPositionStays);
    }

    /// <summary>
    /// Add a child to the component.  Same as SetParent on other.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="other"></param>
    /// <param name="worldPositionStays">If true, the parent-relative position, scale and
    /// rotation are modified such that the object keeps the same world space position,
    /// rotation and scale as before.</param>
    public static void AddChild(this Component component, Component other, bool worldPositionStays = true)
    {
        other.transform.SetParent(component.transform, worldPositionStays);
    }

    /// <summary>
    /// Get all children from tranform. If any.
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static List<Transform> GetAllChildren(this Transform transform)
    {
        var children = new List<Transform>();
        foreach(Transform child in transform)
            children.Add(child);

        return children;
    }

    /// <summary>
    /// Detach GameObject from parent
    /// </summary>
    /// <param name="gameObject"></param>
    public static void DetachFromParent(this GameObject gameObject)
    {
        gameObject.transform.parent = null;
    }
    
    /// <summary>
    /// Detach component from parent
    /// </summary>
    /// <param name="component"></param>
    public static void DetachFromParent(this Component component)
    {
        component.transform.parent = null;
    }

    /// <summary>
    /// Distance between two objects. Same as Vector3.Distance
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="useLocalPosition"></param>
    /// <returns></returns>
    public static float Distance(this Component from, Component to, bool useLocalPosition = false)
    {
        return useLocalPosition ? Vector3.Distance(from.transform.localPosition, to.transform.localPosition) : 
            Vector3.Distance(from.transform.position, to.transform.position);
    }

    /// <summary>
    /// Distance between two objects 2D. Same as Vector2.Distance.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="useLocalPosition"></param>
    /// <returns></returns>
    public static float Distance2D(this Component from, Component to, bool useLocalPosition = false)
    {
        return useLocalPosition ? Vector2.Distance(from.transform.localPosition, to.transform.localPosition) : 
            Vector2.Distance(from.transform.position, to.transform.position);
    }
}