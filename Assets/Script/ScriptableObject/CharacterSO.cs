using System;
using System.Collections.Generic;
using UnityEngine;

// ĳ������ ����
[Serializable]
public struct Stat
{
    public float HP;
    public float ATK;
    public int SPD;
}

// ĳ������ ��
[Serializable]
public enum Team
{
    Player,
    Enemy
}

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Object/Character", order = 0)]
public class CharacterSO : ScriptableObject
{
    [SerializeField] public Team team;
    [SerializeField] public Sprite sprite;
    [SerializeField] public Stat stat;
    [SerializeField] SkillSO skill;

    // ĳ������ ��ų ���
    public void use(Character ch)
    {
        skill.use(ch);
    }

    public List<Vector2> HaveTargeting()
    {
        for (int i = 0; i < skill.EffectList.Count; i++)
        {
            if (skill.EffectList[i].GetType() == typeof(Effect_Attack))
                return skill.EffectList[i].GetRange();
        }
        return null;
    }
}
