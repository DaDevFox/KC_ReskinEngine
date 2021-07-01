using UnityEngine;

[AddComponentMenu("Unique Building Script")]
public class UniqueBuildingScript : Annotation
{
    public override string annotation => $"<b>This script is a placeholder to show that a {scriptName} script would be present here in the game.</b>\n\nThese Scripts are attached to the root GameObject of a Building and are ususally unique to the building containing core functionality this building posseses that no others do. ";
    public string scriptName = "";
}