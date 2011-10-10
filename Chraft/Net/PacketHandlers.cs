﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using Chraft.Net.Packets;

namespace Chraft.Net
{
    public class PacketHandlers
    {

        private static PacketHandler[] m_Handlers;

        public static PacketHandler[] Handlers
        {
            get { return m_Handlers; }
        }

        static PacketHandlers()
        {
            m_Handlers = new PacketHandler[0x100];

            Register(PacketType.KeepAlive, 5, 0, new OnPacketReceive(ReadKeepAlive));
            Register(PacketType.LoginRequest, 0, 23, new OnPacketReceive(ReadLoginRequest));
            Register(PacketType.Handshake, 0, 3, new OnPacketReceive(ReadHandshake));
            Register(PacketType.ChatMessage, 0, 3, new OnPacketReceive(ReadChatMessage));
            Register(PacketType.UseEntity, 10, 0, new OnPacketReceive(ReadUseEntity));
            Register(PacketType.Respawn, 14, 0, new OnPacketReceive(ReadRespawn));
            Register(PacketType.Player, 2, 0, new OnPacketReceive(ReadPlayer));
            Register(PacketType.PlayerPosition, 34, 0, new OnPacketReceive(ReadPlayerPosition));
            Register(PacketType.PlayerRotation, 10, 0, new OnPacketReceive(ReadPlayerRotation));
            Register(PacketType.PlayerPositionRotation, 42, 0, new OnPacketReceive(ReadPlayerPositionRotation));
            Register(PacketType.PlayerDigging, 12, 0, new OnPacketReceive(ReadPlayerDigging));
            Register(PacketType.PlayerBlockPlacement, 0, 13, new OnPacketReceive(ReadPlayerBlockPlacement));
            Register(PacketType.HoldingChange, 3, 0, new OnPacketReceive(ReadHoldingChange));
            Register(PacketType.Animation, 6, 0, new OnPacketReceive(ReadAnimation));
            Register(PacketType.EntityAction, 6, 0, new OnPacketReceive(ReadEntityAction));
            Register(PacketType.CloseWindow, 2, 0, new OnPacketReceive(ReadCloseWindow));
            Register(PacketType.WindowClick, 0, 10, new OnPacketReceive(ReadWindowClick));
            Register(PacketType.CreativeInventoryAction, 9, 0, ReadCreativeInventoryAction);
            Register(PacketType.ServerListPing, 1, 0, new OnPacketReceive(ReadServerListPing));
            Register(PacketType.Disconnect, 0, 3, new OnPacketReceive(ReadDisconnect));
        }

        public static void Register(PacketType packetID, int length, int minimumLength, OnPacketReceive onReceive)
        {
            m_Handlers[(byte)packetID] = new PacketHandler(packetID, length, minimumLength, onReceive);
        }

        public static PacketHandler GetHandler(PacketType packetID)
        {
            return m_Handlers[(byte)packetID];
        }

        public static void ReadKeepAlive(Client client, PacketReader reader)
        {
            KeepAlivePacket ka = new KeepAlivePacket();
            ka.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketKeepAlive(client, ka);
        }

        public static void ReadLoginRequest(Client client, PacketReader reader)
        {
            LoginRequestPacket lr = new LoginRequestPacket();
            lr.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketLoginRequest(client, lr);
        }

        public static void ReadHandshake(Client client, PacketReader reader)
        {
            HandshakePacket hp = new HandshakePacket();
            hp.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketHandshake(client, hp);
        }

        public static void ReadChatMessage(Client client, PacketReader reader)
        {
            ChatMessagePacket cm = new ChatMessagePacket();
            cm.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketChatMessage(client, cm);
        }

        public static void ReadUseEntity(Client client, PacketReader reader)
        {
            UseEntityPacket ue = new UseEntityPacket();
            ue.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketUseEntity(client, ue);
        }

        public static void ReadRespawn(Client client, PacketReader reader)
        {
            RespawnPacket rp = new RespawnPacket();
            rp.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketRespawn(client, rp);
        }

        public static void ReadPlayer(Client client, PacketReader reader)
        {
            PlayerPacket pp = new PlayerPacket();
            pp.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketPlayer(client, pp);
        }

        public static void ReadPlayerPosition(Client client, PacketReader reader)
        {
            PlayerPositionPacket pp = new PlayerPositionPacket();
            pp.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketPlayerPosition(client, pp);
        }

        public static void ReadPlayerRotation(Client client, PacketReader reader)
        {
            PlayerRotationPacket pr = new PlayerRotationPacket();
            pr.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketPlayerRotation(client, pr);
        }

        public static void ReadPlayerPositionRotation(Client client, PacketReader reader)
        {
            PlayerPositionRotationPacket ppr = new PlayerPositionRotationPacket();
            ppr.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketPlayerPositionRotation(client, ppr);
        }

        public static void ReadPlayerDigging(Client client, PacketReader reader)
        {
            PlayerDiggingPacket pd = new PlayerDiggingPacket();
            pd.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketPlayerDigging(client, pd);
        }

        public static void ReadPlayerBlockPlacement(Client client, PacketReader reader)
        {
            PlayerBlockPlacementPacket pb = new PlayerBlockPlacementPacket();
            pb.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketPlayerBlockPlacement(client, pb);
        }

        public static void ReadHoldingChange(Client client, PacketReader reader)
        {
            HoldingChangePacket hc = new HoldingChangePacket();
            hc.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketHoldingChange(client, hc);
        }

        public static void ReadAnimation(Client client, PacketReader reader)
        {
            AnimationPacket ap = new AnimationPacket();
            ap.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketAnimation(client, ap);
        }

        public static void ReadEntityAction(Client client, PacketReader reader)
        {
            EntityActionPacket ea = new EntityActionPacket();
            ea.Read(reader);

            // TODO: implement this packet
            /*if (!reader.Failed)
                Client.HandlePacketEntityAction(client, ea);*/
        }

        public static void ReadCloseWindow(Client client, PacketReader reader)
        {
            CloseWindowPacket cw = new CloseWindowPacket();
            cw.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketCloseWindow(client, cw);
        }

        public static void ReadWindowClick(Client client, PacketReader reader)
        {
            WindowClickPacket wc = new WindowClickPacket();
            wc.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketWindowClick(client, wc);
        }

        public static void ReadServerListPing(Client client, PacketReader reader)
        {
            ServerListPingPacket sl = new ServerListPingPacket();
            sl.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketServerListPing(client, sl);
        }

        public static void ReadDisconnect(Client client, PacketReader reader)
        {
            DisconnectPacket dp = new DisconnectPacket();
            dp.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketDisconnect(client, dp);
        }

        public static void ReadCreativeInventoryAction(Client client, PacketReader reader)
        {
            CreativeInventoryActionPacket ci = new CreativeInventoryActionPacket();
            ci.Read(reader);

            if (!reader.Failed)
                Client.HandlePacketCreativeInventoryAction(client, ci);
        }
    }
}