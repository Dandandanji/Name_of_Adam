using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 필드 위에 올려진 캐릭터의 스크립트
// 스킬 사용, 이동, 데미지와 사망판정을 처리

public class Character : MonoBehaviour
{
    SpriteRenderer SR;
    [SerializeField] public CharacterSO characterSO;

    Tile[,] Tiles;

    [SerializeField] float MaxHP, CurHP;
    #region Loc X, Y
    [SerializeField] int locX, locY;
    public int LocX => locX;
    public int LocY => locY;
    #endregion

    public Vector2 SelectTile;


    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        Tiles = GameManager.Instance.BattleMNG.BattleField.TileArray;

        Init();
        SetLotate();
    }

    // 인스펙터에서 위치 이동시키기 위해 임시로 배치
    private void Update()
    {
        SetLotate();
    }

    // 캐릭터 초기화
    void Init()
    {
        GameManager.Instance.DataMNG.BCL_CharEnter(GetComponent<Character>());

        // sprite를 배치했다면 변경하기
        if (characterSO.sprite != null)
            SR.sprite = characterSO.sprite;

        // 적군일 경우 x축 뒤집기
        SR.flipX = (characterSO.team == Team.Enemy) ? true : false;
        MaxHP = CurHP = characterSO.stat.HP;
    }

    // 스킬 사용
    public void use()
    {
        characterSO.use(GetComponent<Character>());
    }

    #region Character Move

    //오브젝트 생성 이전, 최초 위치 설정
    public void setLocate(int x, int y) {
        locX = x;
        locY = y;
    }

    // 이동 경로를 받아와 이동시킨다
    public void MoveLotate(int x, int y)
    {
        Tiles[LocY, LocX].ExitTile();

        int dumpX = locX;
        int dumpY = locY;

        // 타일 범위를 벗어난 이동이면 이동하지 않음
        if(0 <= locX + x && locX + x < 8)
            dumpX += x;
        if (0 <= locY + y && locY + y < 3)
            dumpY += y;

        // 이동할 곳이 비어있지 않다면 이동하지 않음
        if(!Tiles[dumpY, dumpX].isOnTile)
        {
            locX = dumpX;
            locY = dumpY;
        }


        SetLotate();
    }

    // 타일 위로 이동
    public void SetLotate()
    {
        Vector3 vec = GameManager.Instance.BattleMNG.BattleField.GetTileLocate(LocX, LocY);
        transform.position = vec;

        // 현재 타일에 내가 들어왔다고 알려줌 
        Tiles[LocY, LocX].EnterTile(GetComponent<Character>());
    }
    #endregion

    #region Hit & Destroy
    // 데미지를 받을 때
    public void GetDamage(float DMG)
    {
        CurHP -= DMG;

        Debug.Log("DMG : " + DMG + ", CurHP ; " + CurHP);

        if (MaxHP <= CurHP)
            CurHP = MaxHP;

        DeadCheck();
    }

    // 캐릭터가 사망했는지 확인
    void DeadCheck()
    {
        if(CurHP <= 0)
        {
            // 죽었을 때 처리할 것들
            GameManager.Instance.DataMNG.BCL_CharExit(GetComponent<Character>());
            Tiles[LocY, LocX].ExitTile();

            Destroy(gameObject);
        }
    }
    #endregion



    public void TileSelected(int x, int y) => SelectTile = new Vector2(x, y);

    public int GetSpeed() => characterSO.stat.SPD;

    // 타락 게이지가 늘어나거나 줄어들 때
    public void SetFallGauge(int value)
    {
        int gauge = characterSO.FallGauge;
        int maxGauge = 3;

        gauge += value;
        if (gauge < 0) gauge = 0;
        else if (gauge > maxGauge)
        {
            characterSO.IsFall = true;
            if (characterSO.team == Team.Enemy)
                characterSO.team = Team.Player;
            else
                characterSO.team = Team.Enemy;
            gauge = 0;
        }

        characterSO.FallGauge = gauge;

        Debug.Log($"Fall Gauge : {gauge}, Is Fall? : {characterSO.IsFall}");
    }
}
