﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCutSceneData
{
    public BattleUnit AttackUnit;
    public List<BattleUnit> HitUnits;

    // 각 유닛의 위치
    public Vector3 AttackPosition;
    public List<Vector3> HitPosition;
    // 각 유닛의 타일 위치
    public Vector2 AttackLocation;
    public List<Vector2> HitLocation;

    // 공격 타입
    public CutSceneMoveType MoveType;
    // 얼마나 줌할 것인지
    public float ZoomSize;

    // 확대 할 위치
    public Vector3 ZoomLocation;
    // 움직일 위치
    public Vector3 MovePosition;
    // 공격 유닛이 바라보는 방향
    public int AttackUnitFlipX;

    public BattleCutSceneData(BattleUnit AttackUnit, List<BattleUnit> HitUnits)
    {
        this.AttackUnit = AttackUnit;
        this.HitUnits = HitUnits;

        AttackLocation = AttackUnit.Location;
        AttackPosition = BattleManager.Field.GetTilePosition(AttackLocation);
        HitLocation = new List<Vector2>();
        HitPosition = new List<Vector3>();
        foreach (BattleUnit unit in HitUnits)
        {
            HitLocation.Add(unit.Location);
            HitPosition.Add(BattleManager.Field.GetTilePosition(unit.Location));
        }

        MoveType = AttackUnit.Data.AnimType.MoveType;
        ZoomSize = AttackUnit.Data.AnimType.ZoomSize;

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
            AttackUnitFlipX = -1;
        }
        else if (HitUnitTile.x < AttackUnitTile.x)
        {
            moveTile.x += 1;
            AttackUnitFlipX = 1;
        }
        else
        {
            if (AttackUnitTile.y > HitUnitTile.y)
            {
                moveTile.x -= 1;
                AttackUnitFlipX = -1;
            }
            else
            {
                moveTile.x += 1;
                AttackUnitFlipX = 1;
            }
        }

        if (unit.Data.AnimType.MoveType == CutSceneMoveType.stand)
            return unit.Location;

        return moveTile;
    }
}