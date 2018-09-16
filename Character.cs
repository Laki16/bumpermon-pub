using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour{
    
    public static int maxHP = 200;
    public static int maxSPD = 120;
    public static int maxDEF = 45;
    public static int maxSTR = 110;
    public static int maxLUK = 70;

    [Header("Status")]
    public int MonsterIndex;
    public int Level;
    public float HP;
    public float SPD;
    public float DEF;
    public float STR;
    public float LUK;
    public float nitroEarnSize;
    public float bombSize;
    public float nitroTime;

    [HideInInspector]
    public int[] golemGold = { 10, 50, 100, 300, 500, 1000, 2000, 3500, 5000};
    [HideInInspector]
    public int[] ghostGold = { 2500, 200, 500, 1000, 2500, 5000, 10000, 20000};
    [HideInInspector]
    public int[] dragonGold = { 6000, 1000, 5000, 10000, 20000, 30000};
    [HideInInspector]
    public int[] santaGold = { 1000, 400, 1200, 1700, 2500, 3900, 5500, 10000};
    [HideInInspector]
    public int[] skeletonGold = { 400, 70, 200, 450, 900, 1500, 2700, 4000, 4500, 7000};

    public void CloudLoadCharacter()
    {
        int golem, ghost, santa, skeleton;
        golem = CloudVariables.SystemValues[3];
        ghost = CloudVariables.SystemValues[4];
        santa = CloudVariables.SystemValues[5];
        skeleton = CloudVariables.SystemValues[6];

        if (golem == 0) {
            PlayerPrefs.SetInt("GolemLevel", 1);
        }
        else
        {
            PlayerPrefs.SetInt("GolemLevel", golem);
        }
        PlayerPrefs.SetInt("GhostLevel", ghost);
        PlayerPrefs.SetInt("SantaLevel", santa);
        PlayerPrefs.SetInt("SkeletonLevel", skeleton);
    }

    public void CloudSaveCharacter()
    {
        CloudVariables.SystemValues[3] = PlayerPrefs.GetInt("GolemLevel");
        CloudVariables.SystemValues[4] = PlayerPrefs.GetInt("GhostLevel");
        CloudVariables.SystemValues[5] = PlayerPrefs.GetInt("SantaLevel");
        CloudVariables.SystemValues[6] = PlayerPrefs.GetInt("SkeletonLevel");

        PlayGamesScript.Instance.SaveData();
    }

    //public void InitStatus(){
    //    PlayerPrefs.SetInt("GolemLevel", 1);
    //    PlayerPrefs.SetInt("GhostLevel", 0);
    //    PlayerPrefs.SetInt("DragonLevel", 0);
    //    PlayerPrefs.SetInt("SantaLevel", 0);
    //    PlayerPrefs.SetInt("SkeletonLevel", 0);
    //}

    public void SetStatus(){
        switch(MonsterIndex){
            case 1: //Golem
                if(!PlayerPrefs.HasKey("GolemLevel")){
                    PlayerPrefs.SetInt("GolemLevel", 1);
                }
                Level = PlayerPrefs.GetInt("GolemLevel");
                HP = 50 + Level * 2.5f;
                SPD = 20 + Level * 2;
                DEF = 20 + Level * 1;
                STR = 20 + Level * 5;
                LUK = 2 + Level * .3f;
                nitroEarnSize = 8 + Level * 0.5f;
                bombSize = 15 + Level * 0.3f;
                break;
            case 2: //Ghost
                if (!PlayerPrefs.HasKey("GhostLevel")){
                    PlayerPrefs.SetInt("GhostLevel", 0);
                }
                Level = PlayerPrefs.GetInt("GhostLevel");
                HP = 30 + Level * 5;
                SPD = 35 + Level * 2.5f;
                DEF = 10 + Level * 1;
                STR = 10 + Level * 1;
                LUK = 15 + Level * 3;
                nitroEarnSize = 12 + Level * 1;
                bombSize = 5 + Level * 1.2f;
                break;
            case 3: //Dragon
                if (!PlayerPrefs.HasKey("DragonLevel")){
                    PlayerPrefs.SetInt("DragonLevel", 0);
                }
                Level = PlayerPrefs.GetInt("DragonLevel");
                HP = 100 + Level * 20;
                SPD = 70 + Level * 10;
                DEF = 20 + Level * 5;
                STR = 60 + Level * 8;
                LUK = 20 + Level * 3;
                break;
            case 4: //Santa
                if (!PlayerPrefs.HasKey("SantaLevel"))
                {
                    PlayerPrefs.SetInt("SantaLevel", 0);
                }
                Level = PlayerPrefs.GetInt("SantaLevel");
                HP = 40 + Level * 4;
                SPD = 30 + Level * 2.5f;
                DEF = 5 + Level * 2;
                STR = 5 + Level * 1;
                LUK = 20 + Level * 1.5f;
                nitroEarnSize = 7 + Level * 0.5f;
                bombSize = 10 + Level * 1;
                break;
            case 5: //Skeleton
                if (!PlayerPrefs.HasKey("SkeletonLevel"))
                {
                    PlayerPrefs.SetInt("SkeletonLevel", 0);
                }
                Level = PlayerPrefs.GetInt("SkeletonLevel");
                HP = 50 + Level * 5.5f;
                SPD = 30 + Level * 3;
                DEF = 10 + Level * 2;
                STR = 7 + Level * 2;
                LUK = 5 + Level * 1.5f;
                nitroEarnSize = 12 + Level * 0.4f;
                bombSize = 8 + Level * 1.3f;
                break;
            default:
                Debug.Log("Invalid character type");
                break;
        }
    }

    public int GetRequireGold(int index, int level){
        switch(index){
            case 1: return golemGold[level-1];
            case 2: return ghostGold[level];
            case 3: return dragonGold[level];
            case 4: return santaGold[level];
            case 5: return skeletonGold[level];
            default: return 0;
        }
    }

    public void LevelUp(){
        int curLevel = 0;
        switch(MonsterIndex){
            case 1:
                curLevel = PlayerPrefs.GetInt("GolemLevel");
                curLevel++;
                PlayerPrefs.SetInt("GolemLevel", curLevel);
                break;
            case 2:
                curLevel = PlayerPrefs.GetInt("GhostLevel");
                curLevel++;
                PlayerPrefs.SetInt("GhostLevel", curLevel);
                break;
            case 3:
                curLevel = PlayerPrefs.GetInt("DragonLevel");
                curLevel++;
                PlayerPrefs.SetInt("DragonLevel", curLevel);
                break;
            case 4:
                curLevel = PlayerPrefs.GetInt("SantaLevel");
                curLevel++;
                PlayerPrefs.SetInt("SantaLevel", curLevel);
                break;
            case 5:
                curLevel = PlayerPrefs.GetInt("SkeletonLevel");
                curLevel++;
                PlayerPrefs.SetInt("SkeletonLevel", curLevel);
                break;
        }

        PlayerPrefs.Save();
        //cloud saving
        //PlayGamesScript.Instance.SaveData();
        CloudSaveCharacter();
    }
}
