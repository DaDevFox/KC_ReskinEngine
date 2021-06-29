KCRE Guide | Kingdoms and Castles Modding
=
The KCRE (Kingdoms and Castles Reskin Engine) is an engine that simplifies and standardizes the process of changing visual elements of Kingdoms and Castles through mods. One of the main features of the KCRE is its Unity Tool/Plugin that allows artists to create mods with absolutely no coding necessary. 

Modding in Kingdoms and Castles currently - as of Summer 2021 - requires a significant amount of experience in coding (C#) and knowledge of the Unity Engine, which is the game engine the game uses. This significantly increases barriers to entry into the modding community and steepens the learning curve even with some prior experience. 

The broad goal of this engine is to make it easier where possible for people of varying degrees of skill at programming to add art into the game, as a programmer with no knowledge of art can do a fair bit of modding but an artist with no knowledge of programming can do almost nothing.  

![Image](https://media.discordapp.net/attachments/294162953337307138/704799796325515315/Sky_Over_View.png "Amazing Models by TPunko")
*This wonderful blocky scene was made by TPunko from the Kingdoms and Castles Discord in MagicaVoxel*

# How the Engine works
The Engine itself is technically a 'mod' in the strict sense of 'a modification to a game' but not in the traditional sense. It has a [listing](https://steamcommunity.com/sharedfiles/filedetails/?id=2524492692) in the Steam Workshop as a standalone mod that can be subscribed to by any player, however this listing does absolutely nothing by itself. All it does is allow *other* mods to register to it and change visual elements of the game, with a greatly reduced amount of code, research, and thus general headache. Other mods exist like this such as Zat's [ModMenu](https://steamcommunity.com/sharedfiles/filedetails/?id=2071244182&searchtext=ModMenu) and Slooth's [Custom Spells API](https://steamcommunity.com/sharedfiles/filedetails/?id=2256480946&searchtext=API) and [Custom Research API](https://steamcommunity.com/sharedfiles/filedetails/?id=2264448742&searchtext=API), but they all follow a diferent pattern to traditional mods and so I'm using the term 'engine' to describe this rather than 'mod', just for the sake of making a distinction. 

You may notice I said it functions with a *reduced* amount of code, but not none. This might seem to conflict with the main draw of using this engine: 'no coding required', but both are actually true. 

![Image](https://i.ibb.co/Kmjd9fC/Layers-Explanation.png)

Essentially the Engine has 3 layers, but only 2 neccessary ones: it has the base layer, the Engine itself, - the mod with the steam listing [here](https://steamcommunity.com/sharedfiles/filedetails/?id=2524492692) - and the API, which is a little piece of code that you can put in your own, seperate mod that allows it to communicate with the Engine layer. All mods that run through the engine have some code in them and the API, but there's a tool you can [download](https://github.com/DaDevFox/KCReskinEngine/tree/master/Unity%20Plugin) and use with the Unity Editor that automatically writes the code for you, so you don't have to. Instead of code, it allows you to build your mod through the interfaces, settings, and UI of the Unity Editor.  

Of course this tool is optional and modders experienced in C# can just write the code themselves directly in the `Human-Made Code` + `Mod Files` section of the diagram above utlizing the API (more on this later). 

This does mean potential users of the Unity Tool have to be familiar with the Unity Editor so those looking to get started should look into some tutorials if they are unfamiliar. Note that you only need to learn the interface and none of the scripting aspects of the Editor for this tool (unless you want to of course). 

> If you are looking for tutorials on the Unity Editor I recommend you read through the [Official Unity Documentation](https://docs.unity3d.com/520/Documentation/Manual/GettingStarted.html); it's fairly comprehensive in most areas, especially learning the interface of the editor. 

# Ease of Use

## How much code is necessary?

In the layers diagram all of the red bubbles are code and everything in the Unity layer is done through the UI of the Unity Editor. The entirety of the Unity layer eventually feeds into one red bubble labelled 'Mod Files'. This is the body of your mod and it is automatically generated and will automatically function when subscribed or locally downloaded; so you don't even need to open it. Therefore in the absolute minimalist sense, you don't ever need to even open a code editor, much less write code. 

> The `Human-Made Code` bubble is, of course, optional but if you do want to write code either in addition to the Unity Tool's generated code or just entirely make the mod by yourself, I suggest you look through the [API Reference](https://github.com/DaDevFox/KCReskinEngine/blob/master/API_Reference.md) and the [KC Mod Tutorial](https://modtutorial.kingdomsandcastles.com/). 

If you're thinking, *why in the world would I want to write code when the tool's already doing it for me?* The reason is because it gives far more control and allows you to make specific behaviours that the Engine's standardized processes are too generic for. A good example of this that has been published on the workshop is Skusch's [Day-Night Cycle Mod](https://steamcommunity.com/sharedfiles/filedetails/?id=2013600042&searchtext=day+night+cycle). It was a very early alpha tester of this Engine and used the API code along with a special script Skusch wrote himself to make the windows and street torches of certian buildings glow at night and shut off in the day. 

## UI Summary

## The Interfaces & The Output

So some of the more code-oriented modders might be wondering:
So how do the interfaces correspond to the code that is outputed?

The frame of the mod is built like a traditional mod would be and the skins are pretty much 1:1 transfered from the interface to code as you would expect them to be. Below is an example containing a keep

![Image](https://i.ibb.co/DbDQwvX/keepskinprevie.png)
![Image](https://i.ibb.co/5kqGg6L/keepskincodeprevie.png)
*Both images taken with API & Unity Tool version 0.0.1d*

AssetBundles are automatically built into the multi-platform format the game expects and their paths are written - hardcoded - into the mod code so that it can extract them at runtime. 


Associated Links
=
[Steam Page](https://steamcommunity.com/sharedfiles/filedetails/?id=2524492692)

[Github](https://github.com/DaDevFox/KCReskinEngine)






![Image](https://i.ibb.co/yY9Xd9h/Layers-Explanation-descriptive.png)


image refs:
https://i.ibb.co/hdG3G5D/Churchskincode.png
https://i.ibb.co/1LpSyMX/code-previe.png
https://i.ibb.co/yfQqMRk/editor-previe.png
https://i.ibb.co/fd21Cdw/editor-previe-edited.png
https://i.ibb.co/xSS4GBQ/tree-Skin-Unity.png
https://i.ibb.co/5kqGg6L/keepskincodeprevie.png
https://i.ibb.co/DbDQwvX/keepskinprevie.png