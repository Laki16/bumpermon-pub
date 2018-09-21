using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipDisplay : MonoBehaviour {

    public Text itemTitle;
    public Text itemDesc;

    public void Start()
    {
        itemTitle.text = string.Empty;
        itemDesc.text = string.Empty;
    }

    public void UpdateUI(int EquipIndex)
    {
        //설명 적기, 등급에 따라 배경색 업데이트
        switch (EquipIndex)
        {
            ////////////////////////////////NORMAL/////////////////////////////////////
            case 1000: //헬멧(N)
                itemTitle.text = "헬멧";
                itemDesc.text = "어느 경우에든지 달릴 때는\n헬멧이 필요한 법이죠.";
                break;
            case 1001: //솜사탕(N)
                itemTitle.text = "솜사탕";
                itemDesc.text = "달콤한 솜사탕이네요.\n놀이공원에서 뛰는 느낌이 들어요.";
                break;
            case 1002: //아메리카노(N)
                itemTitle.text = "아메리카노";
                itemDesc.text = "시원한 아메리카노와 함께라면\n더 멀리 갈 수 있을까요?";
                break;
            case 1003: //항아리(N)
                itemTitle.text = "항아리";
                itemDesc.text = "망치와 함께 쓴다면...\n끝없이 올라가야 할지도 모릅니다.";
                break;
            case 1004: //양말(N)
                itemTitle.text = "양말";
                itemDesc.text = "산타의 선물을 받으려면\n머리맡에 양말을 놔둬야겠죠?";
                break;
            case 1005: //배낭(N)
                itemTitle.text = "배낭";
                itemDesc.text = "여행을 떠날 때 든든한 조력자가 되줄\n겁니다.";
                break;
            case 1006: //래쉬가드(N)
                itemTitle.text = "래쉬가드";
                itemDesc.text = "여기는 워터파크가 아닌데 말입니다.";
                break;
            case 1007: //선인장(N)
                itemTitle.text = "선인장";
                itemDesc.text = "조심히 들고 뛰세요!\n가시가 날카로워요.";
                break;
            case 1008: //운동화(N)
                itemTitle.text = "운동화";
                itemDesc.text = "나이카 운동화네요!\n더 많이 뛸 수 있을 거에요.";
                break;
            case 1009: //하이힐(N)
                itemTitle.text = "하이힐";
                itemDesc.text = "균형 잡기는 힘들지만,\n폭발력이 있습니다.";
                break;
            case 1010: //돌 조각(N)
                itemTitle.text = "돌 조각";
                itemDesc.text = "골렘에서 떨어져나온 돌 조각입니다.";
                break;
            case 1011: //화분(N)
                itemTitle.text = "화분";
                itemDesc.text = "아이 엠 그루트";
                break;
            case 1012: //소화기(N)
                itemTitle.text = "소화기";
                itemDesc.text = "빨리 달리면 불이 붙을 수도 있어요!\n그럴 때 유용하답니다.";
                break;

            ////////////////////////////////RARE/////////////////////////////////////
            case 1100: //덤벨(R)
                itemTitle.text = "덤벨";
                itemDesc.text = "이 무거운걸 들고 뛰신다구요?\n떨어트리지 않게 조심하세요.";
                break;
            case 1101: //티셔츠(R)
                itemTitle.text = "개발자의 티셔츠";
                itemDesc.text = "낡았지만 편안해 보여요.";
                break;
            case 1102: //나뭇잎(R)
                itemTitle.text = "나뭇잎";
                itemDesc.text = "나뭇잎이 쓸모가 없다구요?\n작은 것이 강할 때가 있답니다.";
                break;
            case 1103: //갑옷(R)
                itemTitle.text = "갑옷";
                itemDesc.text = "좋은 재질로 만들어진 걸까요?\n가볍고 튼튼해 보이는 갑옷입니다.";
                break;
            case 1104: //축구공(R)
                itemTitle.text = "축구공";
                itemDesc.text = "저도 공 차는거 정말 좋아하는데요\n제가 한번 차보겠습니다.";
                break;
            case 1105: //까마귀(R)
                itemTitle.text = "까마귀";
                itemDesc.text = "까악 까악\n반짝반짝한거 좋아.";
                break;
            case 1106: //안전모(R)
                itemTitle.text = "안전모";
                itemDesc.text = "안전모가 필요할지도 모릅니다.\n상자가 떨어질 수도 있으니까요!";
                break;
            case 1107: //검(R)
                itemTitle.text = "검";
                itemDesc.text = "아직 탈출하지 못한 인형들을 위해\n박스를 베려고 하시는 당신은...";
                break;
            case 1108: //틀니(R)
                itemTitle.text = "틀니";
                itemDesc.text = "이를 악물고 뛰는데에는\n틀니가 제격이죠!";
                break;

            ////////////////////////////////EPIC/////////////////////////////////////
            case 1200: //부적(E)
                itemTitle.text = "부적";
                itemDesc.text = "간혹 유령 인형이 출몰한다는 소문이\n있습니다.\n이 부적을 지니고 다니세요!";
                break;
            case 1201: //드레스(E)
                itemTitle.text = "드레스";
                itemDesc.text = "아주 아름다운 장식들로 가득합니다.\n하지만 여기 친구들에게는 영..";
                break;
            case 1202: //오리발(E)
                itemTitle.text = "오리발";
                itemDesc.text = "오리발을 신고 뛰어보셨나요?\n아주 빠르다고 합니다.";
                break;
            case 1203: //망치(E)
                itemTitle.text = "망치";
                itemDesc.text = "항아리와 같이 사용하면...\n높은 곳으로 갈 수 있을까요?";
                break;
            case 1204: //선물상자(E)
                itemTitle.text = "선물상자";
                itemDesc.text = "산타가 준 선물상자로군요!\n어떤 스탯이 오를지 궁금한데요?";
                break;

            ////////////////////////////////LEGEND/////////////////////////////////////
            case 1300: //모래시계(L)
                itemTitle.text = "모래시계";
                itemDesc.text = "새로운 도전을 위해 모래시계를\n뒤집어보세요.";
                break;
            case 1301: //산삼(L)
                itemTitle.text = "산삼";
                itemDesc.text = "앗, 귀한 걸 발견하셨네요!\n이 산삼은 말하자면..\n지리산에서 발견된 100년 된..";
                break;
            case 1302: //왕관(L)
                itemTitle.text = "왕관";
                itemDesc.text = "컨테이너 벨트 끝에는 궁전이\n있다고 합니다.\n그곳에 사는 왕이 쓰는 걸까요?";
                break;

            ////////////////////////////////SPECIAL/////////////////////////////////////
            case 1037: //골렘(S)
                break;
            case 1038: //스켈레톤(S)
                break;
            case 1039: //산타(S)
                break;
            case 1040: //고스트(S)
                break;
            default: Debug.Log("unassigned equipment");
                break;
        }
    }
}