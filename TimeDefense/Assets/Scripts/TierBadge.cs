using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TierBadge : MonoBehaviour
{


    public Sprite tier1;
    public Sprite tier2;
    public Sprite tier3;

    public Tower parent;

    // Update is called once per frame
    void Update()
    {


        if (parent.towerType == Tower.TowerType.BASE)
        {

            GetComponent<SpriteRenderer>().enabled = false;

        }
        else
        {

            GetComponent<SpriteRenderer>().enabled = true;

            if (parent.towerTier.Equals(parent.m_Level1))
            {

                GetComponent<SpriteRenderer>().sprite = tier1;


            }
            else if (parent.towerTier.Equals(parent.m_Level2))
            {

                GetComponent<SpriteRenderer>().sprite = tier2;

            }
            else if (parent.towerTier.Equals(parent.m_Level3))
            {


                GetComponent<SpriteRenderer>().sprite = tier3;

            }


        }


    }
}
