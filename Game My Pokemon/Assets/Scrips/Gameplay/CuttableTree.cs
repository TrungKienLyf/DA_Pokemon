using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CuttableTree : MonoBehaviour, Interactable
{
    public IEnumerator Interact(Transform initiator)
    {
        yield return DialogManager.Instance.ShowDialogText("Cái cây này có vẻ có thể cắt được bằng Cut");

        var pokemonWithCut = initiator.GetComponent<PokemonParty>().Pokemons.FirstOrDefault(p => p.Moves.Any(m => m.Base.Name == "Cut"));

        if (pokemonWithCut != null)
        {
            int selectedChoice = 0;
            yield return DialogManager.Instance.ShowDialogText($"Bạn có muốn {pokemonWithCut.Base.Name} sử dụng Cut không?",
                choices: new List<string>() { "Có", "Không" },
                onChoiceSelected: (selection) => selectedChoice = selection);

            if (selectedChoice == 0)
            {
                // Có
                yield return DialogManager.Instance.ShowDialogText($"{pokemonWithCut.Base.Name} đã sử dụng cut!");
                gameObject.SetActive(false);
            }
        }
    }
}
