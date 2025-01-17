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

/* ./Scripts/Items/Armor/Ranger/RangerLegs.cs
 *	ChangeLog :
 *	7/26/05, erlein
 *		Automated removal of AoS resistance related function calls. 5 lines removed.
*/

namespace Server.Items
{
    [FlipableAttribute(0x13da, 0x13e1)]
    public class RangerLegs : BaseArmor
    {

        public override int InitMinHits { get { return 35; } }
        public override int InitMaxHits { get { return 45; } }

        // public override int AosStrReq  { get { return 30; } }
        public override int StrReq { get { return 35; } }

        public override int DexReq { get { return 0; } }
        public override int IntReq { get { return 0; } }
        public override int ArmorBase { get { return 16; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Studded; } }
        public override CraftResource DefaultResource { get { return CraftResource.RegularLeather; } }

        public override int LabelNumber { get { return 1041496; } } // studded leggings, ranger armor

        [Constructable]
        public RangerLegs()
            : base(0x13DA)
        {
            Weight = 3.0;
            Hue = 0x5E4; //0x59C;
        }

        public RangerLegs(Serial serial)
            : base(serial)
        {
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

            if (Weight == 3.0)
                Weight = 5.0;
        }
    }
}