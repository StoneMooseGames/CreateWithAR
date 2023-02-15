using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PetList", menuName = "Pets/new Petlist", order = 1)]
public class Petlist : ScriptableObject
{

    public List<GameObject> pets;
}
