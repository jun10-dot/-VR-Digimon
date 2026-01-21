using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconCtrl : MonoBehaviour
{
    [SerializeField]
    public GameObject[] quads;
    [SerializeField]
    private GameObject play;
    [SerializeField]
    private GameObject sleep;
    [SerializeField]
    private Material[] mats;

    private petCtrl pet;

    public GameObject player;

    void Update()
    {
        foreach (GameObject q in quads)
        {
            q.transform.position = new Vector3(q.transform.parent.transform.position.x, q.transform.parent.transform.position.y + 2.0f,
                                                q.transform.parent.transform.position.z);
        }
        foreach (GameObject q in quads)
        {
            q.transform.LookAt(player.transform);
        }
    }

    public void SelectPlay()
    {
        quads[2].SetActive(false);

    }

    public void SelectSleep()
    {
        quads[3].SetActive(false);
    }

    public void BackToIdle()
    {
        quads[2].SetActive(true);
        quads[3].SetActive(true);
    }

    public void ChangeIcon()
    {
        quads[2].GetComponent<MeshRenderer>().material = mats[0];
        quads[3].GetComponent<MeshRenderer>().material = mats[1];
    }
}
