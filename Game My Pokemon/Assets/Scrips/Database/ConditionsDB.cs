using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsDB
{
    public static void Init()
    {
        foreach (var kvp in Conditions)
        {
            var conditionId = kvp.Key;
            var condition = kvp.Value;

            condition.Id = conditionId;
        }
    }
    public static Dictionary<ConditionID, Condition> Conditions { get; set; } = new Dictionary<ConditionID, Condition>()
    {
        {
            ConditionID.psn,
            new Condition()
            {
                Name = "Trúng độc",
                StartMessage = "đã bị trúng độc",
                OnAfterTurn = (Pokemon pokemon) =>
                {
                    pokemon.DecreaseHP(pokemon.MaxHp / 8);
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} nhận sát thương vì đang trúng độc");
                }
            }
        },
        {
            ConditionID.brn,
            new Condition()
            {
                Name = "Bỏng",
                StartMessage = "đã bị bỏng",
                OnAfterTurn = (Pokemon pokemon) =>
                {
                    pokemon.DecreaseHP(pokemon.MaxHp / 16);
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} nhận sát thương vì bị bỏng");
                }
            }
        },
        {
            ConditionID.par,
            new Condition()
            {
                Name = "Tê liệt",
                StartMessage = "đã bị trúng hiệu ứng tê liệt",
                OnBeforeMove = (Pokemon pokemon) =>
                {
                    if  (Random.Range(1, 5) == 1)
                    {
                        pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} đã bị tê liệt và không thể sử dụng kỹ năng");
                        return false;
                    }

                    return true;
                }
            }
        },
        {
            ConditionID.frz,
            new Condition()
            {
                Name = "Đóng băng",
                StartMessage = "đã bị trúng hiệu ứng đóng băng",
                OnBeforeMove = (Pokemon pokemon) =>
                {
                    if  (Random.Range(1, 5) == 1)
                    {
                        pokemon.CureStatus();
                        pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} không còn bị đóng băng nữa");
                        return true;
                    }

                    return false;
                }
            }
        },
        {
            ConditionID.slp,
            new Condition()
            {
                Name = "Ngủ",
                StartMessage = "đã bị trúng hiệu ứng ngủ",
                OnStart = (Pokemon pokemon) =>
                {                    
                    pokemon.StatusTime = Random.Range(1, 4);
                    Debug.Log($"Sẽ ngủ trong  {pokemon.StatusTime} lượt");
                },
                OnBeforeMove = (Pokemon pokemon) =>
                {
                    if (pokemon.StatusTime <= 0)
                    {
                        pokemon.CureStatus();
                        pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} thức dậy!");
                        return true;
                    }

                    pokemon.StatusTime--;
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} đang ngủ");
                    return false;
                }
            }
        },

        {
            ConditionID.confusion,
            new Condition()
            {
                Name = "Bối rối",
                StartMessage = "rơi vào trạng thái bối rối",
                OnStart = (Pokemon pokemon) =>
                {
                    pokemon.VolatileStatusTime = Random.Range(1, 5);
                    Debug.Log($"Sẽ bị hiệu ứng bối rối trong {pokemon.VolatileStatusTime} lượt");
                },
                OnBeforeMove = (Pokemon pokemon) =>
                {
                    if (pokemon.VolatileStatusTime <= 0)
                    {
                        pokemon.CureVolatileStatus();
                        pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} thoát khỏi trạng thái bối rối!");
                        return true;
                    }
                    pokemon.VolatileStatusTime--;

                    // 50% cơ hội thực hiện kỹ năng
                    if (Random.Range(1, 3) == 1)
                        return true;

                    // bị thương bởi bối rối
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} đang bối rối");
                    pokemon.DecreaseHP(pokemon.MaxHp / 8);
                    pokemon.StatusChanges.Enqueue($"Nó đã tự gậy sát thương cho mình vì đang trong trạng thái bối rối");
                    return false;
                }
            }
        }
    };

    public static float GetStatusBonus(Condition condition)
    {
        if (condition == null)
            return 1f;
        else if (condition.Id == ConditionID.slp || condition.Id == ConditionID.frz)
            return 2f;
        else if (condition.Id == ConditionID.par || condition.Id == ConditionID.psn || condition.Id == ConditionID.brn)
            return 1.5f;

        return 1f;
    }
}

public enum ConditionID
{
    none, psn, brn, slp, par, frz, confusion
}