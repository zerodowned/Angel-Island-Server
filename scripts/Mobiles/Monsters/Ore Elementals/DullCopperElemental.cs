/***************************************************************************
 *
 *   RunUO                   : May 1, 2002
 *   portions copyright      : (C) The RunUO Software Team
 *   email                   : info@runuo.com
 *   
 *   Angel Island UO Shard   : March 25, 2004
 *   portions copyright      : (C) 2004-2024 Tomasello Software LLC.
 *   email                   : luke@tomasello.com
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

/* Scripts/Mobiles/Monsters/Ore Elementals/DullCopperElemental.cs
 * ChangeLog
 *  8/16/06, Rhiannon
 *		Changed speed settings to match SpeedInfo table.
 *	7/26/05, erlein
 *		Automated removal of AoS resistance related function calls. 6 lines removed.
 *  7/30/04, Adam
 *		Adjust loot: Less ore (was hurting miners), add gold
 *  6/5/04, Pix
 *		Merged in 1.0RC0 code.
 */


using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an ore elemental corpse")]
    public class DullCopperElemental : BaseCreature
    {
        [Constructable]
        public DullCopperElemental()
            : base(AIType.AI_Melee, FightMode.All | FightMode.Closest, 10, 1, 0.25, 0.5)
        {
            Name = "a dull copper elemental";
            Body = 110;
            BaseSoundID = 268;

            SetStr(226, 255);
            SetDex(126, 145);
            SetInt(71, 92);

            SetHits(136, 153);

            SetDamage(9, 16);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 20;
        }

        // Auto-dispel is UOR - http://forums.uosecondage.com/viewtopic.php?f=8&t=6901
        public override bool AutoDispel { get { return Core.RuleSets.AutoDispelChance(); } }

        public DullCopperElemental(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            if (Core.RuleSets.AngelIslandRules())
            {
                // 'Spawning == true' blocked in BaseCreature for AngelIslandRules()
                PackGem();
                PackGem();

                // 7/7/21, Adam: add AncientSmithyHammer for player crafted magic items
                if (CoreAI.IsDynamicFeatureSet(CoreAI.FeatureBits.MagicCraftSystem) == true)
                    if (Utility.RandomChance(5))
                        PackItem(new TenjinsHammer(200));

                // adam: Changed from 25-40, to 5-15
                PackItem(new DullCopperOre(Utility.Random(5, 15)));

                // add some gold to make up for the low ore
                PackGold(75, 100);
            }
            else
            {
                if (Core.RuleSets.AllShards)
                {   // https://web.archive.org/web/20010805021023fw_/http://uo.stratics.com/hunters/ore_elem.shtml
                    //  Magic Weapons, Magic Armor, 25 Large Colored Ore of the Elemental's type, Gems, 600 Gold 
                    //	(The Dull Copper Elemental that spawns naturally in Shame only gives 2 Large Dull Copper Ore, 2-3 Gems and 90 - 180 Gold)

                    // if it's on a spawner, it's a naturally spawning elemental
                    //  because we don't know at Spawning time if we are naturally spawning, we punt on 'at spawn' loot and instead manage all loot generation 
                    //  when Spawning==false. Sorry thieves, it's a special case.
                    if (Spawning)
                    {
                        // special case: nothing
                        // we do not yet know if we have a spawner
                    }
                    else
                    {   // we now know if we have a spawner
                        bool naturallySpawning = this.SpawnerLocation != Point3D.Zero;
                        if (naturallySpawning)
                        {
                            PackGold(90, 180);
                            PackItem(new DullCopperOre(2));
                            PackGem(Utility.Random(2, 3));
                        }
                        else
                        {
                            PackGold(600);
                            PackMagicEquipment(1, 2);
                            PackItem(new DullCopperOre(25));
                            PackGem(Utility.Random(3, 5));
                            PackGem(1, .05);
                        }
                    }
                }
                else
                {
                    if (Spawning)
                        PackItem(new DullCopperOre(2/*oreAmount*/));

                    AddLoot(LootPack.Average);
                    AddLoot(LootPack.Gems, 2);
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}