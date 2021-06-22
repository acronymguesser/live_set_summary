using LiveSetSummary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace LiveSetSummary
{
    internal class LiveSetXmlParser
    {
        public LiveSet ParseLiveSet(XmlDocument xml)
        {
            var trackList = xml
                .SelectNodes("//Ableton/LiveSet/Tracks/*")
                .Cast<XmlNode>()
                .Select(this.ReadTrack)
                .ToArray();

            var masterTrack = xml
                .SelectNodes("//Ableton/LiveSet/MasterTrack")
                .Cast<XmlNode>()
                .Select(this.ReadTrack)
                .First();

            var tempo = xml
                .SelectNodes("//Ableton/LiveSet/MasterTrack")
                .Cast<XmlNode>()
                .Select(this.ReadTempo)
                .First();

            return new LiveSet
            {
                Tracks = trackList.Length > 0 ? trackList : null,
                MasterTrack = masterTrack,
                Tempo = tempo,
            };
        }

        private Track ReadTrack(XmlNode trackNode)
        {
            var trackType = trackNode.Name switch
            {
                "MidiTrack" => TrackType.Midi,
                "AudioTrack" => TrackType.Audio,
                "GroupTrack" => TrackType.Group,
                "MasterTrack" => TrackType.Master,
                _ => TrackType.Unknown,
            };

            var deviceList = trackNode
                .SelectNodes("DeviceChain/DeviceChain/Devices/*")
                .Cast<XmlNode>()
                .Select(ReadTrackDevice)
                .ToArray();

            return new Track
            {
                Type = trackType,
                Name = trackNode.SelectSingleNode("Name/EffectiveName")?.Attributes["Value"]?.InnerText,
                Devices = deviceList.Length > 0 ? deviceList : null,
            };
        }

        private TrackDevice ReadTrackDevice(XmlNode trackDeviceNode)
        {
            var trackDeviceType = trackDeviceNode.Name switch
            {
                "PluginDevice" => TrackDeviceType.Plugin,
                _ => TrackDeviceType.Native,
            };

            string trackDeviceName = trackDeviceType switch
            {
                TrackDeviceType.Plugin => trackDeviceNode.SelectSingleNode("PluginDesc/VstPluginInfo/PlugName")?.Attributes["Value"]?.InnerText,
                TrackDeviceType.Native => $"{trackDeviceNode.Name} (native)",
                _ => throw new NotImplementedException(),
            };

            return new TrackDevice
            {
                Type = trackDeviceType,
                Name = trackDeviceName,
            };
        }
    
    
        private decimal? ReadTempo(XmlNode masterTrackNode)
        {
            var tempo = masterTrackNode
                .SelectSingleNode("DeviceChain/Mixer/Tempo/Manual")?.Attributes["Value"]?.InnerText;

            if (tempo is null)
                return null;

            if (!Decimal.TryParse(tempo, out decimal result))
                return null;

            return result;
        }
    }
}
