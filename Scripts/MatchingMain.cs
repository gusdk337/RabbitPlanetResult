using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingMain : MonoBehaviour
{
    public GameObject[] TotemPrefabs;
    public GameObject TotemSetPrefab;

    private float repeatInterval = 4f;

    public GameObject basicTotem;
    public UIMatchingDirector director;

    private void Awake()
    {
        //처음 시작하면 기본 토템이 생김
        GameObject basicTotemGo = Instantiate(basicTotem);

        basicTotemGo.transform.position = new Vector3(-0.0561285f, 29.79639f, 17.74149f);
        //basicTotemGo.transform.rotation = Quaternion.Euler(0, 90, 0);
        //basicTotemGo.transform.localScale = Vector3.one;

        //게임 방법 설명중 플레이 안되게 멈추기
        Time.timeScale = 0;

    }

    private void Start()
    {
        AudioListener.volume = 5;
        //토템이 반복적으로 생김
        InvokeRepeating("GenerateTotemSet", 0.0f, repeatInterval);
    }

    private void Update()
    {
        this.ClickTotem();

    }

    public void GenerateTotemSet()
    {
        GameObject totemSetGo = Instantiate(TotemSetPrefab);
        totemSetGo.transform.position = new Vector3(0, 30f, 100f);

    }

    public void ClickTotem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit);

                //클릭한 토템이 토템1이면
                if (hit.collider.gameObject.tag == "BasicTotem1")
                {
                    SoundManager.PlaySFX("Pop");
                    //BasicTotems(Clone)을 찾아 부모로 설정하고 토템2로 변경
                    GameObject parent = GameObject.Find("BasicTotems(Clone)");

                    GameObject basicTotem = Instantiate(TotemPrefabs[1], hit.collider.transform.position, Quaternion.Euler(0, -90, 0));
                    basicTotem.transform.parent = parent.transform;

                    Destroy(hit.collider.gameObject);
                }
                //클릭한 토템이 토템2면
                else if (hit.collider.gameObject.tag == "BasicTotem2")
                {
                    SoundManager.PlaySFX("Pop");

                    //BasicTotems(Clone)을 찾아 부모로 설정하고 토템1로 변경
                    GameObject parent = GameObject.Find("BasicTotems(Clone)");

                    GameObject basicTotem = Instantiate(TotemPrefabs[0], hit.collider.transform.position, Quaternion.Euler(0, -90, 0));
                    basicTotem.transform.parent = parent.transform;

                    Destroy(hit.collider.gameObject);
                }

            }
        }
    }
}
