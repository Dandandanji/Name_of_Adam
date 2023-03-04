using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    // ����Ǵ� ����

    public Stat Stat;
    public List<Passive> Stigma;

    // ������� �ʴ� ����

    public string Name;
    public string Description;
    public Faction Faction;
    public Rarity Rarity;
    public Sprite Image;
    public int DarkEssenseDrop;
    public int DarkEssenseCost;
    public BehaviorType BehaviorType;
    public TargetType TargetType;
    public UnitSkill UnitSkill;


    const int Arow = 5;
    const int Acolumn = 11;

    const int Mrow = 5;
    const int Mcolumn = 5;

    public bool[] AttackRange = new bool[Arow * Acolumn];
    public bool[] MoveRange = new bool[Mrow * Mcolumn];
}
