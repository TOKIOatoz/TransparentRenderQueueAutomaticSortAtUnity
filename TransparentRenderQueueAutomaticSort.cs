using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTransparentSortTransparentRenderQueueAutomaticSort : MonoBehaviour
{

    [SerializeField]
    List<ObjectAndOffset> meshObjects;
    int countI, countJ, countK, listSize;
    Camera mainCamera;
    Vector3 distanceOffset;
    Transform meshObjectOffsetTransform;
    List<int> referenceMeshObjectList = new List<int>();

    void Start()
    {
        countI = 0;
        mainCamera = Camera.main;
        foreach (var meshObject in meshObjects)
        {
            meshObjects[countI].MeshObjectMaterial = meshObject.MeshObject.GetComponent<SkinnedMeshRenderer>().material;
            referenceMeshObjectList.Add(countI);

            if (meshObject.ReferenceObject == null)
            {
                meshObject.ReferenceObject = meshObject.MeshObject;
            }

            countI++;
        }

        listSize = countI;
    }

    void Update()
    {
        countI = 0;
        foreach (var meshObject in meshObjects)
        {
            meshObjectOffsetTransform = meshObject.ReferenceObject.transform;
            meshObjectOffsetTransform.localPosition += meshObject.ReferenceObjectOffset;
            distanceOffset = meshObjectOffsetTransform.position - mainCamera.transform.position;
            meshObject.SqrDistanceFromCamera = distanceOffset.sqrMagnitude;
            meshObjectOffsetTransform.localPosition -= meshObject.ReferenceObjectOffset;
            
            referenceMeshObjectList[countI] = countI;
            meshObject.SortingPriority = listSize - countI;
            for (countJ = 0; countJ < countI; countJ++)
            {
                if (meshObject.SqrDistanceFromCamera < meshObjects[referenceMeshObjectList[countJ]].SqrDistanceFromCamera)
                {
                    meshObject.SortingPriority = meshObjects[referenceMeshObjectList[countJ]].SortingPriority;
                    for (countK = countJ; countK < countI; countK++)
                    {
                        meshObjects[referenceMeshObjectList[countK]].SortingPriority--;
                    }
                    referenceMeshObjectList.Insert(countJ, countI);
                    referenceMeshObjectList.RemoveAt(countI + 1);

                    break;
                }
            }
            countI++;
        }
        foreach (var meshObject in meshObjects)
        {
            meshObject.MeshObjectMaterial.renderQueue = 3000 + meshObject.SortingPriority;
            meshObject.MeshObjectMaterial.SetFloat("_SortingPriority", meshObject.SortingPriority);
        }
    }
}
[System.Serializable]
public class ObjectAndOffset
{
    public GameObject MeshObject = default;
    public GameObject ReferenceObject = null;
    public Vector3 ReferenceObjectOffset = default;
    [System.NonSerialized]
    public int SortingPriority = 0;
    [System.NonSerialized]
    public Material MeshObjectMaterial = default;
    [System.NonSerialized]
    public float SqrDistanceFromCamera = 0;
}