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

using System.Collections;

namespace Server.Mobiles
{
    [TypeAlias("Server.Mobiles.Bower")]
    public class Bowyer : BaseVendor
    {
        private ArrayList m_SBInfos = new ArrayList();
        protected override ArrayList SBInfos { get { return m_SBInfos; } }

        [Constructable]
        public Bowyer()
            : base("the bowyer")
        {
            SetSkill(SkillName.Archery, 60.0, 83.0);
            SetSkill(SkillName.Fletching, 90.0, 100.0);
        }

        public override void InitSBInfo()
        {
            //m_SBInfos.Add( new SBRingmailArmor() );
            //m_SBInfos.Add( new SBStuddedArmor() );
            //m_SBInfos.Add( new SBLeatherArmor() );
            m_SBInfos.Add(new SBRangedWeapon());
            m_SBInfos.Add(new SBBowyer());
        }

        public override VendorShoeType ShoeType
        {
            get { return Utility.RandomBool() ? VendorShoeType.Boots : VendorShoeType.ThighBoots; }
        }

        public override int GetShoeHue()
        {
            return 0;
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            AddItem(new Server.Items.Bow());
            AddItem(new Server.Items.LeatherGorget());
        }

        public Bowyer(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}