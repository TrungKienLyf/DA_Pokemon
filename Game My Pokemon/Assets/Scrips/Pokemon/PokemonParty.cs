using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokemonParty : MonoBehaviour
{
    [SerializeField] List<Pokemon> pokemons;
    public event Action OnUpdated;
    public List<Pokemon> Pokemons
    {
        get { return pokemons;}
        set
        {
            pokemons = value;
            OnUpdated?.Invoke();
        }
    }

    private void Awake()
    {
        foreach (var pokemon in pokemons)
        {
            pokemon.Init();
        }
    }
    private void Start() { }

    public Pokemon GetHealthyPokemon()
    {
        return pokemons.Where(x => x.HP > 0).FirstOrDefault();
    }

    public void AddPokemon(Pokemon newPokemon)
    {
        if (pokemons.Count < 6)
        {
            pokemons.Add(newPokemon);
            OnUpdated?.Invoke();
        }
        else
        {
            // sẽ lm: thêm vào PC nếu có trên 6 pokemon
        }
    }

    public bool CheckForEvolutions()
    {
        return pokemons.Any(p => p.CheckForEvolution() != null);
    }

    public IEnumerator RunEvolutions()
    {
        foreach(var pokemon in pokemons)
        {
            var evolution = pokemon.CheckForEvolution();
            if(evolution != null)
            {
                yield return EvolutionManager.i.Evolve(pokemon, evolution);
            }
        }
    }

    public void PartyUpdated()
    {
        OnUpdated?.Invoke();
    }

    public static PokemonParty GetPlayerParty()
    {
        return FindObjectOfType<PlayerController>().GetComponent<PokemonParty>();
    }

    public bool HasPokemonCanBattle()
    {
        int count = pokemons.Where(x => x.HP > 0).Count();
        if (count != 0)
            return true;
        else
            return false;

    }
}
