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

/* Scripts\Items\Weapons\Swords\BoneHarvester.cs
 * ChangeLog
 * 4/1/22, Adam
 *	Added to Vald Dracula as a special rare drop
 */


namespace Server.Items
{
    [FlipableAttribute(0x26BB, 0x26C5)]
    public class BoneHarvester : BaseSword
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ParalyzingBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.MortalStrike; } }

        //		public override int AosStrengthReq{ get{ return 25; } }
        //		public override int AosMinDamage{ get{ return 13; } }
        //		public override int AosMaxDamage{ get{ return 15; } }
        //		public override int AosSpeed{ get{ return 36; } }

        public override int OldMinDamage { get { return 13; } }
        public override int OldMaxDamage { get { return 15; } }
        public override int OldStrengthReq { get { return 25; } }
        public override int OldSpeed { get { return 36; } }

        public override int OldDieRolls { get { return 2; } }
        public override int OldDieMax { get { return 14; } }
        public override int OldAddConstant { get { return 2; } }

        public override int DefHitSound { get { return 0x23B; } }
        public override int DefMissSound { get { return 0x23A; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 70; } }

        [Constructable]
        public BoneHarvester()
            : base(0x26BB)
        {
            Weight = 3.0;
        }

        public BoneHarvester(Serial serial)
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