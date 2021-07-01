using UnityEngine;

[AddComponentMenu("Unkown Script")]
public class UnknownScript : Annotation
{
    public override string annotation => "<b>This script is a placeholder to show that a script whose purpose is unkown would be present here in the game.</b>\n\nThe KCToolkit is pulled from the game but without all of the script references and therefore all we can see is a missing script, some of them like building colliders and building scripts we as modders can make educated guesses as to where they are but there are some that are as of yet, unknown. This is one of them. ";

    void Start(){
        
    }
}