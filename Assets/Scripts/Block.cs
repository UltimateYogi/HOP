
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private GameObject diamondPrefab, comboPrefab;

    private GameObject currentDiamond, player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void OnEnable()
    {
        // Randomly spawn a diamond
        if (Random.Range(0, 5) > 0) return;
        currentDiamond = Instantiate(diamondPrefab);
        currentDiamond.transform.position =transform.position +diamondPrefab.transform.position;
        
        
        
    }

    private void Update()
    {
        if (!GameManager.instance.isGamerunning) return;


        float playerPosZ = player.transform.position.z;
        float currentZ = transform.position.z;

        // Return to pool if block is too far behind the player
        if (playerPosZ - currentZ > 15f)
        {
            GameManager.instance.ReturnBlockToPool(this.gameObject);
        }
    }

    private void OnDisable()
    {
        GameManager.instance.updateComboAnimation -= UpdateComboAnimation;

        if (currentDiamond)
        {
            Destroy(currentDiamond);
            currentDiamond = null;
        }
    }

    public void SubscribeToMethod()
    {
        GameManager.instance.updateComboAnimation += UpdateComboAnimation;
    }

    void UpdateComboAnimation(bool isCombo)
    {
        comboPrefab.SetActive(isCombo);
    }
}
