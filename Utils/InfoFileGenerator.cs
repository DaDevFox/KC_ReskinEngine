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

            List<string> completedBuildings = new List<string>();

            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            List<Category> categories = new List<Category>();

            foreach(Type t in types)
            {
                if (t.IsSubclassOf(typeof(Category)))
                    categories.Add(Activator.CreateInstance(t) as Category);
            }

            types = types.OrderBy(
                t =>
                {
                    if (t.IsSubclassOf(typeof(Skin)))
                    {
                        if (t.GetCustomAttribute<CategoryAttribute>() != null)
                        {
                            CategoryAttribute categoryAttribute = t.GetCustomAttribute<CategoryAttribute>();

                            Category category = categories.Find((c) => c.id == categoryAttribute.category);

                            if (category != null)
                            {
                                return category.position;
                            }
                            else
                                return 100;
                        }
                    }

                    return 100;
                }).ToArray();

            List<string> usedCategories = new List<string>();


            foreach (Type type in types)
            {

                if(type.GetCustomAttribute<HiddenAttribute>() != null)
                    continue;

                if (type.IsSubclassOf(typeof(Skin)))
                {
                    if(type.GetCustomAttribute<CategoryAttribute>() != null)
                    {
                        CategoryAttribute categoryAttribute = type.GetCustomAttribute<CategoryAttribute>();

                        Category category = categories.Find((c) => c.id == categoryAttribute.category);


                        if (!usedCategories.Contains(categoryAttribute.category))
                        {
                            string title = $" {new String(SeperatorChar, TitleSeperatorCount)} {category.name} {new String(SeperatorChar, TitleSeperatorCount)} ";
                            result += title + Environment.NewLine;
                            result += new String(SeperatorChar, title.Length);
                            result += Environment.NewLine + Environment.NewLine;

                            usedCategories.Add(categoryAttribute.category);
                        }
                    }


                    Skin skin = Activator.CreateInstance(type) as Skin;

                    if (type.GetCustomAttribute<NotSupportedAttribute>() != null)
                        result += $"[Not Supported]{Environment.NewLine}";

                    result += $"{new String(SeperatorChar, TitleSeperatorCount)} {skin.Name} {new String(SeperatorChar, TitleSeperatorCount)}";
                    result += Environment.NewLine;

                    #region For BuildingSkins

                    if (type.IsSubclassOf(typeof(BuildingSkin)))
                    {
                        BuildingSkin buildingSkin = Activator.CreateInstance(type) as BuildingSkin;


                        result +=
                            $"Name: {buildingSkin.FriendlyName}{Environment.NewLine}" +
                            $"UniqueName: {buildingSkin.UniqueName}{Environment.NewLine}";

                        if (type.GetCustomAttribute<JobsAttribute>() != null)
                            result += $"Jobs: {type.GetCustomAttribute<JobsAttribute>().count}{Environment.NewLine}";


                        #region Gather Info

                        List<ModelAttribute> models = new List<ModelAttribute>();
                        List<AnchorAttribute> anchors = new List<AnchorAttribute>();
                        List<MaterialAttribute> materials = new List<MaterialAttribute>();

                        FieldInfo[] fields = buildingSkin.GetType().GetFields();

                        // models
                        foreach (FieldInfo field in fields)
                        {
                            if (field.GetCustomAttribute<ModelAttribute>() != null)
                            {
                                ModelAttribute attribute = field.GetCustomAttribute<ModelAttribute>();
                                attribute.name = field.Name;

                                if (field.GetCustomAttribute<SeperatorAttribute>() != null)
                                    attribute.seperator = true;

                                if (field.GetCustomAttribute<PresetMaterialAttribute>() != null)
                                    attribute.presetMatName = field.GetCustomAttribute<PresetMaterialAttribute>().name;

                                if (type.GetCustomAttributes<NoteAttribute>() != null && field.GetCustomAttributes<NoteAttribute>().Count() > 0)
                                    attribute.notes = field.GetCustomAttributes<NoteAttribute>().Select((note) => note.description).ToArray();

                                models.Add(attribute);
                            }
                        }

                        // anchors
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

                        // materials
                        foreach (FieldInfo field in fields)
                        {
                            if (field.GetCustomAttribute<MaterialAttribute>() != null)
                            {
                                MaterialAttribute attribute = field.GetCustomAttribute<MaterialAttribute>();
                                attribute.name = field.Name;

                                if (field.GetCustomAttribute<SeperatorAttribute>() != null)
                                    attribute.seperator = true;

                                materials.Add(attribute);
                            }
                        }

                        #endregion

                        // models
                        string modelsText = "";

                        if (models.Count > 0)
                        {
                            result += $"Models:{Environment.NewLine}";

                            for (int i = 0; i < models.Count; i++)
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

                                if (!String.IsNullOrEmpty(model.presetMatName))
                                    modelsText += $"\t[Preset Material ({model.presetMatName})]\n";

                                if (model.notes != null && model.notes.Length > 0)
                                {
                                    modelsText += "\tNotes:\n";
                                    foreach (string note in model.notes)
                                        modelsText += $"\t{note}\n";
                                }


                                modelsText += $"\t{string.Format("{0,-15}{1,15} | {2,8}", model.name + ":", model.type.ToString(), model.description)}{Environment.NewLine}";
                            }
                        }

                        result += modelsText;

                        // anchors
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

                        // materials
                        string materialsText = "";

                        if (materials.Count > 0)
                        {
                            result += $"Materials:{Environment.NewLine}";

                            for (int i = 0; i < materials.Count; i++)
                            {
                                MaterialAttribute material = materials[i];
                                if (material.seperator)
                                {
                                    if (i > 0)
                                        materialsText += "\t" + new String(SeperatorChar, models[i - 1].name.Length);
                                    else
                                        materialsText += "\t" + new String(SeperatorChar, SeperatorDefaultCount);
                                    materialsText += Environment.NewLine;
                                }


                                materialsText += $"\t{string.Format("{0,-15}{1,15}", material.name + ":", material.description)}{Environment.NewLine}";
                            }
                        }

                        result += materialsText;

                        if (type.GetCustomAttributes<NoteAttribute>() != null && type.GetCustomAttributes<NoteAttribute>().Count() > 0)
                        {
                            result += $"Notes:\n";
                            foreach (NoteAttribute note in type.GetCustomAttributes<NoteAttribute>())
                                result += $"\t{note.description}\n";
                        }

                        result += Environment.NewLine;

                        completedBuildings.Add(buildingSkin.UniqueName);
                    }

                    #endregion

                    
                }
                
            }


            bool madeHeader = false;

            foreach(Building b in GameState.inst.internalPrefabs)
            {
                if (!completedBuildings.Contains(b.UniqueName))
                {
                    if (!madeHeader)
                    {
                        string title = $" {new String(SeperatorChar, TitleSeperatorCount)} Unsupported {new String(SeperatorChar, TitleSeperatorCount)} ";
                        result += title + Environment.NewLine;
                        result += new String(SeperatorChar, title.Length);
                        result += Environment.NewLine + Environment.NewLine;

                        madeHeader = true;
                    }


                    result += $"[Not Supported]" + Environment.NewLine +
                        $"{new String(SeperatorChar, TitleSeperatorCount)} {b.FriendlyName} {new String(SeperatorChar, TitleSeperatorCount)}" +
                        Environment.NewLine +
                        $"Name: {b.FriendlyName}{Environment.NewLine}" +
                        $"UniqueName: {b.UniqueName}{Environment.NewLine}" +
                        Environment.NewLine;
                }
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
