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

using Server.Network;
using System;

namespace Server.Items
{
    [FlipableAttribute(0xF95, 0xF96, 0xF97, 0xF98, 0xF99, 0xF9A, 0xF9B, 0xF9C)]
    public class BoltOfCloth : Item, IScissorable, IDyable, ICommodity
    {
        string ICommodity.Description
        {
            get
            {
                return string.Format(Amount == 1 ? "{0} bolt of cloth" : "{0} bolts of cloth", Amount);
            }
        }

        [Constructable]
        public BoltOfCloth()
            : this(1)
        {
        }

        [Constructable]
        public BoltOfCloth(int amount)
            : base(0xF95)
        {
            Stackable = true;
            Weight = 5.0;
            Amount = amount;
        }

        public BoltOfCloth(Serial serial)
            : base(serial)
        {
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted) return false;

            Hue = sender.DyedHue;

            return true;
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new BoltOfCloth(), amount);
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

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (Deleted || !from.CanSee(this)) return false;

            base.ScissorHelper(scissors, from, new Cloth(), 50);

            return true;
        }

        public override void OnSingleClick(Mobile from)
        {
            int number = (Amount == 1) ? 1049122 : 1049121;

            from.Send(new MessageLocalized(Serial, ItemID, MessageType.Label, 0x3B2, 3, number, "", (Amount * 50).ToString()));
        }
    }
}