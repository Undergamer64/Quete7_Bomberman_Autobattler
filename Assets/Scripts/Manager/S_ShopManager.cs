using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class S_ShopManager : MonoBehaviour
{

    public static S_ShopManager Instance;

    [SerializeField]
    private List<GameObject> m_buttons;

    [SerializeField]
    private GameObject m_skipButton;

    [SerializeField]
    private GameObject m_shop;

    [SerializeField]
    private S_Character m_character;

    [SerializeField]
    private List<Sprite> m_upgradeSprites;

    delegate bool ApplyUpgrade(S_Character character);

    private List<ApplyUpgrade> m_upgrades = new ();
    private List<ApplyUpgrade> m_upgradesInShop = new ();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        m_upgrades.Add(NbTrapUpgrade);
        m_upgrades.Add(TrapRangeUpgrade);
        m_upgrades.Add(TrapPerforationUpgrade);
        m_upgrades.Add(LivesUpgrade);
        m_upgrades.Add(SpeedUpgrade);

    }



    public void OpenShop() //triggers the animation
    {
        ChooseUpgrade();
        m_shop.SetActive(true);
    }

    public void ChooseUpgrade()
    {
        for (int i = 0; i < m_buttons.Count; i++)
        {
            int randomUpgrade = UnityEngine.Random.Range(0, m_upgrades.Count);
            m_upgradesInShop.Add(m_upgrades[randomUpgrade]);
            m_buttons[i].GetComponent<Image>().sprite = m_upgradeSprites[randomUpgrade];
        }
        //DO IA HERE

    }

    public void ChangeButtons(bool state) //used as an event for the animator
    {
        for (int i = 0;i < m_buttons.Count; i++)
        {
            m_buttons[i].SetActive(state);
        }
        m_skipButton.SetActive(state);

        if (state)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void Upgrade(int upgradeIndex)
    {
        bool success = false;
        if (upgradeIndex >= 0 && upgradeIndex <= m_buttons.Count-1)
        {
            success = m_upgradesInShop[upgradeIndex](m_character);
        }

        if (success)
        {
            m_upgradesInShop.Clear();
            ChangeButtons(false);

            m_shop.GetComponent<Animator>().SetTrigger("CloseShop");
        }
    }

    public void Skip()
    {
        m_upgradesInShop.Clear();
        ChangeButtons(false);

        m_shop.GetComponent<Animator>().SetTrigger("CloseShop");
    }

    public void ShopClosed()
    {
        m_shop.SetActive(false);
        S_GridManager.Instance.ResetGrid();
        S_RoundManager.Instance.ChangeTimerState(true);
        
    }


    bool NbTrapUpgrade(S_Character character)
    {
        if (character.coins >= 5)
        {
            character.m_NbOfBombs++;
            character.coins -= 5;
            character.UpdateCoinDisplay();
            return true;
        }
        else
        {
            return false;
        }
    }

    bool TrapRangeUpgrade(S_Character character)
    {
        if (character.coins >= 5)
        {
            character.m_BombRange++;
            character.coins -= 5;
            character.UpdateCoinDisplay();
            return true;
        }
        else
        {
            return false;
        }
    }

    bool TrapPerforationUpgrade(S_Character character)
    {
        if (character.coins >= 10)
        {
            character.m_BombPerforation++;
            character.coins -= 10;
            character.UpdateCoinDisplay();
            return true;
        }
        else
        {
            return false;
        }
    }

    bool LivesUpgrade(S_Character character)
    {
        if (character.m_lives < 3)
        {
            if (m_character.coins >= 10)
            {
                character.m_lives++;
                character.coins -= 10;
                character.UpdateCoinDisplay();
                return true;
            }
        }
        return false;
    }

    bool SpeedUpgrade(S_Character character)
    {
        if (character.m_speed > 0.05f)
        {
            if (character.coins >= 10)
            {
                character.m_speed /= 2f;
                character.coins -= 10;
                character.UpdateCoinDisplay();
                return true;
            }
        }
        return false;
    }
}
