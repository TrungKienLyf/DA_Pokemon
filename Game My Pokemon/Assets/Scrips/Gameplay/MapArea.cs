using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    [SerializeField] List<PokemonEncounterRecord> wildPokemons;
    [SerializeField] List<PokemonEncounterRecord> wildPokemonsInWater;

    [HideInInspector]
    [SerializeField] int totalChance = 0;

    [HideInInspector]
    [SerializeField] int totalChanceInWater = 0;
    private void OnValidate()
    {
        CalculatioteChancePercentage();
    }
    private void Start()
    {
        CalculatioteChancePercentage();
    }

    void CalculatioteChancePercentage()
    {
        totalChance = -1;
        totalChanceInWater = -1;
        if (wildPokemons.Count > 0)
        {
            totalChance = 0;
            foreach (var record in wildPokemons)
            {
                record.chanceLower = totalChance;
                record.chanceUpper = totalChance + record.changePercentage;

                totalChance = totalChance + record.changePercentage;
            }
        }

        if (wildPokemonsInWater.Count > 0)
        {
            totalChanceInWater = 0;
            foreach (var record in wildPokemonsInWater)
            {
                record.chanceLower = totalChanceInWater;
                record.chanceUpper = totalChanceInWater + record.changePercentage;

                totalChanceInWater = totalChanceInWater + record.changePercentage;
            }
        }
    }
    public Pokemon GetRandomWildPokemon(BattleTrigger trigger)
    {
        var pokemonList = (trigger == BattleTrigger.LongGrass) ? wildPokemons : wildPokemonsInWater;

        int randVal = Random.Range(1, 101);
        var pokemonRecord = pokemonList.First(p => randVal >= p.chanceLower && randVal <= p.chanceUpper);

        var levelRange = pokemonRecord.levelRange;
        int level = levelRange.y == 0 ? levelRange.x : Random.Range(levelRange.x, levelRange.y + 1);

        var wildPokemon = new Pokemon(pokemonRecord.pokemon, level);
        
        wildPokemon.Init();
        return wildPokemon;
    }
}

[System.Serializable]
public class PokemonEncounterRecord
{
    public PokemonBase pokemon;
    public Vector2Int levelRange;
    public int changePercentage;

    public int chanceLower { get; set; }
    public int chanceUpper { get; set; }
}