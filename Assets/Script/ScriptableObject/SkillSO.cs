using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Object/Skill", order = 1)]
public class SkillSO : ScriptableObject
{
    [SerializeField] public List<EffectSO> EffectList;

    // 이펙트 리스트 안의 모든 이펙트가 하나의 이펙트로 묶여서 사용된다.
    public void use(BattleUnit ch)
    {
        for (int i = 0; i < EffectList.Count; i++)
        {
            EffectList[i].Effect(ch);
        }
    }
}