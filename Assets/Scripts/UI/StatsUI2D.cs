using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI2D : MonoBehaviour
{
    // Inspector Components
    [SerializeField] private RawImage[] hearts;
    [SerializeField] private TextMeshProUGUI scoreUI;
    private int heartCnt;
    private float scoreTimer = 0f;

    // Hide Health
    public void healthBar(int health)
    {
        heartCnt = health;
        switch(heartCnt)
        {
            case 0:
                hearts[0].enabled = false;
                break;
            case 1:
                hearts[1].enabled = false;
                break;
            case 2:
                hearts[2].enabled = false;
                break;
            default:
                break;
        }
    }

    // Update Score In Real Time
    private void Update()
    {
        scoreTimer += Time.deltaTime;

        // Update Score Every Second
        if(scoreTimer > 1f)
        {
            PlayerBase.scoreVal++;
            scoreTimer = 0f;

            // Update Score In UI
            scoreUI.text = "Score: " + PlayerBase.scoreVal.ToString();
        }
    }
}
