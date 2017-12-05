using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WeChat.Common
{
    public class SelectItemConfig
    {
        private XmlDocument _doc;
        private XmlNode _rootNode;

        public SelectItemConfig(String mapPath)
        {
            _doc = new XmlDocument();
            _doc.Load(mapPath + @"SelectItem.config");
            _rootNode = _doc.DocumentElement;
        }

        public void setConfig(String key, String value)
        {
            if (value == null || (value != null && value.Length == 0)) { return; }

            if (_rootNode.SelectSingleNode(key) == null || (_rootNode.SelectSingleNode(key) != null && _rootNode.SelectSingleNode(key).ChildNodes.Count == 0))
            {
                XmlElement newNode = _doc.CreateElement(key);
                newNode.InnerText = value;
                _rootNode.AppendChild(newNode);
            }
            else
            {
                _rootNode.SelectSingleNode(key).ChildNodes[0].InnerText = value;
            }
        }

        /// <summary>
        /// 获取单个节点的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public String getConfig(string key)
        {
            if (_rootNode.SelectSingleNode(key) != null)
                if (_rootNode.SelectSingleNode(key).ChildNodes.Count > 0)
                    return _rootNode.SelectSingleNode(key).ChildNodes[0].Value;
                else
                    return "";
            else
                return null;
        }

        /// <summary>
        /// 获取key值对应的多个节点的值的集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, string> getItemListByKey(string key)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>() { };
            if (_rootNode.SelectSingleNode(key) != null)
            {
                XmlNodeList xmlNode = _rootNode.SelectNodes(key);
                int nodeCount = _rootNode.SelectNodes(key).Count;
                if (nodeCount > 0)
                {
                    for (int i = 0; i < nodeCount; i++)
                    {
                        var item = _rootNode.SelectNodes(key).Item(i);
                        dic.Add(item.Attributes["value"].Value, item.InnerText);//Select 下拉框 Value属性
                    }
                }
            }
            return dic;
        }

        public Boolean SaveConfig(String mapPath)
        {
            try
            {
                _doc.Save(mapPath + @"FlexPaper.config");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
