using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace Dvchevskii.Extensions.Configuration.Yaml
{
    public class YamlConfigurationParser
    {
        private const    string            SECTION_DELIMITER = ":";
        private readonly INamingConvention _namingConvention;

        public YamlConfigurationParser(INamingConvention namingConvention)
        {
            _namingConvention = namingConvention;
        }

        public IEnumerable<KeyValuePair<string, string>> Parse(Stream stream)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            using ( StreamReader sr = new StreamReader(stream) )
            {
                YamlStream yamlStream = new YamlStream();
                yamlStream.Load(sr);
                foreach ( YamlDocument document in yamlStream.Documents )
                {
                    IEnumerable<KeyValuePair<string, string>> documentResult =
                    LoadDocument(document.RootNode);
                    foreach ( KeyValuePair<string, string> pair in documentResult )
                    {
                        data[pair.Key] = pair.Value;
                    }
                }

                return data;
            }
        }

        private IEnumerable<KeyValuePair<string, string>> LoadDocument(YamlNode rootNode)
        {
            if ( rootNode is YamlMappingNode mappingNode )
            {
                return VisitMappingNode(mappingNode);
            }

            throw new Exception(
                "Cannot parse configuration document where root node is not a " +
                nameof(YamlMappingNode)
            );
        }

        private IEnumerable<KeyValuePair<string, string>> VisitMappingNode(YamlMappingNode node)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            foreach ( KeyValuePair<YamlNode, YamlNode> pair in node )
            {
                string key = ((YamlScalarNode) pair.Key).Value;

                if ( _namingConvention != null )
                {
                    key = _namingConvention.Apply(key);
                }

                switch (pair.Value)
                {
                    case YamlScalarNode scalarNode:
                        list.Add(new KeyValuePair<string, string>(key, scalarNode.Value));
                        break;

                    case YamlSequenceNode sequenceNode:
                        list.AddRange(PrependSectionName(VisitSequenceNode(sequenceNode), key));
                        break;

                    case YamlMappingNode mappingNode:
                        list.AddRange(PrependSectionName(VisitMappingNode(mappingNode), key));
                        break;
                }
            }

            return list;
        }

        private IEnumerable<KeyValuePair<string, string>> VisitSequenceNode(YamlSequenceNode node)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            int i = 0;
            foreach ( YamlNode child in node.Children )
            {
                switch (child)
                {
                    case YamlScalarNode scalarNode:
                        list.Add(new KeyValuePair<string, string>(i.ToString(), scalarNode.Value));
                        break;

                    case YamlSequenceNode sequenceNode:
                        IEnumerable<KeyValuePair<string, string>> list2 =
                        VisitSequenceNode(sequenceNode);
                        list.AddRange(PrependSectionName(list2, i.ToString()));
                        break;
                    case YamlMappingNode mappingNode:
                        IEnumerable<KeyValuePair<string, string>> list3 =
                        VisitMappingNode(mappingNode);
                        list.AddRange(PrependSectionName(list3, i.ToString()));
                        break;
                }

                i++;
            }

            return list;
        }

        private IEnumerable<KeyValuePair<string, string>> PrependSectionName(
            IEnumerable<KeyValuePair<string, string>> map,
            string sectionName
        ) => map.Select(x => PrependSectionName(x, sectionName));

        private KeyValuePair<string, string> PrependSectionName(
            KeyValuePair<string, string> pair,
            string sectionName
        )
        {
            return new KeyValuePair<string, string>(
                string.Concat(sectionName, SECTION_DELIMITER, pair.Key),
                pair.Value
            );
        }
    }
}
