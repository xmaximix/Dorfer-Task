using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using DG.Tweening;

public class GrassSlicer : MonoBehaviour
{
    [SerializeField] Transform spawnedHullsParent;
    public Material sliceMaterial;
    public LayerMask sliceMask;
    public bool isTouched;

    private void Update()
    {
        if (isTouched == true)
        {
            isTouched = false;

            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, .3f, .3f), transform.rotation, sliceMask);

            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, sliceMaterial);

                if (slicedObject == null)
                {
                    return;
                }

                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, sliceMaterial);
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, sliceMaterial);

                SetTransform(upperHullGameobject, objectToBeSliced.transform);
                SetTransform(lowerHullGameobject, objectToBeSliced.transform);

                MakeItPhysical(upperHullGameobject);
                FadeAndDestroy(lowerHullGameobject);

                objectToBeSliced.GetComponent<Grass>().Destroy();
                Destroy(objectToBeSliced.gameObject);
            }
        }
    }

    private void SetTransform(GameObject hull, Transform objectToBeSliced)
    {
        hull.transform.parent = spawnedHullsParent;
        hull.transform.position = objectToBeSliced.position;
    }

    private void FadeAndDestroy(GameObject obj)
    {
        obj.transform.DOMoveY(-2, Random.Range(3, 8));
        Destroy(obj, 10f);
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<Rigidbody>();
        var rb = obj.GetComponent<Rigidbody>();
        rb.rotation = Quaternion.Euler(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));
        rb.AddForce(new Vector3(Random.Range(-3, 3), Random.Range(2, 5), Random.Range(-3, 3)), ForceMode.Impulse);
        Destroy(obj, 5f);
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}
