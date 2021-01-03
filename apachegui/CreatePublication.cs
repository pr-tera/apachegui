using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace apachegui
{
    class CreatePublication
    {
        public static string VRD = $@"{GetPath.ApacheConfP}\VRD\";
        public static string CFG = $@"{GetPath.ApacheConfP}\CFG\";
        public static void CheckDir(ref bool vrd, ref bool cfg)
        {
            if (!Directory.Exists(VRD))
            {
                try
                {
                    Directory.CreateDirectory(VRD);
                    vrd = true;
                }
                catch
                {
                    vrd = false;
                }
            }
            else
            {
                vrd = true;
            }
            if (!Directory.Exists(CFG))
            {
                try
                {
                    Directory.CreateDirectory(CFG);
                    cfg = true;
                }
                catch
                {
                    cfg = false;
                }
            }
            else
            {
                cfg = true;
            }
        }
        public static void CreateVRD(string alias)
        {
            string filePath = $@"{VRD}{alias}.vrd";
            if (File.Exists(filePath))
            {
                Form1.Message($"{filePath} уже сущестует!");
            }
            else
            {
                try
                {
                    XmlDocument VRDxml = new XmlDocument();
                    XmlDeclaration XmlDec = VRDxml.CreateXmlDeclaration("1.0", "UTF-8", null);
                    VRDxml.AppendChild(XmlDec);
                    XmlElement xPoint = VRDxml.CreateElement("point");

                    XmlAttribute poinAttr = VRDxml.CreateAttribute("xmlns");
                    XmlText pointText = VRDxml.CreateTextNode("http://v8.1c.ru/8.2/virtual-resource-system");

                    XmlAttribute poinAttrXS = VRDxml.CreateAttribute("xmlns:xs");
                    XmlText pointTextXS = VRDxml.CreateTextNode("http://www.w3.org/2001/XMLSchema");

                    XmlAttribute poinAttrXSi = VRDxml.CreateAttribute("xmlns:xsi");
                    XmlText pointTextXSi = VRDxml.CreateTextNode("http://www.w3.org/2001/XMLSchema");

                    XmlAttribute pointAttributeBase = VRDxml.CreateAttribute("Base");
                    XmlText pointTextBase = VRDxml.CreateTextNode($"/{alias}");

                    XmlAttribute pointAttrIb = VRDxml.CreateAttribute("ib");
                    XmlText pointTextib = VRDxml.CreateTextNode(Form1.IbPath);

                    XmlAttribute pointAttrEn = VRDxml.CreateAttribute("enable");
                    XmlText pointTextEn = VRDxml.CreateTextNode("False");

                    if (Form1.Debug == true)
                    {
                        XmlElement xDebug = VRDxml.CreateElement("debug");

                        XmlAttribute debugAttr = VRDxml.CreateAttribute("enable");
                        XmlAttribute debugAttrProt = VRDxml.CreateAttribute("protocol");
                        XmlAttribute debugAttrUrl = VRDxml.CreateAttribute("url");

                        XmlText debugText = VRDxml.CreateTextNode("true");
                        XmlText debugTextProt = VRDxml.CreateTextNode("tcp");
                        XmlText debugTextUrl = VRDxml.CreateTextNode("tcp://localhost");

                        xPoint.AppendChild(xDebug);

                        xDebug.Attributes.Append(debugAttr);
                        xDebug.Attributes.Append(debugAttrProt);
                        xDebug.Attributes.Append(debugAttrUrl);

                        debugAttr.AppendChild(debugText);
                        debugAttrProt.AppendChild(debugTextProt);
                        debugAttrUrl.AppendChild(debugTextUrl);
                    }

                    VRDxml.AppendChild(xPoint);

                    xPoint.Attributes.Append(poinAttr);
                    xPoint.Attributes.Append(poinAttrXS);
                    xPoint.Attributes.Append(poinAttrXSi);
                    xPoint.Attributes.Append(pointAttributeBase);
                    xPoint.Attributes.Append(pointAttrIb);
                    xPoint.Attributes.Append(pointAttrEn);

                    poinAttr.AppendChild(pointText);
                    poinAttrXS.AppendChild(pointTextXS);
                    poinAttrXSi.AppendChild(pointTextXSi);
                    pointAttributeBase.AppendChild(pointTextBase);
                    pointAttrIb.AppendChild(pointTextib);
                    pointAttrEn.AppendChild(pointTextEn);

                    VRDxml.Save(filePath);
                }
                catch
                {
                    Form1.Message("Не удалось создать файл VRD");
                }
            }
        }
        
    }
}
