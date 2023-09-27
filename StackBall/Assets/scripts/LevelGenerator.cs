using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] public GameObject platform;
    [SerializeField] private float angleStep;
    [SerializeField] private int platformAmount;
    [SerializeField] private float platformHeight;

    [SerializeField] Material goodMaterial;
    [SerializeField] Material badMaterial;

    [SerializeField] private float rotSpeed;


    [ContextMenu("GenerateLevel")]

    private void Update()
    {
        transform.Rotate(Vector3.up*rotSpeed*Time.deltaTime);
    }
    public void GenerateLevel()
    {
        for(int i = 0; i < platformAmount; i++)
        {
            var newObj = Instantiate(platform, Vector3.up* -platformHeight* i, Quaternion.Euler(0, angleStep*i, 0), transform);

            int chilCount = newObj.transform.childCount;
            for (int a = chilCount-1; a >= 0; a--)
            {
                var child = newObj.transform.GetChild(a).gameObject;
                child.gameObject.tag = "Good";
                child.GetComponent<Renderer>().sharedMaterial = goodMaterial;
            }
            // TODO ineficient do NotApproximatelyEqual have 2 loops
            if(Random.Range(0,100)<15)
            {
                //Set bad material
                int randChild =  Random.Range(0, chilCount);
                for(int a = chilCount-1; a >= 0; a--)
                {
                    if(a == randChild)
                    {
                        continue;
                    }
                    var child = newObj.transform.GetChild(a).gameObject;
                    child.gameObject.tag = "Bad";
                    child.GetComponent<Renderer>().sharedMaterial = badMaterial;
                }
            }
        }
    }

    [ContextMenu("Clean")]
    public void Clean()
    {
        int chilCount = transform.childCount;
        for(int i = chilCount-1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
