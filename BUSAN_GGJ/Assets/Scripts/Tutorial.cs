using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : StageManager
{
    [SerializeField] private string[] info;

    Coroutine coroutine = null;
    Coroutine minigame = null;


    [SerializeField] private GameObject positive;
    [SerializeField] private GameObject negative;

    [SerializeField] private Transform[] tracks;

    [SerializeField] private GameObject[] tutorials;

    int cur_num = 0;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if(coroutine != null)
        {
            coroutine = StartCoroutine(Tutorials());
        }
    }

    IEnumerator Tutorials()
    {
        GameObject obj = Instantiate(negative, tracks[0].position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);

        show_tutorial();

        obj = Instantiate(positive, tracks[0].position, Quaternion.identity);

        yield return null;
    }

    private void show_tutorial()
    {
        GameStop();
        tutorials[cur_num].SetActive(true);
        cur_num++;
    }
}
