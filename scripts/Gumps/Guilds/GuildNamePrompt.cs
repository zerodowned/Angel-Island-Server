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

using Server.Guilds;
using Server.Prompts;

namespace Server.Gumps
{
    public class GuildNamePrompt : Prompt
    {
        private Mobile m_Mobile;
        private Guild m_Guild;

        public GuildNamePrompt(Mobile m, Guild g)
        {
            m_Mobile = m;
            m_Guild = g;
        }

        public override void OnCancel(Mobile from)
        {
            if (GuildGump.BadLeader(m_Mobile, m_Guild))
                return;

            GuildGump.EnsureClosed(m_Mobile);
            m_Mobile.SendGump(new GuildmasterGump(m_Mobile, m_Guild));
        }

        public override void OnResponse(Mobile from, string text)
        {
            if (GuildGump.BadLeader(m_Mobile, m_Guild))
                return;

            text = text.Trim();

            if (text.Length > 40)
                text = text.Substring(0, 40);

            if (text.Length > 0)
            {
                if (Guild.FindByName(text) != null)
                {
                    m_Mobile.SendMessage("{0} conflicts with the name of an existing guild.", text);
                }
                else
                {
                    m_Guild.Name = text;
                    m_Guild.GuildMessage(1018024, text); // The name of your guild has changed:
                }
            }

            GuildGump.EnsureClosed(m_Mobile);
            m_Mobile.SendGump(new GuildmasterGump(m_Mobile, m_Guild));
        }
    }
}