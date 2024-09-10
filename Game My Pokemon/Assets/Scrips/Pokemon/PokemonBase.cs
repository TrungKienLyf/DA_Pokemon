using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
public class PokemonBase : ScriptableObject
{
    [SerializeField] string namepkm;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    // Base Stats
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] int expYield;
    [SerializeField] GrowthRate growthRate;

    [SerializeField] int catchRate = 255;

    [SerializeField] List<LearnableMove> learnableMoves;
    [SerializeField] List<MoveBase> learnableByItems;

    [SerializeField] List<Evolution> evolutions;

    public static int MaxNumOfMoves { get; set; } = 4;

    public int GetExpForLevel(int level)
    {
        if (growthRate == GrowthRate.Fast)
        {
            return 4 * (level * level * level) / 5;
        }
        else if (growthRate == GrowthRate.MediumFast)
        {
            return level * level * level;
        }
        else if (growthRate == GrowthRate.MediumSlow)
        {
            return 6 * (level * level * level - 75 * level * level + 5000 * level - 700) / 5;
        }
        else if (growthRate == GrowthRate.Slow)
        {
            return 5 * (level * level * level) / 4;
        }
        return -1;
    }

    public string Name
    {
        get { return namepkm; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public PokemonType Type1
    {
        get { return type1; }
    }

    public PokemonType Type2
    {
        get { return type2; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int SpDefense
    {
        get { return spDefense; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }

    public List<MoveBase> LearnableByItems => learnableByItems;
    public List<Evolution> Evolutions => evolutions;

    public int CatchRate => catchRate;

    public int ExpYield => expYield;

    public GrowthRate GrowthRate => growthRate;
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base
    {
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }
}

[System.Serializable]
public class Evolution
{
    [SerializeField] PokemonBase evolvesInfo;
    [SerializeField] int requiredLevel;
    [SerializeField] EvolutionItem requiredItem;

    public PokemonBase EvolvesInfo => evolvesInfo;
    public int RequiredLevel => requiredLevel;
    public EvolutionItem RequiredItem => requiredItem;
}

public enum PokemonType
{
    None,    Nomal,    Fire,    Water,    Electric,    Grass,    Ice,    Fighting,    Poison,    Ground,
    Flying,    Psychic,    Bug,    Rock,    Ghost,    Dragon,    Dark,    Steel,    Fairy
}

public enum GrowthRate
{
    Fast, MediumFast,MediumSlow, Slow
}

public enum Stat
{
    Attack,    Defense,    SpAttack,    SpDefense,    Speed,

   /* 2 thuộc tính này dùng để xác định độ chính xác khi tung kỹ năng xem nó trúng hay 
    * không, và chỉ số nào chỉ thay đổi bởi một số kỹ năng tăng lên hoặc thuộc tính chứ 
    * không thay dổi bởi cấp độ của pokemon*/ 
    Accuracy,    Evasion
}

public class TypeChart
{
    static float[][] chart =
    {
      //                              NOR     FIR     WAT     ELE     GRA     ICE     FIG     POI     GRO     FLY     PFY     BUG     ROC     GHO     DRA     DAR     STE     FAIRY
        /*NORMAL*/      new float[] {   1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     0.5f,   0f,     1f,     1f,     0.5f,   1f     },
        /*FIRE*/        new float[] {   1f,     0.5f,   0.5f,   1f,     2f,     2f,     1f,     1f,     1f,     1f,     1f,     2f,     0.5f,   1f,     0.5f,   1f,     2f,     1f     },
        /*WATER*/       new float[] {   1f,     2f,     0.5f,   1f,     0.5f,   1f,     1f,     1f,     2f,     1f,     1f,     1f,     2f,     1f,     0.5f,   1f,     1f,     1f     },
        /*ELECTRIC*/    new float[] {   1f,     1f,     2f,     0.5f,   0.5f,   1f,     1f,     1f,     0f,     2f,     1f,     1f,     1f,     1f,     0.5f,   1f,     1f,     1f     },
        /*GRASS*/       new float[] {   1f,     0.5f,   2f,     1f,     0.5f,   1f,     1f,     0.5f,   2f,     0.5f,   1f,     0.5f,   2f,     1f,     0.5f,   1f,     0.5f,   1f     },
        /*ICE*/         new float[] {   1f,     0.5f,   0.5f,   1f,     2f,     0.5f,   1f,     1f,     2f,     2f,     1f,     1f,     1f,     1f,     2f,     1f,     0.5f,   1f     },
        /*FIGHTING*/    new float[] {   2f,     1f,     1f,     1f,     1f,     2f,     1f,     0.5f,   1f,     0.5f,   0.5f,   0.5f,   2f,     0f,     1f,     2f,     2f,     0.5f   },
        /*POISON*/      new float[] {   1f,     1f,     1f,     1f,     2f,     1f,     1f,     0.5f,   0.5f,   1f,     1f,     1f,     0.5f,   0.5f,   1f,     1f,     0f,     2f     },
        /*GROUND*/      new float[] {   1f,     2f,     1f,     2f,     0.5f,   1f,     1f,     2f,     1f,     0f,     1f,     0.5f,   2f,     1f,     1f,     1f,     2f,     1f     },
        /*FLYING*/      new float[] {   1f,     1f,     1f,     0.5f,   2f,     1f,     2f,     1f,     1f,     1f,     1f,     2f,     0.5f,   1f,     1f,     1f,     0.5f,   1f     },
        /*PSYCHIC*/     new float[] {   1f,     1f,     1f,     1f,     1f,     1f,     2f,     2f,     1f,     1f,     0.5f,   1f,     1f,     1f,     1f,     0f,     0.5f,   1f     },
        /*BUG*/         new float[] {   1f,     0.5f,   1f,     1f,     2f,     1f,     0.5f,   0.5f,   1f,     0.5f,   2f,     1f,     1f,     0.5f,   1f,     2f,     0.5f,   0.5f   },
        /*ROCK*/        new float[] {   1f,     2f,     1f,     1f,     1f,     1f,     2f,     0.5f,   1f,     0.5f,   2f,     1f,     2f,     1f,     1f,     1f,     0.5f,   1f     },
        /*GHOST*/       new float[] {   0f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     2f,     1f,     1f,     2f,     1f,     0.5f,   1f,     1f     },
        /*DRAGON*/      new float[] {   1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     2f,     1f,     0.5f,   0f     },
        /*DARK*/        new float[] {   1f,     1f,     1f,     1f,     1f,     1f,     0.5f,   1f,     1f,     1f,     2f,     1f,     1f,     2f,     1f,     0.5f,   1f,     0.5f   },
        /*STEEL*/       new float[] {   1f,     0.5f,   0.5f,   0.5f,   1f,     2f,     1f,     1f,     1f,     1f,     1f,     1f,     2f,     1f,     1f,     1f,     0.5f,   2f     },
        /*FAIRY*/       new float[] {   1f,     0.5f,   1f,     1f,     1f,     1f,     2f,     0.5f,   1f,     1f,     1f,     1f,     1f,     1f,     2f,     2f,     0.5f,   1f     }

    };

    public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
    {
        if (attackType == PokemonType.None || defenseType == PokemonType.None)
            return 1;

        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
    }
}
