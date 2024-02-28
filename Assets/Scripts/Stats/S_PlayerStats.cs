using UnityEngine;

[CreateAssetMenu(fileName = "S_PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class S_PlayerStats : ScriptableObject
{

    [Header("Based stats :")]
    public int m_nbTrap;
    public int m_Range;
    public int m_Perforation;
    public bool m_CanMoveTraps;

/*
    [Header("Particularities :")]
    [ShowCondition("m_HasParticularities")]
    public bool m_CanExplodedWhenKilled;
    [ShowCondition("m_CanExplodedWhenKilled")]
    public int m_ExplosionRadius;

    [ShowCondition("m_HasParticularities")]
    public bool m_CanShootGaz;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (m_CanExplodedWhenKilled && !m_HasParticularities)
        {
            m_CanExplodedWhenKilled = false;
        }
    }
#endif*/
}