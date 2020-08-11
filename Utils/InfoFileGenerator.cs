using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReskinEngine.API
{
    public class InfoFileGenerator
    {
        public static char SeperatorChar { get; } = '-';
        public static int SeperatorDefaultCount { get; } = 8;
        public static int TitleSeperatorCount { get; } = 2;

        public static string Spacing { get; } = "    ";

        public static string Generate()
        {
            string result = "";

            List<string> completed = new List<string>();

            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {

                if(type.GetCustomAttribute<HiddenAttribute>() != null)
                    continue;


                if (type.IsSubclassOf(typeof(BuildingSkin)))
                {

                    BuildingSkin s = Activator.CreateInstance(type) as BuildingSkin;

                    completed.Add(s.UniqueName);

                    if (type.GetCustomAttribute<NotSupportedAttribute>() != null)
                        result += $"[Not Supported]{Environment.NewLine}";

                    result += $"{new String(SeperatorChar, TitleSeperatorCount)} {s.FriendlyName} {new String(SeperatorChar, TitleSeperatorCount)}";
                    result += Environment.NewLine;
                    result +=
                        $"Name: {s.FriendlyName}{Environment.NewLine}" +
                        $"UniqueName: {s.UniqueName}{Environment.NewLine}";


                    List<ModelAttribute> models = new List<ModelAttribute>();
                    List<AnchorAttribute> anchors = new List<AnchorAttribute>();

                    FieldInfo[] fields = s.GetType().GetFields();
                    foreach(FieldInfo field in fields)
                    {
                        if(field.GetCustomAttribute<ModelAttribute>() != null)
                        {
                            ModelAttribute attribute = field.GetCustomAttribute<ModelAttribute>();
                            attribute.name = field.Name;

                            if (field.GetCustomAttribute<SeperatorAttribute>() != null)
                                attribute.seperator = true;

                            models.Add(attribute);
                        }
                    }

                    foreach (FieldInfo field in fields)
                    {
                        if (field.GetCustomAttribute<AnchorAttribute>() != null)
                        {
                            AnchorAttribute attribute = field.GetCustomAttribute<AnchorAttribute>();
                            attribute.name = field.Name;

                            if (field.GetCustomAttribute<SeperatorAttribute>() != null)
                                attribute.seperator = true;

                            anchors.Add(attribute);
                        }
                    }

                    string modelsText = "";

                    if (models.Count > 0) 
                    {
                        result += $"Models:{Environment.NewLine}";

                        for(int i = 0; i < models.Count; i++)
                        {
                            ModelAttribute model = models[i];
                            if (model.seperator)
                            {
                                if (i > 0)
                                    modelsText += "\t" + new String(SeperatorChar, models[i - 1].name.Length);
                                else
                                    modelsText += "\t" + new String(SeperatorChar, SeperatorDefaultCount);
                                modelsText += Environment.NewLine;
                            }

                            
                            modelsText += $"\t{string.Format("{0,-15}{1,15} | {2,8}", model.name + ":", model.type.ToString(), model.description)}{Environment.NewLine}";
                        }
                    }

                    result += modelsText;

                    string anchorsText = "";

                    if (anchors.Count > 0)
                    {
                        result += $"Anchors:{Environment.NewLine}";

                        for (int i = 0; i < anchors.Count; i++)
                        {
                            AnchorAttribute anchor = anchors[i];
                            if (anchor.seperator)
                            {
                                if (i > 0)
                                    anchorsText += "\t" + new String(SeperatorChar, models[i - 1].name.Length);
                                else
                                    anchorsText += "\t" + new String(SeperatorChar, SeperatorDefaultCount);
                                anchorsText += Environment.NewLine;
                            }


                            anchorsText += $"\t{string.Format("{0,-15}{1,15}", anchor.name + ":", anchor.description)}{Environment.NewLine}";
                        }
                    }

                    result += anchorsText;
                    
                    result += Environment.NewLine;
                }
                
            }

            foreach(Building b in GameState.inst.internalPrefabs)
            {
                if (!completed.Contains(b.UniqueName))
                    result += $"[Not Supported]" + Environment.NewLine + 
                        $"{new String(SeperatorChar, TitleSeperatorCount)} {b.FriendlyName} {new String(SeperatorChar, TitleSeperatorCount)}" +
                        Environment.NewLine +
                        $"Name: {b.FriendlyName}{Environment.NewLine}" +
                        $"UniqueName: {b.UniqueName}{Environment.NewLine}" +
                        Environment.NewLine;
            }






            return result;
        }

        /// <summary>
        /// Aligns each line (seperated by <c>\n</c>) so that the word at index wordIndex is at the same column on each line
        /// </summary>
        /// <param name="toAlign"></param>
        /// <param name="wordIndex"></param>
        /// <returns></returns>
        private static string Align(string toAlign, int wordIndex)
        {
            string[] lines = toAlign.Split('\n');
            int highest = 0;

            // Find rightmost character
            foreach(string line in lines)
            {
                string[] words = line.Split(' ');
                if(words.Length - 1 >= wordIndex)
                {
                    int current = line.IndexOf(words[wordIndex]);
                    if (current > highest)
                        highest = current;
                }
            }

            string[] newLines = new string[lines.Length];

            // Format to fit rightmost character
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] words = line.Split(' ');
                if (words.Length - 1 >= wordIndex)
                {
                    int current = line.IndexOf(words[wordIndex]);
                    if (current == highest)
                        continue;

                    int diff = highest - current;

                    newLines[i] = line;
                    newLines[i].Insert(current - 1, new String(' ', diff));
                }
                else
                    newLines[i] = lines[i];
            }

            // Concatenate new lines into one string
            string newText = "";
            foreach (string newLine in newLines)
                newText += newLine;

            return newText;

        }



    }
}
