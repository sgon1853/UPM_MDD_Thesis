// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SIGEM.Client.Logics.Preferences
{
    public interface IScenarioPrefs
    {
        string Name{get;set;}
        string ToXML();
        void Serialize(XmlWriter writer);
        void Deserialize(XmlNode node, string version);
    }
}

