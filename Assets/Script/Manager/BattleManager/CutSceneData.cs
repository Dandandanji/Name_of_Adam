using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCutSceneData
{
    public BattleUnit AttackUnit;
    public List<BattleUnit> HitUnits;

    // �� ������ ��ġ
    public Vector3 AttackPosition;
    public List<Vector3> HitPosition;
    // �� ������ Ÿ�� ��ġ
    public Vector2 AttackLocation;
    public List<Vector2> HitLocation;

    // ���� Ÿ��
    public CutSceneMoveType MoveType;
    // ������ �� ������
    public float DefaultZoomSize;
    // �󸶳� ���� ������
    public float ZoomSize;
    // ������� ����� ������
    public float GradientPower;

    // Ȯ�� �� ��ġ
    public Vector3 ZoomLocation;
    // ������ ��ġ
    public Vector3 MovePosition;
    // ���� ������ ��� ���⿡ �ִ���
    public int AttackUnitDirection;

    public BattleCutSceneData(BattleUnit AttackUnit, List<BattleUnit> HitUnits)
    {
        this.AttackUnit = AttackUnit;
        this.HitUnits = HitUnits;

        AttackLocation = AttackUnit.Location;
        AttackPosition = BattleManager.Field.GetTilePosition(AttackUnit.Location);
        HitLocation = new List<Vector2>();
        HitPosition = new List<Vector3>();
        foreach (BattleUnit unit in HitUnits)
        {
            HitLocation.Add(unit.Location);
            HitPosition.Add(BattleManager.Field.GetTilePosition(unit.Location));
        }

        DefaultZoomSize = Camera.main.fieldOfView;
        MoveType = AttackUnit.Data.AnimType.MoveType;
        ZoomSize = AttackUnit.Data.AnimType.ZoomSize;
        GradientPower = AttackUnit.Data.AnimType.GradientPower;

        MovePosition = BattleManager.Field.GetTilePosition(GetMoveLocation(AttackUnit));
        
        Vector3 vec = Vector3.Lerp(MovePosition, HitPosition[0], 0.5f);
        ZoomLocation = new Vector3(vec.x, vec.y, 0);
    }

    Vector2 GetMoveLocation(BattleUnit unit)
    {
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
            if (unit.GetFlipX())
            {
                moveTile.x += 1;
                AttackUnitDirection = 1;
            }
            else
            {
                moveTile.x -= 1;
                AttackUnitDirection = -1;
            }
        }

        if (unit.Data.AnimType.MoveType == CutSceneMoveType.stand)
            return unit.Location;

        return moveTile;
    }
}