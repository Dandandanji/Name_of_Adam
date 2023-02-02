using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_AI_Controller : MonoBehaviour
{
    protected BattleDataManager _BattleDataMNG;

    protected Field _field;

    protected List<Vector2> FindTileList = new List<Vector2>();
    protected List<Vector2> RangedVectorList = new List<Vector2>();
    protected List<Vector2> AttackRangeList = new List<Vector2>();
    protected SortedSet<Vector3> AttackTileSet = new SortedSet<Vector3>();

    protected BattleUnit caster = new BattleUnit();


    void Awake()
    {
        _BattleDataMNG = GameManager.BattleMNG.BattleDataMNG;
        _field = GameManager.BattleMNG.Field;
    }

    public virtual void AI_Action()
    {
        
    }

    public void SetCaster(BattleUnit unit)
    {
        caster = unit;
    }
    #region AISet
    protected void SetFindTileList()
    {
        //���� ��ġ���� ���ݹ��� ���� ������ ã�´�.

        FindTileList.Clear();
        RangedVectorList.Clear();


        foreach (Vector2 arl in caster.BattleUnitSO.GetRange())
        {
            Vector2 vector = caster.Location;

            if (_field.IsInRange(vector))
            {
                Vector2 vec = vector;
                if (_field.TileDict[vec].IsOnTile)
                {
                    FindTileList.Add(vec);
                    // ���� if �ѹ� �� �־ �Ǹ� ���⼭ ���Ÿ� �Ǻ�
                }
            }
        }

        foreach (Vector2 ftl in FindTileList)
        {
            if (_field.TileDict[ftl].Unit.BattleUnitSO.RType == RangeType.Ranged)
            {
                RangedVectorList.Add(ftl);
            }
        }

        // ���� ������ FindTileList�� ���� �� ���Ÿ� ����Ʈ ����
    }

    protected bool IsUnitExist()
    {
        // ������ �������� �ִ��� Ȯ��
        return FindTileList.Count > 0;
    }

    protected bool IsRangedUnitExist()
    {
        return RangedVectorList.Count > 0;
    }

    protected void SetDistance()
    {
        foreach (BattleUnit unit in _BattleDataMNG.BattleUnitList)
        {
            if (unit.BattleUnitSO.MyTeam)
            {
                foreach (Vector2 arl in caster.BattleUnitSO.GetRange())
                {
                    Vector3 vector = unit.Location - arl;
                    if (unit.BattleUnitSO.RType == RangeType.Ranged)
                        vector.z = 0f;//���Ÿ��� 0
                    else
                        vector.z = 0.1f;//�ٰŸ��� 0.1


                    AttackTileSet.Add(vector);
                }
            }
        }
    }

    protected void SearchAttackableTile()
    {

        //��� ���� Ÿ���� AttackTileSet�� �����Ѵ�. X, Y�� ��ǥ, Z�� ���Ÿ�/�ٰŸ� ����
        //������ ���� �� �ִ� Ÿ���� �̵� ���� ���� �ִ� �� Ȯ���Ѵ�.
        //�� ��, �Ʒ�, ��, �����ʸ� �̵� �����ϴٰ� ����

        FindTileList.Clear();
        RangedVectorList.Clear();

        for (int i = -1; i <= 1; i += 2)
        {
            for (float j = 0; j <= 0.1f; j += 0.1f)
            {
                Vector3 vec1 = new Vector3(caster.Location.x + i, caster.Location.y, j);
                if (AttackTileSet.Contains(vec1))
                {
                    FindTileList.Add(vec1);
                }

                Vector3 vec2 = new Vector3(caster.Location.x, caster.Location.y + i, j);
                if (AttackTileSet.Contains(vec2))
                {
                    FindTileList.Add(vec2);
                }
            }
        }

        foreach (Vector3 v in FindTileList)
        {
            if (v.z == 0)
            {
                RangedVectorList.Add(new Vector2(v.x, v.y));
            }
        }

    }

    protected void OrderbyDistance()
    {
        Vector3 MyPosition = caster.Location;

        float dis = 100f;
        Vector3 minVec = new Vector3();

        foreach (Vector3 v in RangedVectorList)
        {
            if (dis > Mathf.Abs(v.x - MyPosition.x) + Mathf.Abs(v.y - MyPosition.y))
            {
                dis = Mathf.Abs(v.x - MyPosition.x) + Mathf.Abs(v.y - MyPosition.y);
                minVec = v;
            }
        }
        //���� ����� Ÿ�� = minVec���� �̵�

        dis = 100f;//��Ȱ��
        Vector3 moveVec = new Vector3();
        for (int i = -1; i <= 1; i += 2)
        {
            Vector3 vec1 = new Vector3(MyPosition.x + i, MyPosition.y, 0);
            if (dis > (vec1 - minVec).sqrMagnitude)
            {
                dis = (vec1 - minVec).sqrMagnitude;
                moveVec = vec1;
            }

            Vector3 vec2 = new Vector3(MyPosition.x, MyPosition.y + i, 0);
            if (dis > (vec2 - minVec).sqrMagnitude)
            {
                dis = (vec2 - minVec).sqrMagnitude;
                moveVec = vec2;
            }
        }
    }
    #endregion
}

public class Common_Unit_AI_Controller : Unit_AI_Controller
{
    public override void AI_Action()
    {

        //���޹��� �������� ������ ã�´�.
        foreach (Vector2 arl in AttackRangeList)
        {
            Vector2 vector = caster.Location;

            if (_field.IsInRange(vector))
            {
                Vector2 vec = vector;
                if (_field.TileDict[vec].IsOnTile)
                {
                    FindTileList.Add(vec);
                }
            }
        }

        //ã�� ������ �ִ��� Ȯ���ϰ�, �ִٸ� ���Ÿ�����, �ٰŸ����� Ȯ���Ѵ�.
        if (FindTileList.Count > 0)
        {
            foreach (Vector2 ftl in FindTileList)
            {
                if (_field.TileDict[ftl].Unit.BattleUnitSO.RType == RangeType.Ranged)
                {
                    RangedVectorList.Add(ftl);
                }
            }

            if (RangedVectorList.Count > 0)
            {
                //���Ÿ� ������ ���� ���
                //Random.Range(0, RangeList.Count);
            }
            else
            {
                //�ٰŸ� ���ָ� ���� ���
                //Random.Range(0, findUnitList.Count);
            }
        }
        else
        {
            //���� ���� ������ ã�� ������ ������ �̵��ϰ� �����Ѵ�
            SetDistance();
            SearchAttackableTile();
            if (IsUnitExist())
            {
                if (IsRangedUnitExist())
                {
                    //���Ÿ��� ����
                    //Random.Range(0, RangedVectorList.Count);
                    
                }
                else
                {
                    //�ٰŸ��� ����
                    //Random.Range(0, FindTileList.Count);
                }
            }
            else
            {
                OrderbyDistance();
                //moveVec���� �̵�
            }
        }
    }
}