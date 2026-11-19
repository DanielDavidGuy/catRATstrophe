using System.Collections;
using UnityEngine;

public class Resource : LevelElement {
    public int resourcesAvailable;
	
    void Start() {
        resourcesAvailable = 1;
    }

    public IEnumerator mine()
    {
		Inventory.inventory["yarn"] += resourcesAvailable;
		resourcesAvailable = 0;
		yield return new WaitForSeconds(5);
		resourcesAvailable = 1;
    }
}
