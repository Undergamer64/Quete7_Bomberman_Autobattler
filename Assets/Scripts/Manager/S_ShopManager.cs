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
    private S_Character m_character;

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



    public void OpenShop()
    {
        //Play animation here

        Time.timeScale = 0;
        for (int i = 0; i < m_buttons.Count-1; i++)
        {
            int randomUpgrade = UnityEngine.Random.Range(0, m_upgrades.Count-1);
            m_upgradesInShop.Add(m_upgrades[randomUpgrade]);
        }

        //temp
        ChangeButtonsEvent(true);
    }

    public void ChangeButtonsEvent(bool state)
    {
        //for the animator
        for (int i = 0;i < m_buttons.Count; i++)
        {
            m_buttons[i].SetActive(state);
        }
    }

    public void Upgrade(int upgradeIndex)
    {
        if (upgradeIndex >= 0 && upgradeIndex <= m_buttons.Count-1)
        {
            m_upgradesInShop[upgradeIndex](m_character);
        }
        m_upgradesInShop.Clear();

        //temp
        ChangeButtonsEvent(false);
        Time.timeScale = 1.0f;
    }

    bool NbTrapUpgrade(S_Character character)
    {
        if (character.coins >= 5)
        {
            character.m_NbOfBombs++;
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
                return true;
            }
        }
        return false;
    }
}
