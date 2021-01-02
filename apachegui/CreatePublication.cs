using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public static void CreateVRD()
        { 
            
        }
    }
}
