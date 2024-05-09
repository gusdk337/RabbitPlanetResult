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

## ▶️플레이 영상
https://youtu.be/a9wI2uNZs-M

&nbsp;

## ✏️팀 내 맡은 역할
- 팀장
- 메인 게임 홀수 스테이지
- 미니 게임 4개(매칭 토템, 점핑 래빗, 미로 찾기, 플래피 래빗)
- 장애물 3종(독 장판, 스팀, 가시 꽃)

&nbsp;

## ❗메인 게임
> - 정해진 개수의 하트를 모두 모아 클리어 후 다음 스테이지로 이동

&nbsp;

![1스테이지](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/07b3269e-77fb-4706-9170-130ca0a4f1a1)

&nbsp;
▲ 1스테이지

&nbsp;

![3스테이지](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/e39de233-1bd8-4757-a0b5-892022fdc876)

&nbsp;
▲ 3스테이지

&nbsp;

![5스테이지](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/26aac410-26e0-4d5b-8049-473f15e4a089)

&nbsp;
▲ 5스테이지

&nbsp;

![7스테이지](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/483a0235-e18d-4d93-8a64-3007c47fb087)

&nbsp;
▲ 7스테이지

&nbsp;

![9스테이지](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/73bd3c21-2b22-44d7-938e-d70a624c5f97)

&nbsp;
▲ 9스테이지

&nbsp;

## ❗미니 게임
1. 매칭 토템
   > - 다가오는 토템의 색깔에 맞춰 토템을 클릭해서 색깔을 변경하는 게임

    ![매칭토템](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/91e734b5-8449-4af3-a5b9-68ddd209aa15)

<details>
 <summary>코드 보기</summary>
 
```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    public enum eState
    {
        Idle, Jump
    }

    public Animator anim;
    private eState state;
    private Rigidbody rb;
    public float jumpForce = 5f;
    private bool isAnimating = false;

    public int heartCnt;

    private void Start()
    {
        AudioListener.volume = 5;
        this.anim = this.GetComponent<Animator>();
        this.state = eState.Idle;
        this.rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.anim.SetInteger("State", 1);
            this.state = eState.Jump;
            this.Jump();
            this.isAnimating = true;

            if (isAnimating)
            {
                this.anim.Play("Jump", 0, 0);
            }
        }
        else
        {
            this.anim.SetInteger("State", 0);
            this.state = eState.Idle;
        }

        if(this.gameObject.transform.position.y > 3.9 || this.gameObject.transform.position.y < -3)
        {
            Destroy(this.gameObject);
        }
    }

    public void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;

        this.state = eState.Jump;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            this.heartCnt++;
            Debug.Log(heartCnt);
            SoundManager.PlaySFX("Pop");
            Destroy(collision.gameObject);

        }
    }
}

```
▲ FlappyPlayer 스크립트

```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyMain : MonoBehaviour
{
    public UIFlappyDirector director;

    public GameObject stemPrefab;
    public GameObject flowerPrefab;
    public GameObject heartPrefab;

    private float repeatInterval1 = 3.0f;
    private float repeatInterval2 = 4.0f;
    private float repeatInterval3 = 4.5f;

    private void Awake()
    {
        //게임 방법 설명중 플레이 안되게 멈추기
        Time.timeScale = 0;
    }

    public void Init()
    {
        this.director.Init();
    }

    private void Start()
    {
        //n초마다 장애물 생성
        InvokeRepeating("GenerateStem", 0.0f, repeatInterval1);
        InvokeRepeating("GenerateFlower", 0.0f, repeatInterval2);

        //n초마다 하트 생성
        InvokeRepeating("GenerateHeart", 0.0f, repeatInterval3);
    }

    public void GenerateStem()
    {
        GameObject stem = Instantiate(stemPrefab);
        stem.transform.position = new Vector3(12.01f, Random.Range(4.9f, 5.3f), -0.354388f);
        stem.transform.localScale = new Vector3(Random.Range(1f, 3f), Random.Range(1f, 3f), Random.Range(1f, 3f));

    }

    public void GenerateFlower()
    {
        GameObject flower = Instantiate(flowerPrefab);
        flower.transform.position = new Vector3(12.01f, -2.53f, -0.354388f);
        flower.transform.localScale = new Vector3(Random.Range(8.817859f, 15f), Random.Range(8.817859f, 15f), Random.Range(8.817859f, 15f));
    }
    public void GenerateHeart()
    {
        GameObject heartGo = Instantiate(heartPrefab);
        heartGo.transform.position = new Vector3(12.01f, Random.Range(-0.1f, 3f), -0.354388f);
    }

}

```
▲ FlappyMain 스크립트

```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlappyDirector : MonoBehaviour
{
    public FlappyPlayer player;

    public UIGameOver gameOverPopup;
    public UIGameRule uiGameRule;
    public UIMemoryClear uiGameClear;
    public Text txtCurrentHeartCnt;
    public Text txtMaxHeartCnt;

    public void Init()
    {

    }

    private void Start()
    {
        this.txtMaxHeartCnt.text = 5.ToString();
    }

    private void Update()
    {
        if(player == null)
        {
            this.gameOverPopup.gameObject.SetActive(true);
        }

        this.txtCurrentHeartCnt.text = this.player.heartCnt.ToString();

        if(this.txtCurrentHeartCnt.text == this.txtMaxHeartCnt.text)
        {
            this.uiGameClear.gameObject.SetActive(true);
        }
    }
}

```
▲ UIFlappyDirector 스크립트
</details>
&nbsp;

2. 점핑 래빗
   > - 점프 버튼을 눌러 점프하면서 하트를 모으는 게임

    ![점핑래빗](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/b8bed44d-2612-4e2c-a437-db1de7d2d9b1)

<details>
 <summary>코드 보기</summary>
 
```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlayer : MonoBehaviour
{
    public enum eState
    {
        Idle, Jump
    }

    private float moveSpeed = 2f;
    private Animator anim;
    private eState state;
    private Rigidbody rb;
    private float jumpForce = 4f;
    private bool isAnimating = false;

    public int heartCnt;

    public VariableJoystick joy;

    private void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        this.Move();
    }

    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal") + joy.Horizontal;

        Vector3 dir = Vector3.Normalize(new Vector3(h, 0, 0));

        this.transform.Translate(dir * this.moveSpeed * Time.deltaTime, Space.World);
    }

    public void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;

        this.state = eState.Jump;
        this.anim.SetInteger("State", 1);

        this.isAnimating = true;

        if (isAnimating)
        {
            this.anim.Play("Jump", 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            this.heartCnt++;
            Debug.Log(heartCnt);
            SoundManager.PlaySFX("Pop");
            Destroy(collision.gameObject);
        }
    }
}

```
▲ JumpingPlayer 스크립트

```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingMain : MonoBehaviour
{
    public GameObject boardPrefab;
    private float repeatInterval = 1.3f;

    private void Awake()
    {
        //게임 방법 설명중 플레이 안되게 멈추기
        Time.timeScale = 0;
    }

    private void Start()
    {
        InvokeRepeating("GenerateBoard", 0.0f, repeatInterval);
    }

    public void GenerateBoard()
    {
        GameObject boardGo = Instantiate(boardPrefab);
        boardGo.transform.position = new Vector3(Random.Range(-3f, 3f), 19.46f, 33f);
    }

}

```
▲ JumpingMain 스크립트

```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private float speed = 3f;


    private void Update()
    {
        this.MoveBoard();
    }

    public void MoveBoard()
    {
        this.transform.Translate(-this.transform.forward * this.speed * Time.deltaTime);

        if (this.gameObject.transform.position.z < 7f)
        {
            Destroy(this.gameObject);
        }

    }
}

```
▲ Board 스크립트

```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJumpingDirector : MonoBehaviour
{
    public Button btnJump;
    public JumpingPlayer player;
    public UIGameRule uiGameRule;
    public UIGameOver uiGameOver;
    public UIMemoryClear uiGameClear;

    public Text txtCurrentHeartCnt;
    public Text txtMaxHeartCnt;

    private bool isClear;

    private void Start()
    {
        this.txtMaxHeartCnt.text = 3.ToString();

    }
    void Update()
    {
        this.btnJump.onClick.AddListener(() =>
        {
            this.player.Jump();
        });

        this.txtCurrentHeartCnt.text = this.player.heartCnt.ToString();

        if (this.txtCurrentHeartCnt.text == this.txtMaxHeartCnt.text)
        {
            this.uiGameClear.gameObject.SetActive(true);
            this.isClear = true;
        }

        if(this.player.transform.position.y <= 21f)
        {
            if (!this.isClear)
            {
                this.uiGameOver.gameObject.SetActive(true);
            }
        }

    }
}

```
▲ UIJumpingDirector 스크립트

</details>
&nbsp;

3. 미로 찾기
   > - 제한 시간 안에 미로의 출구를 찾아 이동하는 게임

     ![미로찾기](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/a165dcfe-b196-4772-a2fa-f209d8d03fd4)

<details>
 <summary>코드 보기</summary>
 
```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePlayer : MonoBehaviour
{
    public enum eState
    {
        Idle, Walk
    }

    public Animator anim;
    private eState state;
    public float speed = 3f;

    //게임 오버 시 플레이어 멈추기
    public bool isStopped;

    public VariableJoystick joy;

    private void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.state = eState.Idle;
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal") + joy.Horizontal;
        float v = Input.GetAxisRaw("Vertical") + joy.Vertical;
        Vector3 dir = Vector3.Normalize(new Vector3(h, 0, v));

        this.transform.Translate(dir * this.speed * Time.deltaTime, Space.World);

        var angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        if (dir != Vector3.zero)
        {
            this.anim.SetInteger("State", 1);
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

        }
        else
        {
            this.anim.SetInteger("State", 0);
        }

        this.StopMove();    //게임 오버되면 멈춤

    }

    public void StopMove()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (this.isStopped)
        {
            this.speed = 0;
            rb.velocity = Vector3.zero;
        }
    }
}

```
▲ MazePlayer 스크립트

```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMain : MonoBehaviour
{
    public Transform goalPos;
    public MazePlayer player;
    public UIMazeDirector director;

    public float arrivalDistance = 0.5f;

    public float distanceToGoal;

    public GameObject heart;

    private void Awake()
    {
        //게임 방법 설명중 플레이 안되게 멈추기
        Time.timeScale = 0;
    }

    private void Update()
    {
        this.distanceToGoal = Vector3.Distance(player.transform.position, goalPos.position);

        //골인 지점 도착
        if (distanceToGoal <= arrivalDistance)
        {
            Destroy(this.heart);

            this.player.isStopped = true;

            this.director.uiGameClear.gameObject.SetActive(true);
        }
    }
}

```
▲ MazeMain 스크립트

```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMazeDirector : MonoBehaviour
{
    public UIGameOver uiGameOver;
    public UIGameRule uiGameRule;
    public UIMemoryClear uiGameClear;
    public MazePlayer player;
    public Text timerText;
    public float totalTime = 30f; // 시작할 시간(60초)

    public float currentTime;

    void Start()
    {
        currentTime = totalTime;
        UpdateTimerDisplay();
    }
    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            currentTime = 0;
            UpdateTimerDisplay();

            if (this.uiGameOver != null)
            {
                this.uiGameOver.gameObject.SetActive(true);
            }
        }
    }
    private void UpdateTimerDisplay()
    {
        // 시간을 정수로 반올림하여 텍스트에 표시
        timerText.text = Mathf.Ceil(currentTime).ToString();
    }
}

```
▲ UIMazeDirector 스크립트

</details>
&nbsp;

4. 플래피 래빗
   > - 화면을 터치해 장애물을 피하며 하트를 모으는 게임

     ![플래피래빗](https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/96e5972e-79ad-438b-8026-28b7c50b8dbc)

<details>
 <summary>코드 보기</summary>
 
```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    public enum eState
    {
        Idle, Jump
    }

    public Animator anim;
    private eState state;
    private Rigidbody rb;
    public float jumpForce = 5f;
    private bool isAnimating = false;

    public int heartCnt;

    private void Start()
    {
        AudioListener.volume = 5;
        this.anim = this.GetComponent<Animator>();
        this.state = eState.Idle;
        this.rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.anim.SetInteger("State", 1);
            this.state = eState.Jump;
            this.Jump();
            this.isAnimating = true;

            if (isAnimating)
            {
                this.anim.Play("Jump", 0, 0);
            }
        }
        else
        {
            this.anim.SetInteger("State", 0);
            this.state = eState.Idle;
        }

        if(this.gameObject.transform.position.y > 3.9 || this.gameObject.transform.position.y < -3)
        {
            Destroy(this.gameObject);
        }
    }

    public void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;

        this.state = eState.Jump;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            this.heartCnt++;
            Debug.Log(heartCnt);
            SoundManager.PlaySFX("Pop");
            Destroy(collision.gameObject);

        }
    }
}

```
▲ FlappyPlayer 스크립트

```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyMain : MonoBehaviour
{
    public UIFlappyDirector director;

    public GameObject stemPrefab;
    public GameObject flowerPrefab;
    public GameObject heartPrefab;

    private float repeatInterval1 = 3.0f;
    private float repeatInterval2 = 4.0f;
    private float repeatInterval3 = 4.5f;

    private void Awake()
    {
        //게임 방법 설명중 플레이 안되게 멈추기
        Time.timeScale = 0;
    }

    public void Init()
    {
        this.director.Init();
    }

    private void Start()
    {
        //n초마다 장애물 생성
        InvokeRepeating("GenerateStem", 0.0f, repeatInterval1);
        InvokeRepeating("GenerateFlower", 0.0f, repeatInterval2);

        //n초마다 하트 생성
        InvokeRepeating("GenerateHeart", 0.0f, repeatInterval3);
    }

    public void GenerateStem()
    {
        GameObject stem = Instantiate(stemPrefab);
        stem.transform.position = new Vector3(12.01f, Random.Range(4.9f, 5.3f), -0.354388f);
        stem.transform.localScale = new Vector3(Random.Range(1f, 3f), Random.Range(1f, 3f), Random.Range(1f, 3f));

    }

    public void GenerateFlower()
    {
        GameObject flower = Instantiate(flowerPrefab);
        flower.transform.position = new Vector3(12.01f, -2.53f, -0.354388f);
        flower.transform.localScale = new Vector3(Random.Range(8.817859f, 15f), Random.Range(8.817859f, 15f), Random.Range(8.817859f, 15f));
    }
    public void GenerateHeart()
    {
        GameObject heartGo = Instantiate(heartPrefab);
        heartGo.transform.position = new Vector3(12.01f, Random.Range(-0.1f, 3f), -0.354388f);
    }

}

```
▲ FlappyMain 스크립트

```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        this.transform.Translate(this.transform.right * this.speed * Time.deltaTime);

        if(this.gameObject.transform.position.x < -14.5)
        {
            Destroy(this.gameObject);
        }
    }
}

```
▲ Stem 스크립트

```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        this.transform.Translate(this.transform.right * this.speed * Time.deltaTime);

        if (this.gameObject.transform.position.x < -14.5)
        {
            Destroy(this.gameObject);
        }
    }
}

```
▲ Flower 스크립트

```ts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyHeart : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        this.transform.Translate(Vector3.left * this.speed * Time.deltaTime, Space.World);

        if (this.gameObject.transform.position.x < -14.5)
        {
            Destroy(this.gameObject);
        }
    }
}

```
▲ FlappyHeart 스크립트

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

## 🎶BGM

https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/fb2fb06e-e232-43d7-81d0-8b340619cfe3

▲ 타이틀 BGM 자체 제작

https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/cdffae3f-7794-4663-8045-c42f3331d25d

▲ 메인 게임 BGM 자체 제작

https://github.com/gusdk337/RabbitPlanetResult/assets/51481890/4378aacc-a891-450a-a1b6-039d9f874d5c

▲ 메인 게임 BGM 자체 제작
