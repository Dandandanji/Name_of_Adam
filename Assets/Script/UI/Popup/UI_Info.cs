using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Info : UI_Popup
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private TextMeshProUGUI _stat;

    [SerializeField] private UI_HPBar _hpBar;
    [SerializeField] private UI_HoverImageBlock _stigama_small;
    [SerializeField] private Transform _stigamaGrid;

    [SerializeField] private UI_HoverImageBlock _SkillImage;

    [SerializeField] private Transform _rangeGrid;
    [SerializeField] private GameObject _squarePrefab;

    public void Set(DeckUnit unit, Team team, int currnetHP, int fall)
    {
        _name.text = unit.Data.Name;
        _cost.text = unit.Stat.ManaCost.ToString();

        _stat.text = "HP:     " + currnetHP.ToString() + " / " + unit.Stat.HP.ToString() + "\n" +
                       "Attack: " + unit.Stat.ATK.ToString() + "\n" +
                       "Speed:  " + unit.Stat.SPD.ToString();

        _hpBar.SetHPBar(team, null);
        _hpBar.SetFallBar(unit);

        _hpBar.RefreshHPBar((float)currnetHP / (float)unit.Stat.HP);
        _hpBar.RefreshFallGauge(fall);

        unit.SetStigma();
        foreach (Passive sti in unit.Stigmata)
        {
            Debug.Log("����");
            ���� stig = unit.PassiveToStigma(sti);

            GameObject.Instantiate(_stigama_small, _stigamaGrid).GetComponent<UI_HoverImageBlock>().Set(unit.GetStigmaImage(stig), unit.GetStigmaText(stig));
        }

        if (unit.Data.BehaviorType == BehaviorType.�ٰŸ�)
        {
            _SkillImage.Set(GameManager.Resource.Load<Sprite>($"Arts/UI/Battle_UI/�ٰŸ�_������"), unit.Data.Description.Replace("(ATK)", unit.Stat.ATK.ToString()));
        }
        else
        {
            _SkillImage.Set(GameManager.Resource.Load<Sprite>($"Arts/UI/Battle_UI/���Ÿ�_������"), unit.Data.Description.Replace("(ATK)", unit.Stat.ATK.ToString()));
        }

        foreach (bool range in unit.Data.AttackRange)
        {
            Image block = GameObject.Instantiate(_squarePrefab, _rangeGrid).GetComponent<Image>();
            if (range)
                block.color = Color.red;
            else
                block.color = Color.grey;
        }
    }
}
