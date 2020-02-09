# Development Progress
## 20180510
### GroundSpawn.cs 추가
- 변수
  - ground : 그라운드 게임오브젝트. 인스펙터에서 Plane을 넣어주자.
  - maxGrounds : 게임에 그라운드가 존재하는 총 갯수. 메모리 절약 관련
  - groundXSize : 그라운드 프리팹의 x스케일이다.
  - localPosition : 이 게임의 최초 그라운드의 기준점. 현재 (0,0,0)으로 설정되어 있다.
  - spawnPosition : 처음에 생성되어 있는 세 개 Ground이후 ground가 생성되는 위치.

- 메소드
  - Start()함수에서는 groundXSize와 localPosition을 가져오고, spawnPosition은 세 개 이후의 위치이므로 localPosition에 groundXSize * 3 만큼 더해준다.
  - SpawnGround()함수에서는 캐릭터가 하나의 ground를 지날 때마다 ground를 생성한다. 캐릭터에서 참조해서 실행해줌. 생성한 후에는 groundXSize만큼 더해준다.
  - DeleteGround() 추가 예정.

## 20180511
### PlayerController.cs 추가
- 변수
  - lane : 현재 선택된 라인. (-1, 0, 1으로 구분)
  - swipeSpeed : 좌우로 이동하는 속도
  - speed : 앞으로 전진하는 속도 (1부터 300까지)
  - groundCount : ground를 생성하기 위한 변수이다.
  - initialPos : 마우스로 스와이프하기 위한 변수. 클릭했을 때의 위치를 저장한다.

- 메소드
  - Update()에서 Translate 메소드를 이용해 지속적으로 앞으로 이동.
  - 내 위치에서 groundCount를 뺐을 때 0보다 크면 50만큼 차이가 나게 되는 것이다. 이 때 SpawnGround() 메소드를 실행한다. 그 후에 groundCount에 이 값을 저장한다.
  - 마우스를 클릭하고 드래그한 뒤 마우스를 뗐을 때 그 방향을 계산해서(Calculate()메소드 이용) 상하좌우에 해당하는 메소드를 실행한다.
  - 현재 좌우로 이동가능하게 구현해 두었다.

## 20180512
### PlayerController.cs 업데이트
- 변수
  - isKeyPressed : 키보드 좌우 키를 눌렀을 때 눌린 상태인지를 체크하는 플래그 bool 변수이다.

- 메소드
  - Update()문에서 키보드 관련 명령을 추가했다. 지금 빠르게 두 번 누르면 레인 밖으로 나가는 버그가 존재한다.

### SpwanBlocks.cs 추가
#### 변수
- 변수(System)
  - beforeLane : 이전에 생성된 레인의 위치
  - nowLane : 현재 생성될 레인의 위치
  - iterator : 레인을 생성할 때 돌아갈 blocks 배열의 이터레이터
  - deleteIterator : 블럭을 지울 때 돌아갈 이터레이터
  - endSpawn : 하나의 블럭 조합이 끝났는지를 판단하는 플래그
  - passedCount : 생성한 블럭의 총 갯수

- 변수(Block)
  - block : 생성할 블럭의 프리팹
  - blockXSize : 블럭 스케일의 x값
  - maxBlocks : 생성할 수 있는 블럭의 총 갯수
  - lane : 모든 블럭을 담아둘 수 있는 GameObject 배열
  - genSpeed : 블럭이 나오는 속도

- 변수(Difficulty)
  - difficulty : 5에서부터 15까지의 랜덤 값. 어려울수록 블럭이 짧아진다.
  - beforeDifficulty : 이전의 difficulty값
  - space : 큰 블럭들 사이 간격

#### 메소드
- SpawnBlock()
  - Compute()메소드를 호출해서 어떤 레인에서 생성시킬 것인지 정한다.
  - difficulty값을 설정해서 그만큼 블럭을 생성한다.

- ComputeLane() : 어떤 레인으로 갈 것인지 정한다. 이전에 생성한 것에서 다른 쪽으로 생성되게 한다.

- InitBlock() : 블럭을 미리 Instantiate 해둔다. (SingleTon 패턴과 연관)

- DeleteBlock() : 지나간 블럭을 삭제한다. 현재는 속도에 반비례하는만큼 WaitForSeconds()메소드를 이용해 삭제하도록 했지만, 속도는 변화가 너무 많은 변수이므로 거리에 따른 삭제를 하는 것이 나을 듯.



로봇팔 완성, 부스트 방울 내려가는거, 카메라 버그, 스페이스 넓히기, 난이도 줄이기, 

bgm, 스프린트, 부스트, 니트로, 충돌, 기어차징, 기어업, 기어다운, 기어풀림, 공격, 상자부서짐, 마인터짐,

## 20180606
### 컨셉 변경점
 - life : 게이지 형식으로 바꾼다 (ex. hp : 100)
 - life는 지속적으로 감소, 부딪힐때 감소, 정해진 위치에 나오는 hp아이템으로 회복(이어하기는 50% 가량 회복시켜줌)
 - 부딪히는거는 스테이지가 진행될수록 더 아파지게.
 - shield : 지속시간 추가 (3~5)
 - Player 캐릭터 추가
 - 상점 시스템 추가
 - 기어 시스템 삭제
 - 점수는 거리로만 판별(빠르게 플레이하는 유저는 life에서 이점이 있음)
 - 랜덤으로 나오는 아이템에 life 제거(life는 고정위치, 추후 mine 도 고려)
 - 캐릭터별 스테이터스(업그레이드 요소), 캐릭터별 상한제한도 고려
 - 최소속도 점점 증가하게(난이도 조절)

### 버그 수정
 - Debug 모드 수정방안 : 이동가능한 제한 조건을 lane 변수 기준으로 한다.

### 업데이트 방향
 - 상점 세분화, 스테이지 추가, 캐릭터 추가
 - max node 길이 축소해서 block 풀의 개수를 줄임.