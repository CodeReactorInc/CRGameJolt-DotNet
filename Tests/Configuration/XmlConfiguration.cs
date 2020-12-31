using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Test.Configuration
{
    public class XmlConfiguration
    {
        public string GameId { get; set; }
        public string GameKey { get; set; }

        public string Username { get; set; }
        public string UserToken { get; set; }

        public string Path { get; private set; }

        public XmlConfiguration(string path)
        {
            Path = path;
            XDocument xdoc = XDocument.Load(path);

            GameId = xdoc.Element("CRGameJolt").Element("Game").Element("ID").Value;
            GameKey = xdoc.Element("CRGameJolt").Element("Game").Element("Key").Value;

            Username = xdoc.Element("CRGameJolt").Element("User").Element("Name").Value;
            UserToken = xdoc.Element("CRGameJolt").Element("User").Element("Token").Value;
        }

        public XmlConfiguration(string gameId, string gameKey, string path)
        {
            Path = path;
            GameId = gameId;
            GameKey = gameKey;
        }

        public void Save()
        {
            using (FileStream fs = new FileStream(Path, FileMode.Create))
            {
                new XDocument(new XElement("CRGameJolt", new XElement[] {
                    new XElement("Game", new XElement[] {
                        new XElement("ID", GameId),
                        new XElement("Key", GameKey)
                    }),
                    new XElement("User", new XElement[] {
                        new XElement("Name", Username),
                        new XElement("Token", UserToken)
                    })
                })).Save(fs);
            }
        }
    }
}
