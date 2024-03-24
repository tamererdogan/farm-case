using System.Collections;
using UnityEngine;

public class PlantField : MonoBehaviour
{
    private int state;

    private SeedSO seedSO;

    public int GetState()
    {
        return state;
    }

    public void Plant(SeedSO seedSO, float plantTimeBoost)
    {
        this.seedSO = seedSO;
        StartCoroutine(PlantRoutine(plantTimeBoost));
    }

    public IEnumerator PlantRoutine(float plantTimeBoost)
    {
        state++; //Planting state
        Debug.Log("VFX başlandı."); //Start VFX
        yield return new WaitForSeconds(seedSO.plantTime - plantTimeBoost); //Wait for planting
        Debug.Log("VFX kapandı."); //Close VFX

        //Small Object Instantiate
        GameObject smallObject = Instantiate(seedSO.smallPrefab, transform);
        smallObject.transform.localScale = new Vector3(1, 1, 1);
        smallObject.transform.localPosition = new Vector3(0, 0.55f, 0);

        state++; //Small state
    }
}
