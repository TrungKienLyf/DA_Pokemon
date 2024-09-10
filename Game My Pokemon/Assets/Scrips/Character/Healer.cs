using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public IEnumerator Heal(Transform player, Dialog dialog)
    {
        int selectedChoice = 0;

        yield return DialogManager.Instance.ShowDialogText("Bạn và pokemon của mình có vẻ khá mệt mỏi! Bạn có muốn nghỉ ngơi và hồi phục không?", 
               choices: new List<string>() { "Được", "Không, cảm ơn" },
               onChoiceSelected: (choiceIndex) => selectedChoice = choiceIndex);

        if(selectedChoice == 0)
        {
            //yes
            yield return Fader.i.FadeIn(0.5f);

            var playerParty = player.GetComponent<PokemonParty>();
            playerParty.Pokemons.ForEach(p => p.Heal());
            playerParty.PartyUpdated();

            yield return Fader.i.FadeOut(0.5f);

            yield return DialogManager.Instance.ShowDialogText($"Pokemon của bạn đã được hồi phục toàn bộ thể lực");
        }
        else if(selectedChoice == 1)
        {
            //no
            yield return DialogManager.Instance.ShowDialogText($"Được thôi! Hãy quay lại khi bạn đổi ý nhé");
        }

        
    }
}
