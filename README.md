# <p align="center">토끼의 행성</p>

<p align="center">
<img src="https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/0b826058-0f23-49c7-b9cb-a6a39f41a659" width="200">
</p>

## 🎮게임 소개
장애물을 피해 하트를 모으며 10개의 스테이지를 통과하는 게임 &nbsp;

- 장르: 플랫폼 로그라이크 게임
- 특징: 메인 게임과 미니 게임으로 나누어져 메인 게임에서 장애물과 접촉 시 미니 게임으로 전환
- 비고: 대학교 졸업 작품을 리뉴얼하여 플레이스토어에 출시

&nbsp;

## 👩🏻‍💻개발 기간 & 개발 인원
- 개발 기간: 2023.09.06~2023.10.31(약 8주)
- 개발 인원: 3인(아트 1, 프로그래머 2)
  
&nbsp;

## 🔗구글 플레이스토어 링크
https://play.google.com/store/apps/details?id=com.h3.rabbitplanet

&nbsp;

## ✏️팀 내 맡은 역할
- 팀장
- 메인 게임 홀수 스테이지 [click](#메인-게임)
- 미니 게임 4개(플래피 래빗, 미로 찾기, 점핑 문, 큐브 맞추기) [click](#미니-게임)
- 장애물 3종(독 장판, 스팀, 가시 꽃) [click](#장애물)

&nbsp;

## ❗메인 게임
> - 정해진 개수의 하트를 모두 모아 클리어 후 다음 스테이지로 이동

&nbsp;
![1-ezgif com-video-to-gif-converter](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/db88dc2d-7da4-4d9c-8ea1-f8fe700b2f98)

&nbsp;
▲ 1스테이지

&nbsp;

![2-ezgif com-video-to-gif-converter](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/10d6a1e0-31a8-4ba1-8706-febf8735dace)

&nbsp;
▲ 3스테이지

<details>
 <summary>코드 보기</summary>
 
```ts

```
▲  스크립트
</details>
&nbsp;

## ❗미니 게임
1. 
   > - 
   > - 
   > - 

<details>
 <summary>코드 보기</summary>
 
```ts

```
▲  스크립트
</details>
&nbsp;

## ❗장애물
1. 독 장판
   > - 독 식물에서 독이 뿜어져나오는 곳이 그림자로 예고 후 독이 퍼짐
   > - 독에 닿으면 미니 게임으로 전환
   
   ![독장판](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/a57d3de1-4209-4296-b03c-c986032d9757)

<details>
 <summary>코드 보기</summary>
 
```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGenerator : MonoBehaviour
{
    public GameObject fxPoisonInFlower;
    public GameObject fxPoisonOnTile;
    private float repeatInterval = 3.0f;    //반복되는 시간

    private GameObject poisonGoOnTile; //타일에 생성되는 독
    private GameObject poisonGoInFlower; //꽃에 생성되는 독

    public float x1;
    public float x2;
    public float y;
    public float z1;
    public float z2;

    public void Init(float x1, float x2, float y, float z1, float z2)
    {
        this.x1 = x1;
        this.x2 = x2;
        this.y = y;
        this.z1 = z1;
        this.z2 = z2;
    }

    void Start()
    {
        //반복적으로 독 생성(꽃에서)
        InvokeRepeating("GeneratePoisonInFlower", 0.0f, repeatInterval);

        //반복적으로 독 생성(타일에서)
        InvokeRepeating("GeneratePoisonOnTile", 0.0f, repeatInterval);

    }

    public void GeneratePoisonInFlower()
    {
        this.poisonGoInFlower = Instantiate(fxPoisonInFlower);
    }

    public void GeneratePoisonOnTile()
    {
        this.poisonGoOnTile = Instantiate(fxPoisonOnTile);
        this.poisonGoOnTile.transform.position = new Vector3(Random.Range(x1, x2), y, Random.Range(z1, z2));  //랜덤 위치 정해주기
    }
}

```
▲  스크립트
</details>

&nbsp;

2. 스팀
   > - 스팀 식물에서 스팀이 뿜어져나옴
   > - 스팀에 닿으면 미니 게임으로 전환
   
   ![스팀](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/bd1e5c22-7092-4a04-80aa-9c0d5fc11df2)

<details>
 <summary>코드 보기</summary>
 
```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{
    public GameObject fxSteam;
    public bool isSteamActive;


    private void Start()
    {
        InvokeRepeating("ToggleSteam", 0.0f, 2f);   //2초마다 반복
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("스팀에 닿음");
            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.StartMiniGame);
        }
    }

    public void ToggleSteam()
    {
        //켜져있으면 끄고 꺼져있으면 킴
        if (isSteamActive)
        {
            this.fxSteam.SetActive(false);
            isSteamActive = false;
        }
        else
        {
            this.fxSteam.SetActive(true);
            isSteamActive = true;
        }
    }

}

```
▲  스크립트
</details>

&nbsp;

3. 가시 꽃
   > - 일반 꽃을 밟으면 가시 꽃으로 변함
   > - 가시 꽃에 닿으면 미니 게임으로 전환
   
   ![가시꽃](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/3c95ca95-8bc7-4a1d-b0b2-11e669b0588d)

<details>
 <summary>코드 보기</summary>
 
```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn1 : MonoBehaviour
{
    public Animator anim;
    public GameObject sharpThorn;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.anim.Play("Disappear");    //가시 꽃으로 바뀌는 애니메이션

            this.sharpThorn.SetActive(true);    //가시 꽃 활성화

            EventDispatcher.instance.SendEvent((int)EventEnum.eEventType.StartMiniGame);    //미니게임 전환 이벤트 발동
        }
    }
}

```
▲  스크립트
</details>
&nbsp;
