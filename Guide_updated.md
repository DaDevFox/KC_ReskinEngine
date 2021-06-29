Kingdoms and Castles Reskin Engine Guide | Kingdoms and Castles Modding
=
The KCRE (Kingdoms and Castles Reskin Engine) is an engine that simplifies and standardizes the process of changing visual elements of Kingdoms and Castles through mods. One of the main features of the KCRE is its Unity Plugin that allows artists to create mods with absolutely no coding necessary. 

Modding in Kingdoms and Castles currently - as of Summer 2021 - requires a significant amount of experience in coding (C#) and knowledge of the Unity Engine, which is the game engine the game uses. This significantly increases barriers to entry into the modding community and steepens the learning curve even with some prior experience. 

The broad goal of this engine is to make it easier where possible for people of varying degrees of skill at programming to add art into the game, as a programmer with no knowledge of art can do a fair bit of modding but an artist with no knowledge of programming can do almost nothing.  

![Image](https://media.discordapp.net/attachments/294162953337307138/704799796325515315/Sky_Over_View.png "Amazing Models by TPunko")
*This wonderful blocky scene was made by TPunko from the Kingdoms and Castles Discord in MagicaVoxel*

# How the Engine works
The Engine itself is technically a 'mod' in the strict sense of 'a modification to a game' but not in the traditional sense. It has a [listing](https://steamcommunity.com/sharedfiles/filedetails/?id=2524492692) in the Steam Workshop as a standalone mod that can be subscribed to by any player, however this listing does absolutely nothing by itself. All it does is allow *other* mods to register to it and change visual elements of the game, with a greatly reduced amount of code, research, and thus general headache. Other mods exist like this such as Zat's [ModMenu](https://steamcommunity.com/sharedfiles/filedetails/?id=2071244182&searchtext=ModMenu) and Slooth's [Custom Spells API](https://steamcommunity.com/sharedfiles/filedetails/?id=2256480946&searchtext=API) and [Custom Research API](https://steamcommunity.com/sharedfiles/filedetails/?id=2264448742&searchtext=API), but they all follow a diferent pattern to traditional mods and so I'm using the term 'engine' to describe this rather than 'mod', just for the sake of making a distinction. 

You may notice I said it functions with a *reduced* amount of code, but not none. This might seem to conflict with the main draw of using this engine, 'no coding required', but both are actually true. 

![Image](https://i.ibb.co/Kmjd9fC/Layers-Explanation.png)


Essentially the Engine has 3 layers, but only 2 neccessary ones: it has the base layer, the Engine itself, -  the mod with the steam listing [here](https://steamcommunity.com/sharedfiles/filedetails/?id=2524492692) - and the API, which is a little piece of code that you can put in your own, seperate mod that allows it to communicate with the Engine layer. All mods that run through the engine have some code in them and the API, but there's a tool you can download and use with the Unity Editor that automatically writes the code for you, so you don't have to. Instead of code, it allows you to build your mod through the interfaces, settings, and UI of the Unity Editor.  

![Image](https://i.ibb.co/mv4zwHQ/editor-previe-edited.png)


Associated Links
=
[Steam Page](https://steamcommunity.com/sharedfiles/filedetails/?id=2524492692)

[Github](https://github.com/DaDevFox/KCReskinEngine)






![Image](https://i.ibb.co/yY9Xd9h/Layers-Explanation-descriptive.png)