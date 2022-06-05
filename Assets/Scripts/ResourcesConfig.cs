using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesConfig", menuName = "ScriptableObjects/ResourcesConfig", order = 1)]
[Serializable]
public class ResourcesConfig : ScriptableObject
{
    public List<ResourcePair> allResources;
}

[Serializable]
public class ResourcePair
{
    public ResourceType type;
    public GameObject prefab;
}
