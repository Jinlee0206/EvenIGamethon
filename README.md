# EvenIGamethon

## 주의사항
1. Push 이전에 항상 **pull** 할것 있는지 확인하기
2. **Scene 작업** 전에는 항상 Scene 관리자에게 물어보고 작업하기
3. 작업 내용의 **주석**은 최대한 상세히
4. **상속 구조로 이루어진 스크립트**는 기존의 변수명 수정은 최대한 삼가고 불가피하게 작업을 해야하는 경우는 팀원들에게 미리 알리고 주석을 상세히 달기
5. 개발 버전 업데이트 규칙
   - 최신 업데이트를 **최상단**에 배치

|V.1.0.0  | 1             | 0            |  0           | JS      | 2024-01-08 |
|:-------:|:-------------:|:------------:|:------------:|:-------:|:-------:|
|   의미  | 개발일정 페이즈 | 씬수정       | 스크립트 작업 |작업자    |  날짜  |

6. 개발 일정 페이즈  

|구간   |    Phase1    |     Phase2   |      Phase3   |     Phase4    |
|:----:|:------------:|:------------:|:-------------:|:-------------:|
|기간| 01.08 ~ 01.26 | 01.27 ~ 02.02 | 02.03 ~ 02.09 | 02.10 ~ 02.23 |
|개요| 게임 구현 | 서버 구현(구글플레이 제외) | 서버 구현(구글플레이 포함) | 최종 QA, 버그 수정, 추가적인 시스템 구현 및 개발 최적화| 
---
## V.1.2.0 - JS 2024-01-19
- SceneManager 구축으로 인한 개발 버전 업데이트
- Image Upload
  - 시드 (해바라기 씨)
  - 로고, 배경이미지
  - 타이머 (인게임 타이머 이미지) 

- Lobby
 - 로비 씬 UI 작업 완료
 - <p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/105345909/114fa4ac-c6cc-415b-a588-f2e8f5ec941c" width = "150" height = "250">   

 - 배경은 Grid로 작업할 예정

- PopUpManager
  - PopUpManager 시스템 구축
    - PopUpManger.cs : 스태틱 프로퍼티로 프리팹으로 생성된 PopUp들을 [Canvas] - [PopUps] 아래 생성하게 하고 차례로 Close 할 수 있게 구현
    - PopUpWindow.cs : Stack 기반으로 PopUpWindow가 열림
    - PopUpNames.cs : PopUp 이름을 관리하는 MonoBehaviour를 상속 받지않는 단일 클래스. PopUpManager에서 생성 및 초기화를 함
    - PopUpHandler.cs : 각 버튼에 대한 동작함수(PopUp)를 정리

    > PopUpManager를 활용하여 직관적이고 가독성 높은 코드를 작성함.  

    > 구현 중에 유지보수하기 쉬운 코드로의 리팩터링을 해나가며 작업한 것이 큰 도움이 됨.  

  - Settings, ExplainStamina, ExplainCorn 작업 완료
    - <p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/105345909/0b02e72c-d263-40c1-8def-10e0434c1031" width = "150" height = "250"> 
    - <p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/105345909/a73f2a6c-9b98-4c5b-9233-d13f50a96ded" width = "150" height = "250"> 
    - <p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/105345909/9b9a437c-6a8d-4d45-8328-cdf15533eb56" width = "150" height = "250">
    
- DragPlayer
  - IPointerClickHandler 인터페이스 활용
    - 클릭시 Profile이 PopUp 되게 구현
    - 추가적으로 IDragHandler, IEndDragHandler를 활용한 Dragable Object도 활용 가능
    - 개발PM님과 미팅 완료하였고 추후 애니메이션 추가되면 활용 가능성 있음

- Scene
 - SceneManager 시스템 구축
   - SceneLoader를 싱글톤으로 구현
   - Loading 바 차는 것을 시각적으로 보여주기 위해 SmoothDamp 이용해서 Debug용 딜레이 추가
   - <p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/105345909/30c72f99-b5c0-4a18-ba49-1358908e604f" width = "150" height = "250">

- 해야할 일 :  AchieveManager
  - 해금 조건 달성 매니저 초기 세팅 완료
  - 이차원 배열안에 해금 카드들의 GameObject를 다 담아 놓고, 해금 카드를 선택했을 시 SetActive하는 방식으로 구현 예정 
  - 1차 프로토 타입 Build Test를 위해 UI 작업을 우선 진행하였고, 21일 이후에 다시 Card 뽑기 로직 작업 진행 예정

---
## V.1.1.9 - SM 2024-01-19
- 붐바르다 최종 구현
    - 최종 구동 확인 -> 범위 딜 정상 작동
    - 기획안 수정 -> 충돌시 데미지 입힌 후 폭발 추가 데미지 -> 폭탄을 던지듯이 그냥 폭발 데미지만
    - 관련 데이터 Xml에 추가
    - <p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/109404269/a5c3b1ff-9770-48d2-95a9-fc66f852f9a6" width = "130" height = "210">   
- 루모스 구현 준비
---

## V.1.1.8 - JS 2024-01-18
- LevelUp.cs 로직 구현
  - 공통 적용 부분 구현 완료
    - Damage, CoolTime 등
    - 해금 시스템 구현 필요...
- Lobby_Battle UI 구현 완료
  - Chapter, Stage 별 버튼 스크립트화 완료
  - StageSelect를 기준으로 chapter, stage spwaner에서 사용하고 있기 때문에, 다시 Lobby_Battle 씬이 로드 되어도 이전 정보 저장할 수 있게 코딩 완료
  - <p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/105345909/b94acab3-22ae-4f5c-8863-ccb5efe97a5b" width = "130" height = "210">   
---

## V.1.1.7 - SM 2024-01-18
- 챕터별 커지는 몬스터들 배율 적용
- 스킬 로직 준비
    - 확정난 이펙트로 매직볼, 봄바르다, 아구아멘티 애니메이션 작성
- 보스전시 특수효과
    - 각 챕터 스테이지 5, 웨이브 10에대한 예외처리
    - 빨간빛 깜빡거리는 특수효과 + 보스몹 스폰 함수 별도 마련 완료
- 누락된 몹 애니메이션 추가
    - 2-1~5
- 각 스킬 애니메이션 상속 구조 구현
- 봄바르다 -> 피격시 폭발 애니메이션 나오게 구현 -> 애니메이션 종료후 비활성화
    - 폭발 애니메이션시 데미지 들어가는 로직 추가 필요
- 몬스터 피격 OnTriggerEnter와 TakeHit 함수 분리 -> 스킬 구현에서 사용하기 위해서
- *봄바르다 관련 이슈*
    - 딜레이가 들어가면 데미지가 안들어가고, 딜레이를 없애면 데미지가 잘 들어가는 현상 -> 아직 해결 실패
---
## V.1.1.6 - JS 2024-01-18
- LevelUp.cs 로직 구현
  - 카드 3가지 뽑는 로직 구현
  - ScriptableObject 스킬별로 분류
      - 행은 열거형 Type으로, 열은 CardId로 구분

- 해야할 일
  - 해금 로직 구현
  - 폭발, 지속시간 증가 로직 구현 연동 필요 (구현이 되면)

---
## V.1.1.5 - SM 2024-01-17
- 스테이지별 몬스터 스탯 배율 적용
- 메인 햄찌 적용
- 각 스킬 틀 제작 
    - 실제 작동 로직은 아마 내일, 에셋 확정
- 몬스터 피격시 깜빡깜빡거리도록 시각 효과 추가
---
## V.1.1.4 - SM 2024-01-16
- 레벨 디자인 기획서대로 전부 Xml화
- 스테이지, 챕터 선택창 테스트용 제작
<p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/109404269/073021c1-fcc7-4eca-baf5-547a27572e1a" width = "200" height = "320">

- xml 불러와서 각 웨이브별 소환될 몬스터들을 저장
    - 저장 후 셔플, 섞인대로 소환
    - 프리팹 하나로 모든것을 통제하기 위한 코딩
- 몹, 플레이어, 웨이브 데이터 모두 삽입
    - 정상 작동
- 몬스터 피격시 데미지 팝업이 뜨도록 설정
<p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/109404269/88022886-73a0-4b7e-b4ff-23f68bd6c2a3" width = "200" height = "320">

##### 내일 할 일
- Idle 애니메이션 제작 후 벽에 도달시 Idle, Attack 둘만 나오도록
- 스킬별 폭발 등 로직 구현
- 메인햄찌 적용, 애니메이션 코드 구현
---
## V.1.1.3 - JS 2024-01-15
- Card.cs 추가 작업
  - Card UI 리팩토링
    - Card 스크립트에서 Player 스크립트 바로 참조해서, PlayerData의 배열에 접근하여 데미지를 증가 시키는 방식 - 구현 완료
      - 추가적으로 기본 공격으로 나가지 않는 2,3,4,5,6,7,8에 해당하는 스킬들을 해금하는 방법에 대해 수민님과 논의가 필요함
      - 데미지 증가, 관통, 쿨타임 감소는 적용이 간단 하므로 즉시 적용 가능.
      - 폭발 적용, 폭발 데미지 증가, 공격 범위 증가, 스킬 지속시간에 대한 구현 방법에 대한 논의 필요
    - 현재 구현 된 방식으로는 한 종류의 카드가 무한히 뜨게 할 수는 없으니 상한선 기획 필요

  - 창 컨트롤 완료
    - 레벨업 시 Card UI 뜨게 설정

  - 시간 컨트롤 완료
    - GameManager에 Stop(), Resume() 함수 만들어서 게임 시간에 접근할 수 있게 함수 추가

  - 랜덤 아이템 디자인
    - 미리 만들어 둔 스크립터블 오브젝트를 랜덤하게 뜨게 하는 로직 설계 필요
    - 해금이 우선이 되어야 하기 때문에 레벨은 살리는 방향으로 설계 할 예정

<p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/105345909/2dcddee9-2418-4ad1-b8a8-67dfa25db668" width = "350" height = "200">   

- StageClear UI
  - StageClearUI 제작 완료
  - Reward, Stage Clear 랭크, 애니메이션 제작 필요

<p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/105345909/f83016d8-0e6c-4831-a50d-8ba60cf75857" width = "150" height = "180">

- UI Font 변경
  - 현재 UI 폰트 한글 지원 안됨 : 변경 예정

- 특이사항
  - 파라미터 에러 발생 _unity_self
  - 간헐적으로 발생하는 것으로 확인되고, 유니티 버그 인 것 같기도... 유니티 재시작 시 안나는 것 확인했는 데, 지속적으로 나면 Issue Tracking 필요  

<p align ="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/105345909/60afc6fe-d8f7-4384-9dcb-6140e1860570" width = "350" height = "170">

---

## V.1.1.2 - JS 2024-01-14
- Card, CardData script 작성
  - [일반 몬스터] - [메인 캐릭터 스킬]에 해당 하는 부분
  - ScriptableObject를 이용해 스킬 종류에 따라 구분하고, 스킬 카드 종류에 따라 PlayerData의 값을 변화 시키는 로직으로 설계
    - PlayerData와 Card 스크립트 간의 연결을 어떻게 해야할 지 고민이 필요...
- UI Font 변경
  - ThaleahFat Legacy Font 사용
---

## V.1.1.1 - SM 2024-01-14
- 스킬에 따른 쿨타임 로직 개선
- 적 벽에 도달후 움직이는 버그 개선
- 스테이지별 몹 리젠 수 적용(기획서대로)
- 게임 승리 로직
    - 현재 스테이지에 나오는 총 몹의 수를 받아서, 몬스터가 죽을 때마다 --, 0이 되면 승리 로직 시작
- 확정된 에셋 애니메이션 제작
- 확정된 에셋 애니메이션 상속구조 생성, 몬스터에 적용
    - 내일 할 일 -> Idle 모션 만들기, 상속구조 수정
<p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/109404269/dc71ef49-5f08-4f8e-b83e-3ae3d69e813f" width = "200" height = "320">

---
## V.1.1.0 - JS 2024-01-12
- 해상도 변경
  - 1080 * 1920 (최종 확정)
  - 해상도 변경에 따른 UI 변경 작업 진행
- Battle_Proto Scene에 UI_Proto Scene 병합 작업 진행
  - 프로토 타입 개발을 위한 간단한 업무이기 때문에 한 씬에서 작업을 하는 것이 작업 속도 향상에 유리하다고 판단함  
   <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/105345909/18094dc4-7835-4124-b3a9-988baa6272b8" width = "200" height = "320">
  
- Exp, Level, Time Data 업데이트 동기화
  - Hp 동기화는 수민님과 코드리뷰 이후 명일 작성 예정
- 수진님 스킬 규칙 설명 기획서 확인
  - 개발 측면 피드백을 위해 2024-01-13 오후 시간대 미팅 예정

---
## V.1.0.4 - SM 2024-01-12
- 자동공격 로직 작성
- 스킬에 따른 다른 쿨타임 독자적으로 돌아가도록 작성
- 몬스터 Xml 데이터 파싱 적용
- 벽 체력 설정
	- 벽 피격 로직
---
## V.1.0.3 - SM 2024-01-11
#### Battle_Proto 씬 작업
- 총알 오브젝트 풀링
    - 몬스터의 오브젝트 풀링과 같은 방식으로 작동
- 플레이어 총알 발사 로직
    - ##### 오류 수정 사항
    - 발사 시 발사 방향으로 회전된 스프라이트가 나오도록 처리
    - 총알이 날아가는 도중에 적이 죽어버리는 경우 처리
    - 가장 가까운 적을 찾는 함수가 비활성화된 객체들까지 포함해서 계산하는 오류 처리
- 몬스터 피격, 사망 로직
    - 몬스터 피격시 데미지를 받고, 사망하도록(SetActice(false))
- XML 파싱 틀 제작
    - 현재는 메인 캐릭터의 데이터가 파싱되도록 처리됨
    - 추후 몬스터쪽에도 적용할 예정
<p align="center"> <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/109404269/7fae7317-bc98-45bc-95ce-b5580492e40a" width = "180" height = "320">
  
---
## V.1.0.2 - JS 2024-01-11
- 싱글톤 제네릭 업데이트
  - 재생산성을 높이기 위해 제네릭 타입으로 싱글톤 스크립트 작성완료, 게임 매니저에 적용
- UI 작업
  - UIManager 스크립트 작성
    - GameManager 호출 이후 UIManager를 생성하고, GameManager와 커플링 없이 UI 업무는 UIManager에서 독립적으로 실행할 수 있게 구현 예정
  - CardUI
    - 특정 조건(레벨업)이 되었을 때, CardUI가 오픈되고 게임은 일시 정지된다.
    - 세가지 선택지 중에 하나를 선택하였을 때, 해당 카드의 효과를 스테이지 상에 즉각 업데이트하며(미구현) 게임 플레이가 재개된다.
    - Card 선택 메소드는 따로 로직 작성할 수 있게 스크립트 분할 작업
  - GameSpeedUI
    - 기본 배속, 1.5배속 버튼 형식으로 추가, 후에 Asset 정해지면 이미지 토글 형식으로 대체 예정
  - PauseUI
    - Home
    - Resume
    - Retry
---
## V.1.0.1 - SM 2024-01-10
#### Battle_Proto 씬 작업
- 적 오브젝트 풀링 틀 작성
    - 적 객체가 사망 시 Destroy()가 아닌 SetActive(false) 시켜서 씬에 남아있게 함
    - 다음 적이 생성될 때 현재 비활성화된 객체가 있는 경우 해당 객체를 재활용해서 생성
    - 없는 경우에는 새로 생성
    - 적은 프리팹 하나로 구현
        - RuntimeAnimatorController만 바꾸면 외형도 바뀌게 작업
        - 몬스터 능력치는 SpawnData로 삽입됨 -> 추후 Xml로 삽입하도록 변경 예정
    - PoolManager에는 위에서 적은 프리팹 정보가 들어있음
    - Spawner에는 SpawnData 배열로 능력치가 저장되어있음
    - 해당 정보들을 불러와서 몬스터를 생성하는 방식
- 적 움직임 작성
    - 적 움직임은 Y축으로 내려오는 움직임만 필요하므로 해당 부분 작성
- 각 웨이브 시간마다 다음 웨이브가 몰려오도록 작성
<p align="center"> <img src = "https://github.com/Jinlee0206/Jinlee0206.github.io/assets/105345909/ae6f4c1b-b2de-4e65-a4fb-89fe67223a1a" width = "180" height = "320">
  
---
## V.1.0.0 - JS 2024-01-10
- 전투 기획 초안
  <details>
  <summary> 접기/펼치기 </summary>  
  <img src = "https://github.com/Jinlee0206/EvenIGamethon/assets/109404269/f2f72556-ab26-4a0d-860c-51dd179601a8" width = "420" height = "930">
 </details>

- 해상도 조절  
  - 9 * 16 모바일 비율 임시 설정 (택)
  - 9 * 19 플립 같은 종횡비가 큰 비율

- UI 작업
  - Title UI
    - 임시 타이틀로 간단하게 배경과 이미지, 타이틀, 그리고 시작 문구로 간단 제장  
   <img src = "https://github.com/Jinlee0206/Jinlee0206.github.io/assets/105345909/a1f63735-e03f-4134-b89e-c71f951d7c5e" width = "180" height = "320">

  - Stage UI
    - ScoreUI
      - 획득 점수 / 플레이 속도 조절 버튼 (임시)
        - 점수는 Int 형으로 Text 받는 식으로 GameManager 구축 후 추후 연동
        - 플레이 속도 조절은 버튼으로 구현할 예정 => 버튼을 누를 때마다 이미지 변경되게 구현 예정 
      - Wave  
        - 현재 웨이브와 해당 스테이지 총 웨이브 표기
      - Timer
        - 해당 스테이지 시작부터 타이머 쭉 흘러가게 설정
      - Exp Bar
        - 슬라이더로 제작
        - 왼쪽에서 오른쪽으로 채워지는 형식으로 구현
        - 100% 채워질 시 카드 오픈 UI 뜨게 구현 예정
        - 현재 레벨을 알 수 있게 레벨 표시도 진행하게 기획 수정 요청 필요
    - DefenseUI
      - SpawnPoint
        - 중앙에 위치한 스폰 포인트는 기본 햄찌가 소환될 곳 (default)
        - 양 옆에 서브 햄찌가 소환될 공간을 미리 만들어두고 클릭해서 건설할 수 있는 타워 디펜스 형식의 구현 예정  
       
    - AttackUI
      - 해금되는 기본 공격 스킬 탭
      - 쿨타임이 돌면 자동으로 공격이 실행되고 쿨타임 아이콘 UI 보여질 예정
     <p align="center"> <img src = "https://github.com/Jinlee0206/Jinlee0206.github.io/assets/105345909/87342756-c30a-4d58-b753-39cf4a6e4f3e" width = "40" height = "40">

    - 최종 결과   
    <p align="center"> <img src = "https://github.com/Jinlee0206/Jinlee0206.github.io/assets/105345909/6caae26b-e6ab-4e0d-a905-7df65e6b70b9" width = "180" height = "320">
    
---

### V.0.0.0 - JS 2024-01-08
- 개발 초기 셋업
  - Unity Version matching : V22.3.4.f1  
  - Asset Serialization Mode : Force Text  
  - Git Repository set up : 개인 레포지토리에 개설, public으로 사용 예정  
  - Git LFS installed and checked -> success  
  - Test Project 생성  
    - 개인 Layout 생성

- Git, Github 비개발자 직군 간단 세미나

- Asset Searching
  - 상위 기획 자료 미팅 (2024.01.08 22:00 예정) 이후 방향성 확인 이후 어울릴 만한 Asset search
   
- 공동 작업용 변수명 통일 기준 확립
  - [C# 변수 명명법](https://jinlee0206.github.io/develop/Naming.html)
  - 폴더 분류 기준 확립

- 개발자 간 개발 작업 분업화 미팅
  - 대주제 : 프로토 타입단계에서 Scene 담당, 수학 로직, 이펙트, UI, ... , etc.
