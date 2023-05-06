using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneData
{
    public BattleUnit AttackUnit;
    public List<BattleUnit> HitUnits;

    // �� ������ ��ġ
    public Vector3 AttackPosition;
    public List<Vector3> HitPosition = new List<Vector3>();
    // �� ������ Ÿ�� ��ġ
    public Vector2 AttackLocation;
    public List<Vector2> HitLocation = new List<Vector2>();

    // ���� Ÿ��
    public CutSceneMoveType MoveType;
    // ������ �� ������
    public float DefaultZoomSize;
    // �󸶳� ���� ������
    public float ZoomSize;
    // ������� ����� ������
    public float TiltPower;

    // Ȯ�� �� ��ġ
    public Vector3 ZoomLocation;
    // ������ ��ġ
    public Vector3 MovePosition;
    // ���� ������ ��� ���⿡ �ִ���
    public int AttackUnitDirection;

    public CutSceneData(BattleUnit AttackUnit, List<BattleUnit> HitUnits)
    {
        this.AttackUnit = AttackUnit;
        this.HitUnits = HitUnits;

        AttackPosition = AttackUnit.transform.position;
        AttackLocation = AttackUnit.Location;
        HitPosition = new List<Vector3>();
        HitLocation = new List<Vector2>();
        foreach (BattleUnit unit in HitUnits)
        {
            HitPosition.Add(unit.transform.position);
            HitLocation.Add(unit.Location);
        }

        DefaultZoomSize = Camera.main.fieldOfView;
        MoveType = AttackUnit.Data.AnimType.MoveType;
        ZoomSize = AttackUnit.Data.AnimType.ZoomSize;
        TiltPower = AttackUnit.Data.AnimType.TiltPower;

        MovePosition = BattleManager.Field.GetTilePosition(GetMoveLocation(AttackUnit));
        
        Vector3 vec = Vector3.Lerp(MovePosition, HitPosition[0], 0.5f);
        ZoomLocation = new Vector3(vec.x, vec.y, 0);
    }

    Vector2 GetMoveLocation(BattleUnit unit)
    {
        if (unit.Data.AnimType.MoveType == CutSceneMoveType.stand)
            return unit.Location;

        Vector2 AttackUnitTile = AttackLocation;
        Vector2 HitUnitTile = HitLocation[0];

        Vector2 moveTile = HitUnitTile;

        if (AttackUnitTile.x < HitUnitTile.x)
        {
            moveTile.x -= 1;
            AttackUnitDirection = -1;
        }
        else if (HitUnitTile.x < AttackUnitTile.x)
        {
            moveTile.x += 1;
            AttackUnitDirection = 1;
        }
        else
        {
            moveTile = AttackUnitTile;
            AttackUnitDirection = 0;
        }

        return moveTile;
    }
}