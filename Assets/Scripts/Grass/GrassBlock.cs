using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBlock : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] public BoxCollider col;

    private Backpack backpack;
    private Transform backpackTransform;
    private bool moving;

    private void Start()
    {
        col.enabled = false;
        transform.rotation = Random.rotation;
        rb.AddForce(new Vector3(Random.Range(-1, 1), Random.Range(7, 9), Random.Range(-1, 1)), ForceMode.Impulse);
        StartCoroutine(EnableCollider());
    }

    private void Update()
    {
        if (moving)
        {
            MoveToBackpack();

            if (Vector3.Distance(transform.position, backpackTransform.position) < 0.01f && Quaternion.Angle(transform.rotation, backpackTransform.rotation) <= 1)
            {
                moving = false;
                transform.parent = backpack.transform;
                backpack.pickedBlocks--;
                backpack.AddBlock(this);
            }
        }
    }

    private void MoveToBackpack()
    {
        transform.position = Vector3.MoveTowards(transform.position, backpackTransform.position, Time.deltaTime * 5);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, backpackTransform.rotation, Time.deltaTime * 360);
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(.5f);
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            backpack = player.backpack;
            if (backpack.blocksInside + backpack.pickedBlocks + 1 > backpack.maxBlocks || backpack.selling)
            {
                return;
            }
            backpack.pickedBlocks++;
            backpackTransform = backpack.transform;
            InitMove();
        }
    }

    private void InitMove()
    {
        rb.isKinematic = true;
        col.enabled = false;
        moving = true;
    }
}
