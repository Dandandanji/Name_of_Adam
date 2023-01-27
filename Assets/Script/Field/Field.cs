using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{

    [SerializeField] GameObject TilePrefabs;
    [SerializeField] GameObject UnitPrefabs;
    private const int MaxFieldX = 8;
    private const int MaxFieldY = 3;

    BattleManager _BattleMNG;
    BattleDataManager _BattleDataMNG;
    UIManager _UIMNG;

    List<List<Tile>> _TileArray = new List<List<Tile>>();
    public List<List<Tile>> TileArray => _TileArray;
    private Dictionary<Vector2, Tile> _tileDict = new Dictionary<Vector2, Tile>();

    // �ʵ��� ������ ���� �ʵ��� ��ġ
    public Vector3 FieldPosition => new Vector3(0, -1.4f, 0);
    public Vector3 FieldRotation => new Vector3(16, 0, 0);

    private void Awake()
    {
        for (int i = 0; i < MaxFieldY; i++)
            for (int j = 0; j < MaxFieldX; j++)
                _tileDict.Add(new Vector2(j, i), CreateTile(j, i));

        // Legacy
        for (int i = 0; i < MaxFieldY; i++)
        {
            TileArray.Add(new List<Tile>());

            for (int j = 0; j < MaxFieldX; j++)
            {
                TileArray[i].Add(CreateTile(j, i));
            }
        }

        transform.position = FieldPosition;
        transform.eulerAngles = FieldRotation;
    }

    private void Start()
    {
        _BattleMNG = GameManager.Instance.BattleMNG;
        _BattleDataMNG = _BattleMNG.BattleDataMNG;
        _UIMNG = GameManager.Instance.UIMNG;
    }

    private Tile CreateTile(int x, int y)
    {
        x -= MaxFieldX / 2;
        y -= MaxFieldY / 2;

        float disX = transform.localScale.x / MaxFieldX;
        float disY = transform.localScale.y / MaxFieldY;

        float locX = (disX * x) + (disX * 0.5f);
        float locY = disY * y;

        GameObject tile = GameObject.Instantiate(TilePrefabs, transform);
        tile.transform.position = new Vector3(locX, transform.position.y + locY);

        return tile.GetComponent<Tile>();
    }

    public BattleUnit GetTargetUnit(int x, int y)
    {
        if (RangeOverCheck(x, y))
        {
            TileArray[y][x].SetColor(Color.red);

            return TileArray[y][x].UnitOnTile;
        }

        return null;
    }

    private bool RangeOverCheck(int x, int y)
    {
        if (0 <= x && 0 <= y && x < MaxFieldX && y < MaxFieldY)
        {
            return true;
        }
        return false;
    }

    // ������ ��ġ�� �ִ� Ÿ���� ��ǥ�� ��ȯ
    public Vector3 GetTileLocate(int x, int y)
    {
        Vector3 vec = _TileArray[y][x].transform.position;

        float sizeX = _TileArray[y][x].transform.localScale.x * 0.5f;
        float sizeY = _TileArray[y][x].transform.localScale.y * 0.5f;
        sizeY += _TileArray[y][x].transform.localScale.y * 0.5f; // ��������Ʈ �������� ���� �ӽ���ġ

        vec.x += sizeX;
        vec.y += sizeY;

        return vec;
    }

    public void ClearAllColor()
    {
        foreach (List<Tile> list in TileArray)
        {
            foreach (Tile tile in list)
            {
                tile.SetCanSelect(false);
                tile.SetColor(Color.white);
            }
        }
    }

    public void EnterTile(BattleUnit ch, int x, int y)
    {
        TileArray[y][x].EnterTile(ch);
    }

    public void ExitTile(int x, int y)
    {
        TileArray[y][x].ExitTile();
    }

    public bool GetIsOnTile(int x, int y) => TileArray[y][x].IsOnTile;

    public void TileClick(Tile tile)
    {
        int tileX = 0,
            tileY = 0;

        for (int i = 0; i < TileArray.Count; i++)
        {
            for (int j = 0; j < TileArray[i].Count; j++)
            {
                if (ReferenceEquals(tile, TileArray[i][j]))
                {
                    tileX = j;
                    tileY = i;
                }
            }
        }

        // ���� Ŭ�� ���°� � ��������, Ŭ�� �������� üũ�ϴ� Ŭ���� ���� �ʿ�

        // ������ ������ Ÿ���� �������̶��
        if (tile.CanSelect)
        {
            ClearAllColor();
            _BattleMNG.GetNowUnit().TileSelected(tileX, tileY);
            return;
        }
        // Ŭ���� Ÿ�Ͽ� ������ ���� ��
        else if (tile.UnitOnTile != null)
        {
            //BattleUnit SelectUnit = tile.TileUnit;
            //FieldClear();

            //// �� ������ �Ʊ��̶��
            //if (tile.TileUnit.BattleUnitSO.MyTeam)
            //{
            //    // ������ ������ ��ų�� Ÿ���� �������� Ȯ���Ѵ�.
            //    List<Vector2> vecList = SelectUnit.BattleUnitSO.GetTargetingRange();
            //    SetCanSelect(vecList, SelectUnit);
            //}
        }
        // Ŭ���� Ÿ�Ͽ� ������ ���� ��
        else
        {
            //�ڵ带 ������ Ÿ���� ���� ��
            if (_UIMNG.Hands.ClickedHand != 0)
            {
                //���� ��
                if (tileX > 3 && tileY > 2)
                {
                    Debug.Log("out of range");
                }
                else
                {
                    if (_BattleDataMNG.CanUseMana(_UIMNG.Hands.ClickedUnit.GetUnitSO().ManaCost)) //���ǹ��� ���̶�� �̹� ������ �Ҹ�� ��
                    {
                        _BattleDataMNG.ChangeMana(-1 * _UIMNG.Hands.ClickedUnit.GetUnitSO().ManaCost);
                        GameObject BattleUnitPrefab = Instantiate(UnitPrefabs);

                        _BattleDataMNG.CreatBattleUnit(BattleUnitPrefab, tileX, tileY);

                        _UIMNG.Hands.RemoveHand(_UIMNG.Hands.ClickedHand);
                        _UIMNG.Hands.ClearHand();
                    }
                    else
                    {
                        //���� ����
                        Debug.Log("not enough mana");
                    }
                }
            }
        }
    }

    public void SetCanSelect(List<Vector2> vecList, BattleUnit SelectUnit)
    {
        if (vecList != null)
        {
            // Ÿ������ �´ٸ� ���� ǥ��
            for (int i = 0; i < vecList.Count; i++)
            {
                int x = SelectUnit.LocX - (int)vecList[i].x;
                int y = SelectUnit.LocY - (int)vecList[i].y;

                if (0 <= x && x < 8)
                {
                    if (0 <= y && y < 3)
                        TileArray[y][x].SetCanSelect(true);
                }
            }
        }
    }
}