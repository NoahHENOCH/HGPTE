using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBarVUI : MonoBehaviour
{
    [SerializeField]
    public GameObject content;
    public GameObject inventorySlotPrefab;
    public Player player;
    void Start()
    {
        player.inventory.onInventoryChangedBar += UpdateUI; // on ajoute l'event
    }

    void OnDestroy()
    {
        player.inventory.onInventoryChangedBar -= UpdateUI;
    }

    // met à jour l'UI de la barre d'inventaire verticale
    public void UpdateUI(){
        List <Slot> slots = player.inventory.slots;
        Slot lastSlot = player.inventory.lastSlot;
        SlotUI dataSlot;
        if(lastSlot != null){

            Transform existingSlot = null;

            // on parcours les slots pour voir si on a deja un slot avec le id
            foreach (Transform child in content.transform)
            {
                dataSlot = child.gameObject.GetComponent<SlotUI>();
                if (dataSlot != null && dataSlot.getId() == lastSlot.id)
                {
                    existingSlot = child;
                    break;
                }
            }
            if (existingSlot) // si on a trouvé un slot avec le meme id
            {
                refreshGraphicSlot(existingSlot); // on refresh le slot
            }
            else{
                createGraphicSlot(lastSlot); // on crée un nouveau slot
            }
        }
    } // UpdateUI()

    // rafraichit le slot graphiquement
    private void refreshGraphicSlot(Transform existingSlot){
        SlotUI dataSlot = existingSlot.gameObject.GetComponent<SlotUI>();
        int nb = dataSlot.getNb();
        dataSlot.setNb(nb + 1);
        existingSlot.GetChild(1).gameObject.GetComponent<Text>().text = (nb + 1).ToString();
    } // refreshGraphicSlot()

    // crée un slot graphiquement
    private void createGraphicSlot(Slot lastSlot){
        GameObject graphicSlot = Instantiate(inventorySlotPrefab);

        SlotUI dataSlot = graphicSlot.AddComponent<SlotUI>();
        dataSlot.setId(lastSlot.id);
        dataSlot.setNb(1);

        graphicSlot.transform.GetChild(0).gameObject.GetComponent<Text>().text = lastSlot.name;
        graphicSlot.transform.GetChild(1).gameObject.GetComponent<Text>().text = dataSlot.getNb().ToString();
        graphicSlot.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = lastSlot.icon;
        graphicSlot.transform.SetParent(content.transform, false);
        graphicSlot.transform.SetAsFirstSibling();

        // animation fade out
        StartCoroutine(FadeOutAndDestroy(graphicSlot, 1f));
    } // createGraphicSlot()

    // animation fade out
    private IEnumerator FadeOutAndDestroy(GameObject graphicSlot, float duration)
    {
        // on attends 1sec
        yield return new WaitForSeconds(5);


        CanvasGroup canvasGroup = graphicSlot.AddComponent<CanvasGroup>();
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            canvasGroup.alpha = 1 - t;
            yield return null; // attends jusqu'à la prochaine frame
        }

        Destroy(graphicSlot);
    } // FadeOutAndDestroy()
}