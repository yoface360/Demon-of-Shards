using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
        Skill.SetupSkillLists();

        //Unarmed Skills
        Skill punch = new Skill(15f, .25f, .5f, "Punch");
        Skill heavyPunch = new Skill(35f, .25f, 1.5f, "Heavy Punch");
        Skill.unarmedSkillsList.Add(punch);
        Skill.unarmedSkillsList.Add(heavyPunch);

        //Sword Skills
        Skill swordSlash = new Skill(25f, 1.2f, .8f, "Sword Slash");
        Skill doubleSlash = new Skill(60f, 1.2f, 2.3f, "Double Slash");
        Skill.swordSkillsList.Add(swordSlash);
        Skill.swordSkillsList.Add(doubleSlash);

        //Spear Skills
        Skill spearThrust = new Skill(15f, .5f, .75f, "Thrust");
        Skill spearBrace = new Skill(50f, 3f, 6f, "Spear Brace"); //Cannot move during skill, does massive damage while animation is active
        Skill.spearSkillsList.Add(spearThrust);
        Skill.spearSkillsList.Add(spearBrace);

        //Axe Skills
        Skill axeSlash = new Skill(20f, .5f, 1.25f, "Axe Slash");
        Skill axeBleed = new Skill(10f, .5f, 3f, "Deep Cuts"); //Will make an enemy bleed for a time
        Skill.axeSkillsList.Add(axeSlash);
        Skill.axeSkillsList.Add(axeBleed);

        //Mace Skills
        Skill maceBash = new Skill(30f, .5f, 1.5f, "Mace Bash");
        Skill maceStunBash = new Skill(20f, .5f, 2.25f, "Stunning Blow"); //Will stun enemy for a time
        Skill.maceSkillsList.Add(maceBash);
        Skill.maceSkillsList.Add(maceStunBash);

        //Hammer Skills
        Skill hammerBash = new Skill(40f, .75f, 2.25f, "Hammer Bash");
        Skill hammerBoneCrunch = new Skill(30f, .75f, 5.25f, "Bone Crunching Bash"); //Later will cause enemy stats to decrease for a time
        Skill.hammerSkillsList.Add(hammerBash);
        Skill.hammerSkillsList.Add(hammerBoneCrunch);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CalculateDamage(Skill s, int baseLvlDamage, int skillLvlDamage, int skillModifier)
    {
        s.Damage = baseLvlDamage + (skillLvlDamage * skillModifier);
    }
}
