using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchedulesDirect;
using System.Xml;
using System.Globalization;
using System.Runtime.ExceptionServices;

namespace SDGrabSharp.Common
{
    /// <summary>
    /// Storage for running data from Schedules direct (to prevent overuse of service)
    /// </summary>
    ///
    public class DataCache
    {
        public SDTokenResponse tokenData;
        public SDCountries countryData;
        public Dictionary<string, IEnumerable<SDHeadendsResponse>> headendData;
        public Dictionary<string, SDGetLineupResponse> stationMapData;
        public Dictionary<string, IEnumerable<SDPreviewLineupResponse>> previewStationData;
        private readonly int cacheExpiryHours;

        public DataCache(int cacheExpiryHours)
        {
            this.cacheExpiryHours = cacheExpiryHours;
            Clear();
        }

        private static void AddCacheDateAttribute(XmlElement element, DateTime? cacheDate)
        {
            var cacheAttribute = element.OwnerDocument.CreateAttribute("cachedate");

            cacheAttribute.InnerText = (cacheDate ?? DateTime.UtcNow).ToString("yyyyMMddHHmmss");
            element.Attributes.Append(cacheAttribute);
        }

        private static DateTime? GetCacheDate(XmlNode element)
        {
            if (element.Attributes?["cachedate"] == null) return DateTime.MinValue;
            DateTime elementDate;
            if (DateTime.TryParseExact(element.Attributes["cachedate"].Value, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out elementDate))
                return elementDate;

            return DateTime.MinValue;
        }

        public IEnumerable<SDHeadendsResponse> GetHeadendData(SDJson sd, string country, string postcode)
        {
            var headendKey = $"{country},{postcode}";
            if (!headendData.ContainsKey(headendKey))
            {
                var headendDataJS = sd.GetHeadends(country, postcode);

                if (headendDataJS != null)
                    headendData.Add(headendKey, headendDataJS);

                return headendDataJS;
            }
            else
            {
                // Validate oldest cached value
                var oldestDate = headendData[headendKey].Where(line => line.cacheDate != null).Select(line => line.cacheDate).Min() ?? DateTime.UtcNow;
                if (oldestDate <= DateTime.UtcNow.AddHours(0 - cacheExpiryHours))
                {
                    // Delete cached value and return new value
                    headendData.Remove(headendKey);
                    return GetHeadendData(sd, country, postcode);
                }

                // Otherwise return from cache
                return headendData[headendKey];
            }
        }

        public SDCountries GetCountryData(SDJson sd)
        {
            if (countryData == null || (countryData.cacheDate ?? DateTime.UtcNow) <= DateTime.UtcNow.AddHours(0 - cacheExpiryHours))
                countryData = sd.GetCountries();

            return countryData;
        }

        public SDGetLineupResponse GetLineupData(SDJson sd, string lineup)
        {
            if (!stationMapData.ContainsKey(lineup))
            {
                var thisMap = sd.GetLineup(lineup, true);
                if (thisMap != null)
                {
                    stationMapData.Add(lineup, thisMap);
                    return thisMap;
                }
            }
            else
            {
                SDGetLineupResponse map = null;
                if (stationMapData.ContainsKey(lineup))
                {
                    map = stationMapData[lineup];

                    // Validate cache is in date. If not replace it fresh
                    if ((map.cacheDate ?? DateTime.MinValue) <= DateTime.UtcNow.AddHours(0 - cacheExpiryHours))
                    {
                        stationMapData.Remove(lineup);
                        return GetLineupData(sd, lineup);
                    }
                }

                return map;
            }
            return null;
        }

        public IEnumerable<SDPreviewLineupResponse> GetLineupPreview(SDJson sd, string lineup)
        {
            if (!previewStationData.ContainsKey(lineup))
            {
                var thisPreview = sd.GetLineupPreview(lineup);
                if (thisPreview != null)
                {
                    previewStationData.Add(lineup, thisPreview);
                    return thisPreview;
                }
            }
            else
            {
                var preview = previewStationData[lineup];

                // Validate cache is in date. If not replace it fresh
                var oldestDate = preview.Where(line => line.cacheDate != null).Select(line => line.cacheDate).Min() ?? DateTime.UtcNow;
                if (oldestDate <= DateTime.UtcNow.AddHours(0 - cacheExpiryHours))
                {
                    previewStationData.Remove(lineup);
                    return GetLineupPreview(sd, lineup);
                }

                return preview;
            }            
            return null;
        }

        public void Clear()
        {
            tokenData = null;
            countryData = null;
            stationMapData = new Dictionary<string, SDGetLineupResponse>();
            headendData = new Dictionary<string, IEnumerable<SDHeadendsResponse>>();
            previewStationData = new Dictionary<string, IEnumerable<SDPreviewLineupResponse>>();
        }

        public void Save(string filename)
        {
            var cacheXml = new XmlDocument();
            var rootXmlNode = cacheXml.CreateXmlDeclaration("1.0", "UTF-8", null);
            cacheXml.InsertBefore(rootXmlNode, cacheXml.DocumentElement);

            var rootCacheNode = cacheXml.CreateElement("SDGrabSharpCache");

            // Set save date attribute
            rootCacheNode.SetAttribute("cache-save-date", DateTime.UtcNow.ToString("yyyyMMddHHmmss"));

            // add Root node to document
            cacheXml.AppendChild(rootCacheNode);

            // Write Countries
            if (countryData != null && countryData.Continents.Count > 0)
            {
                var countryRoot = cacheXml.CreateElement("CountryData");
                AddCacheDateAttribute(countryRoot, countryData.cacheDate);

                foreach (var continent in countryData.Continents)
                {
                    var continentNode = cacheXml.CreateElement("continent");
                    continentNode.SetAttribute("name", continent.ContinentName);
                    AddCacheDateAttribute(continentNode, continent.cacheDate);

                    foreach (var country in continent.Countries)
                    {
                        var countryNode = cacheXml.CreateElement("country");
                        AddCacheDateAttribute(countryNode, country.cacheDate);
                        if (country.ShortName != null)
                            countryNode.SetAttribute("shortname", country.ShortName);
                        if (country.PostalCodeExample != null)
                            countryNode.SetAttribute("postalCodeExample", country.PostalCodeExample);
                        if (country.PostalCode != null)
                            countryNode.SetAttribute("postalCode", country.PostalCode);
                        countryNode.SetAttribute("onePostalCode", country.OnePostalCode ? "true" : "false");
                        if (country.FullName != null)
                            countryNode.InnerText = country.FullName;

                        continentNode.AppendChild(countryNode);
                    }
                    countryRoot.AppendChild(continentNode);
                }
                rootCacheNode.AppendChild(countryRoot);
            }

            // Write headend Data
            if (headendData != null && headendData.Count > 0)
            {
                var headEndRoot = cacheXml.CreateElement("HeadEndData");
                foreach (var headendCombo in headendData)
                {
                    var splitKey = headendCombo.Key.Split(',');
                    var headEndListNode = cacheXml.CreateElement("HeadEndList");
                    headEndListNode.SetAttribute("country", splitKey[0]);
                    headEndListNode.SetAttribute("postcode", splitKey[1]);
                    var headendData = headendCombo.Value;
                    foreach (var headEnd in headendCombo.Value)
                    {
                        var headEndNode = cacheXml.CreateElement("HeadEnd");
                        AddCacheDateAttribute(headEndNode, headEnd.cacheDate);
                        if (headEnd.Headend != null)
                            headEndNode.SetAttribute("headend", headEnd.Headend);
                        if (headEnd.Location != null)
                            headEndNode.SetAttribute("location", headEnd.Location);
                        if (headEnd.Transport != null)
                            headEndNode.SetAttribute("transport", headEnd.Transport);
                        foreach (var lineup in headEnd.Lineups)
                        {
                            var lineUpNode = cacheXml.CreateElement("LineUp");
                            AddCacheDateAttribute(lineUpNode, lineup.cacheDate);
                            if (lineup.Lineup != null)
                                lineUpNode.SetAttribute("lineup", lineup.Lineup);
                            if (lineup.URI != null)
                                lineUpNode.SetAttribute("uri", lineup.URI);
                            if (lineup.Name != null)
                                lineUpNode.InnerText = lineup.Name;

                            headEndNode.AppendChild(lineUpNode);
                        }
                        headEndListNode.AppendChild(headEndNode);
                    }
                    headEndRoot.AppendChild(headEndListNode);
                }
                rootCacheNode.AppendChild(headEndRoot);
            }

            // Write Preview stations
            if (previewStationData != null && previewStationData.Count > 0)
            {
                var lineupPreviewRoot = cacheXml.CreateElement("PreviewLineupData");
                foreach (var lineup in previewStationData.Select(line => line))
                {
                    var lineupNode = cacheXml.CreateElement("PreviewLineup");
                    var oldestDate = lineup.Value.Where(line => line.cacheDate != null).Select(line => line.cacheDate).Min();

                    AddCacheDateAttribute(lineupNode, oldestDate);
                    lineupNode.SetAttribute("lineup", lineup.Key);
                    foreach (var channel in lineup.Value)
                    {
                        var channelNode = cacheXml.CreateElement("Channel");
                        channelNode.SetAttribute("channel", channel.Channel);
                        channelNode.SetAttribute("callsign", channel.Callsign);
                        channelNode.InnerText = channel.Name;
                        lineupNode.AppendChild(channelNode);
                    }

                    lineupPreviewRoot.AppendChild(lineupNode);
                }

                rootCacheNode.AppendChild(lineupPreviewRoot);
            }

            // Write stations
            if (stationMapData != null && stationMapData.Count > 0)
            {
                var stationMapRoot = cacheXml.CreateElement("LineUpData");
                foreach (var stationMapCombo in stationMapData)
                {
                    var stationMapNode = cacheXml.CreateElement("LineUp");
                    AddCacheDateAttribute(stationMapNode, stationMapCombo.Value.cacheDate);
                    stationMapNode.SetAttribute("lineup", stationMapCombo.Key);

                    // Maps first
                    foreach (var map in stationMapCombo.Value.Map)
                    {
                        var mapNode = cacheXml.CreateElement("Map");
                        AddCacheDateAttribute(mapNode, map.cacheDate);
                        mapNode.SetAttribute("atscMajor", map.AtscMajor.ToString());
                        mapNode.SetAttribute("atscMinor", map.AtscMinor.ToString());
                        if (map.Channel != null)
                            mapNode.SetAttribute("channel", map.Channel);
                        if (map.DeliverySystem != null)
                            mapNode.SetAttribute("deliverySystem", map.DeliverySystem);
                        if (map.Fec != null)
                            mapNode.SetAttribute("fec", map.Fec);
                        mapNode.SetAttribute("frequencyHz", map.FrequencyHz.ToString());
                        if (map.LogicalChannelNumber != null)
                            mapNode.SetAttribute("logicalChannelNumber", map.LogicalChannelNumber);
                        if (map.MatchType != null)
                            mapNode.SetAttribute("matchType", map.MatchType);
                        if (map.ModulationSystem != null)
                            mapNode.SetAttribute("modulationSystem", map.ModulationSystem);
                        if (map.NetworkID != null)
                            mapNode.SetAttribute("networkID", map.NetworkID);
                        if (map.Polarization != null)
                            mapNode.SetAttribute("polarization", map.Polarization);
                        if (map.ProviderCallsign != null)
                            mapNode.SetAttribute("providerCallsign", map.ProviderCallsign);
                        if (map.ServiceID != null)
                            mapNode.SetAttribute("serviceID", map.ServiceID);
                        if (map.StationID != null)
                            mapNode.SetAttribute("stationID", map.StationID);
                        mapNode.SetAttribute("symbolrate", map.Symbolrate.ToString());
                        if (map.TransportID != null)
                            mapNode.SetAttribute("transportID", map.TransportID);
                        mapNode.SetAttribute("uhfVhf", map.UhfVhf.ToString());
                        stationMapNode.AppendChild(mapNode);
                    }

                    // Station
                    foreach (var station in stationMapCombo.Value.Stations)
                    {
                        var stationNode = cacheXml.CreateElement("Station");
                        AddCacheDateAttribute(stationNode, station.cacheDate);
                        stationNode.SetAttribute("affiliate", station.Affiliate);
                        stationNode.SetAttribute("id", station.StationID);
                        stationNode.SetAttribute("callsign", station.Callsign);
                        stationNode.SetAttribute("name", station.Name);
                        foreach (var lang in station.BroadcastLanguage)
                        {
                            var languageNode = cacheXml.CreateElement("BroadcastLanguage");
                            languageNode.InnerText = lang;
                            stationNode.AppendChild(languageNode);
                        }

                        foreach (var lang in station.DescriptionLanguage)
                        {
                            var languageNode = cacheXml.CreateElement("DescriptionLanguage");
                            languageNode.InnerText = lang;
                            stationNode.AppendChild(languageNode);
                        }

                        if (station.Broadcaster != null)
                        {
                            var broadcasterNode = cacheXml.CreateElement("Broadcaster");
                            broadcasterNode.SetAttribute("city", station.Broadcaster.City);
                            broadcasterNode.SetAttribute("state", station.Broadcaster.State);
                            broadcasterNode.SetAttribute("postalcode", station.Broadcaster.Postalcode);
                            broadcasterNode.SetAttribute("country", station.Broadcaster.Country);
                            stationNode.AppendChild(broadcasterNode);
                        }

                        if (station.Logo != null)
                        {
                            var logoNode = cacheXml.CreateElement("Logo");
                            logoNode.SetAttribute("url", station.Logo.URL);
                            logoNode.SetAttribute("height", station.Logo.Height.ToString());
                            logoNode.SetAttribute("width", station.Logo.Width.ToString());
                            logoNode.SetAttribute("md5", station.Logo.MD5);
                            stationNode.AppendChild(logoNode);
                        }
                        stationMapNode.AppendChild(stationNode);
                    }

                    // Metadata
                    if (stationMapCombo.Value.Metadata != null)
                    {
                        var metaNode = cacheXml.CreateElement("MetaData");
                        metaNode.SetAttribute("lineup", stationMapCombo.Value.Metadata.Lineup);
                        metaNode.SetAttribute("modified", stationMapCombo.Value.Metadata.Modified.GetValueOrDefault().ToString("yyyyMMddHHmmss"));
                        metaNode.SetAttribute("transport", stationMapCombo.Value.Metadata.Transport);
                        metaNode.SetAttribute("modulation", stationMapCombo.Value.Metadata.Modulation);
                        stationMapNode.AppendChild(metaNode);
                    }
                    stationMapRoot.AppendChild(stationMapNode);
                }
                rootCacheNode.AppendChild(stationMapRoot);
            }

            // Save
            cacheXml.Save(filename);
        }

        private bool validateCacheDate(DateTime? cacheDateTime)
        {
            if (cacheDateTime == null || cacheDateTime.Value.AddHours(cacheExpiryHours) >= DateTime.UtcNow)
                return true;

            return false;
        }

        public bool Load(string filename)
        {
            if (!File.Exists(filename))
                return false;

            Clear();
            var cacheDoc = new XmlDocument();
            cacheDoc.Load(filename);

            var rootNode = cacheDoc.SelectSingleNode("//SDGrabSharpCache");

            if (rootNode == null)
                return false;

            var countryDataNode = rootNode.SelectSingleNode("CountryData");
            var headendDataNode = rootNode.SelectSingleNode("HeadEndData");
            var lineupDataNode = rootNode.SelectSingleNode("LineUpData");
            var previewDataNode = rootNode.SelectSingleNode("PreviewLineupData");

            // Country data
            if (countryDataNode != null)
            {
                countryData = new SDCountries {cacheDate = GetCacheDate(countryDataNode)};

                if (validateCacheDate(countryData.cacheDate))
                {
                    var continentNodes = countryDataNode.SelectNodes("continent");
                    foreach (XmlNode continentNode in continentNodes)
                    {
                        var thisContinent = new SDCountries.Continent
                        {
                            cacheDate = GetCacheDate(continentNode),
                            ContinentName = continentNode.Attributes["name"].Value
                        };

                        if (validateCacheDate(thisContinent.cacheDate))
                        {
                            var countryNodes = continentNode.SelectNodes("country");
                            foreach (XmlNode countryNode in countryNodes)
                            {
                                var thisCountry = new SDCountries.Country {cacheDate = GetCacheDate(countryNode)};
                                if (validateCacheDate(thisCountry.cacheDate))
                                {
                                    if (countryNode.Attributes["shortname"] != null)
                                        thisCountry.ShortName = countryNode.Attributes["shortname"].Value;
                                    if (countryNode.Attributes["postalCodeExample"] != null)
                                        thisCountry.PostalCodeExample =
                                            countryNode.Attributes["postalCodeExample"].Value;
                                    if (countryNode.Attributes["postalCode"] != null)
                                        thisCountry.PostalCode = countryNode.Attributes["postalCode"].Value;
                                    if (countryNode.Attributes["onePostalCode"] != null)
                                        thisCountry.OnePostalCode =
                                            countryNode.Attributes["onePostalCode"].Value == "true";
                                    if (countryNode.InnerText != null)
                                        thisCountry.FullName = countryNode.InnerText;
                                    thisContinent.Countries.Add(thisCountry);
                                }
                            }

                            countryData.Continents.Add(thisContinent);
                        }
                    }
                }
            }

            // Head end data
            if (headendDataNode != null)
            {
                foreach (XmlNode headEndListNode in headendDataNode.SelectNodes("HeadEndList"))
                {
                    var headEnds = new List<SDHeadendsResponse>();
                    var thisKey =
                        $"{headEndListNode.Attributes["country"].Value},{headEndListNode.Attributes["postcode"].Value}";

                    foreach (XmlNode headEndNode in headEndListNode.SelectNodes("HeadEnd"))
                    {
                        var lineUpList = new List<SDHeadendsResponse.SDLineup>();
                        var thisHeadEnd = new SDHeadendsResponse {cacheDate = GetCacheDate(headEndNode)};
                        if (!validateCacheDate(thisHeadEnd.cacheDate)) continue;
                        if (headEndNode.Attributes["headend"] != null)
                            thisHeadEnd.Headend = headEndNode.Attributes["headend"].Value;
                        if (headEndNode.Attributes["location"] != null)
                            thisHeadEnd.Location = headEndNode.Attributes["location"].Value;
                        if (headEndNode.Attributes["transport"] != null)
                            thisHeadEnd.Transport = headEndNode.Attributes["transport"].Value;

                        foreach (XmlNode lineUpNode in headEndNode.SelectNodes("LineUp"))
                        {
                            var thisLineup = new SDHeadendsResponse.SDLineup {cacheDate = GetCacheDate(lineUpNode)};
                            if (!validateCacheDate(thisLineup.cacheDate)) continue;
                            if (lineUpNode.Attributes["lineup"] != null)
                                thisLineup.Lineup = lineUpNode.Attributes["lineup"].Value;
                            if (lineUpNode.Attributes["uri"] != null)
                                thisLineup.URI = lineUpNode.Attributes["uri"].Value;
                            if (lineUpNode.InnerText != null)
                                thisLineup.Name = lineUpNode.InnerText;
                            lineUpList.Add(thisLineup);
                        }

                        thisHeadEnd.Lineups = lineUpList.ToArray();
                        headEnds.Add(thisHeadEnd);
                    }

                    headendData.Add(thisKey, headEnds);
                }
            }

            if (previewDataNode != null)
            {
                foreach (XmlNode previewLineupNode in previewDataNode.SelectNodes("PreviewLineup"))
                {
                    var lineup = previewLineupNode?.Attributes["lineup"]?.Value ?? String.Empty;
                    var previewList = new List<SDPreviewLineupResponse>();
                    foreach (XmlNode channelNode in previewLineupNode.SelectNodes("Channel"))
                    {
                        var channel = new SDPreviewLineupResponse {cacheDate = GetCacheDate(previewLineupNode)};
                        if (validateCacheDate(channel.cacheDate))
                        {
                            channel.Channel = channelNode?.Attributes["channel"]?.Value ?? string.Empty;
                            channel.Callsign = channelNode?.Attributes["callsign"]?.Value ?? string.Empty;
                            channel.Name = channelNode.InnerText ?? string.Empty;
                            previewList.Add(channel);
                        }
                    }
                    previewStationData.Add(lineup, previewList);
                }
            }

            // Map/Station/Metadata
            if (lineupDataNode != null)
            {
                //stationMapData = new Dictionary<string, SDGetLineupResponse>();
                foreach (XmlNode lineUpNode in lineupDataNode.SelectNodes("LineUp"))
                {
                    var thisKey = lineUpNode.Attributes["lineup"].Value;
                    var thisStationMap = new SDGetLineupResponse {cacheDate = GetCacheDate(lineUpNode)};

                    if (validateCacheDate(thisStationMap.cacheDate))
                    {
                        // Maps
                        var mapList = new List<SDGetLineupResponse.SDLineupMap>();
                        foreach (XmlNode mapNode in lineUpNode.SelectNodes("Map"))
                        {
                            var thisMap = new SDGetLineupResponse.SDLineupMap {cacheDate = GetCacheDate(mapNode)};
                            if (validateCacheDate(thisMap.cacheDate))
                            {
                                if (mapNode.Attributes["atscMajor"] != null)
                                    thisMap.AtscMajor = int.Parse(mapNode.Attributes["atscMajor"].Value);
                                if (mapNode.Attributes["atscMinor"] != null)
                                    thisMap.AtscMinor = int.Parse(mapNode.Attributes["atscMinor"].Value);
                                if (mapNode.Attributes["channel"] != null)
                                    thisMap.Channel = mapNode.Attributes["channel"].Value;
                                if (mapNode.Attributes["deliverySystem"] != null)
                                    thisMap.DeliverySystem = mapNode.Attributes["deliverySystem"].Value;
                                if (mapNode.Attributes["fec"] != null)
                                    thisMap.Fec = mapNode.Attributes["fec"].Value;
                                if (mapNode.Attributes["frequencyHz"] != null)
                                    thisMap.FrequencyHz = UInt64.Parse(mapNode.Attributes["frequencyHz"].Value);
                                if (mapNode.Attributes["logicalChannelNumber"] != null)
                                    thisMap.LogicalChannelNumber = mapNode.Attributes["logicalChannelNumber"].Value;
                                if (mapNode.Attributes["matchType"] != null)
                                    thisMap.MatchType = mapNode.Attributes["matchType"].Value;
                                if (mapNode.Attributes["modulationSystem"] != null)
                                    thisMap.ModulationSystem = mapNode.Attributes["modulationSystem"].Value;
                                if (mapNode.Attributes["networkID"] != null)
                                    thisMap.NetworkID = mapNode.Attributes["networkID"].Value;
                                if (mapNode.Attributes["polarization"] != null)
                                    thisMap.Polarization = mapNode.Attributes["polarization"].Value;
                                if (mapNode.Attributes["providerCallsign"] != null)
                                    thisMap.ProviderCallsign = mapNode.Attributes["providerCallsign"].Value;
                                if (mapNode.Attributes["serviceID"] != null)
                                    thisMap.ServiceID = mapNode.Attributes["serviceID"].Value;
                                if (mapNode.Attributes["stationID"] != null)
                                    thisMap.StationID = mapNode.Attributes["stationID"].Value;
                                if (mapNode.Attributes["symbolrate"] != null)
                                    thisMap.Symbolrate = int.Parse(mapNode.Attributes["symbolrate"].Value);
                                if (mapNode.Attributes["transportID"] != null)
                                    thisMap.TransportID = mapNode.Attributes["transportID"].Value;
                                if (mapNode.Attributes["uhfVhf"] != null)
                                    thisMap.UhfVhf = int.Parse(mapNode.Attributes["uhfVhf"].Value);
                                mapList.Add(thisMap);
                            }
                        }

                        // Stations
                        var stationList =
                            new List<SDGetLineupResponse.SDLineupStation>();
                        foreach (XmlNode stationNode in lineUpNode.SelectNodes("Station"))
                        {
                            var thisStation = new SDGetLineupResponse.SDLineupStation
                            {
                                cacheDate = GetCacheDate(stationNode)
                            };
                            if (validateCacheDate(thisStation.cacheDate))
                            {
                                if (stationNode.Attributes["affiliate"] != null)
                                    thisStation.Affiliate = stationNode.Attributes["affiliate"].Value;
                                if (stationNode.Attributes["id"] != null)
                                    thisStation.StationID = stationNode.Attributes["id"].Value;
                                if (stationNode.Attributes["callsign"] != null)
                                    thisStation.Callsign = stationNode.Attributes["callsign"].Value;
                                if (stationNode.Attributes["name"] != null)
                                    thisStation.Name = stationNode.Attributes["name"].Value;

                                var broadcastLanguages = new List<string>();
                                foreach (XmlNode broadcastLanguageNode in stationNode.SelectNodes("BroadcastLanguage"))
                                    broadcastLanguages.Add(broadcastLanguageNode.InnerText);
                                thisStation.BroadcastLanguage = broadcastLanguages.ToArray();

                                var descriptionLanguages = new List<string>();
                                foreach (XmlNode descriptionLanguageNode in stationNode.SelectNodes(
                                    "DescriptionLanguage"))
                                    descriptionLanguages.Add(descriptionLanguageNode.InnerText);
                                thisStation.DescriptionLanguage = descriptionLanguages.ToArray();

                                var broadcasterNode = stationNode.SelectSingleNode("Broadcaster");
                                if (broadcasterNode != null)
                                {
                                    thisStation.Broadcaster =
                                        new SDGetLineupResponse.SDLineupStation.SDStationBroadcaster
                                        {
                                            City = broadcasterNode.Attributes["city"].Value,
                                            State = broadcasterNode.Attributes["state"].Value,
                                            Postalcode = broadcasterNode.Attributes["postalcode"].Value,
                                            Country = broadcasterNode.Attributes["country"].Value
                                        };
                                }

                                var logoNode = stationNode.SelectSingleNode("Logo");
                                if (logoNode != null)
                                {
                                    thisStation.Logo = new SDGetLineupResponse.SDLineupStation.SDStationLogo
                                    {
                                        URL = logoNode.Attributes["url"].Value,
                                        Height = int.Parse(logoNode.Attributes["height"].Value),
                                        Width = int.Parse(logoNode.Attributes["width"].Value),
                                        MD5 = logoNode.Attributes["md5"].Value
                                    };
                                }

                                stationList.Add(thisStation);
                            }
                        }

                        thisStationMap.Map = mapList.ToArray();
                        thisStationMap.Stations = stationList.ToArray();

                        // Metadata
                        var metadataNode = lineUpNode.SelectSingleNode("MetaData");
                        if (metadataNode != null)
                        {
                            thisStationMap.Metadata = new SDGetLineupResponse.SDLineupMetadata
                            {
                                Lineup = metadataNode.Attributes["lineup"].Value,
                                Modified = DateTime.ParseExact(
                                    metadataNode.Attributes["modified"].Value, "yyyyMMddHHmmss",
                                    CultureInfo.InvariantCulture),
                                Transport = metadataNode.Attributes["transport"].Value,
                                Modulation = metadataNode.Attributes["modulation"].Value
                            };
                        }

                        stationMapData.Add(thisKey, thisStationMap);
                    }
                }
            }
            return true;
        }
    }
}
