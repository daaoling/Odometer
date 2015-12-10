Odometer for NGUI

Add some polish and pizzazz to your game with an flexible and animated odometer.
Count up distance traveled, enemies killed or even loot earned with this flexible add-on to NGUI.

REQUIRES NGUI 3.5.5 or higher (when UIWrapContent was added).  Requires NGUI example atlas (wooden) for the demo scenes - obviously, you will want to switch out with your own assets.
The actual Odometer asset does NOT require the NGUI examples - only the example/demo scenes I provided.

For most common uses (counter odometer with no ‘thousands’ separators or decimal signs), just drag provided Odometer into your UI Root and call either SetValue() or Increment() function(s) to have the widgit do it’s work!!

Custom options include:

.Color (foreground and background) options for the entire odometer and even digit-level overrides.
.Rollover option to have odometer roll to all zeros when hitting max value - OR you can have odometer ‘stick’ at the max value.
.Automatic scaling of all elements through a single checkbox and height setting.
.Automatic re-sizing border/background
.Ability to use custom font (true type only)
.Ability to tweak ‘springiness’ of the number rotation to make it smooth or ‘springy’.
.Ability to customize each digit element - so as to include comma or period separators or ensure that item doesn’t scroll (such as currency symbol).  

Getting Started
- - - - - - - - 

First, make sure the demo scenes run for you:

1.  Ensure that you have NGUI 3.5.5 or higher (preferably the latest - I specifically tested with 3.6.5) imported into your project.
2.  All the example scenes in Odometer utilize one of the NGUI example atlases (Wooden) - so you should ensure that you have the NGUI examples imported as well. Note that the Odometer asset itself does NOT require the NGUI examples (it has it's own atlas)
3.  Import the Odometer package.  When complete, there should be no errors or warnings.
4.  Try the example scenes (in the OdometerForNGUI folder under Scenes)
5.  The buttons should be pretty self explanatory - mess around with the provided odometers.

Ok - now let’s generate your OWN odometers!!

1.  Create a new scene
3.  Use the NGUI menu item at the top to create a UI Root for your odometer.  NGUI->Create->2D UI
2.  Drag the “Odometer” prefab from the OdometerForNGUI/Prefabs folder onto the UI Root node of the scene.
3.  Hit play and you have a basic odometer (set to all zeros) to work with as we talk about the configurable settings!!

Configuring the Odometer prefab/script
- - - - - - - - - - - - - - - - - - - -

The Odometer prefab is a simple gameobject with the Odometer script/component assigned to it with 2 children - one (called Digit Grid) to hold the digits of the odometer and one (called Border) to house a re-sizable border/background sprite to tie the digits together.

Configurable elements of the Odometer itself (via the Unity editor or programatically) are:

Digit Prefab - This is used for instantiating a provided prefab (called Digit) representing each digit of the odometer.  You probably shouldn’t change this unless you are doing something really fancy and are creating your own special Digit prefabs - which is outside the scope of this document.

Max Value - This determines how many digits will be in the resulting odometer - and more importantly, the maximum value that the prefab can ‘increment’ to without either rolling over or stopping.  The default of 99999 will give a 5 digit odometer.

Rollover - boolean that tells the odometer to roll over to all zeros once Max Value is reached.  Defaulting to true, setting this value to false will cause the odometer to simply stop incrementing once it reaches Max Value

Use Grid - boolean (defaulting to true) that tells the odometer to expect the digits to be placed inside an NGUI UIGrid.  This allows for even spacing and consistent appearance. The only time you might NOT what this to be true is if you were doing a heavily custom odometer where you had some elements of different width or wanted to control the spacing in a unique way.  

Auto Populate - boolean (defaulting to true) that tells the odometer to automatically populate the digits from base Digit prefab (mentioned above).  You might turn this to false (as I did in my currency and thousands separator example scenes) when you want to customize individual digit colors or content.  

Auto Size - boolean (defaulting to true) that tells the odometer to use the Digit Height param (see below) as the basis for auto-sizing all elements of the odometer (including border, background sprites, font size, etc).  You might set this to false if you didn’t like NOT seeing the odometer in scene view in it’s correct size at all times.  You might set all the values exactly as you want them and then turn auto size off.  I COULD have implemented the odometer in an single size and let the user simple scale it (using the game object transform) however they wanted instead of implementing this complex auto-size mechanism.  However, scaling nested NGUI elements has always given me fits - and is problematic (I think) when trying to do pixel-perfect stuffs.  Should only be used if Use Grid (above is true)

Background Color - (default Black) color for the background of the digit number
Font Color - (default White) color for the digit number itself.
Back Background Color (default Black) color for the ‘wheel’ behind the digits that shows when scrolling the digits.
Note that these colors CAN be overridden for each digit if you are doing a custom grid and have Auto Populate (see above) set to false.

Use Border- boolean (defaulting to true) that tells the odometer to calculate and place a border/background around the digits.  This border logic only works if Auto Size and Use Grid are true.  The border is simply a sliced sprite child of the Odometer prefab.  This setting will ‘enable’ it and size/scale.  For a super-custom odometer, you could set this to false and manually enable the border gameobject and size it as necessary. 

Font - Must be a truetype font - one (Arial) comes with Unity (and is used by default).  You can take any TTF font on your system (on windows, there are a bunch in windows\fonts) or from the inter web and place it in your asset folder(s) and use it here.  This will override the default Arial font.  I experimented with NGUI bitmap fonts and could not get away from the blurry issues that occurred with scaling - so I require truetype fonts.  

Springiness - integer (defaulting to 8) that is clamped between 1 and 10.  Essentially it controls how ‘snappy’ the digit rotation is.  If you want smoother, you can try a value like 2.  8 is a good solid value that looks good in all my tests.

Digit Height - integer (defaults to 100) that controls all of the Auto Size logic (see above).  Essentially it represents the total size of an individual digit (not counting the border (if used)

Font Size - integer (defaults to 100). I am using NGUI scale-to-fit settings for the digit labels, so that is why it is being set to an arbitrarily large value.  This setting only propagates to the labels if Auto Size is on.


How to use Odometer for NGUI in you project
- - - - - - - - - - - - - - - - - - - - - -

There are several exposed functions in the Odometer component that you can call from your code to communicate with the Odometer

int getValue ().  Use this to get the current value of the odometer (I have not found this useful - but maybe you might)

void SetValue(int val).  Use this to initialize the odometer to a specific value - or to jump to a specific value.

void IncrementJump (int val).  The preferred way to increase the odometer value.  In my side-scrolling space shooter, I pass a 1 into this function for every meter traveled - and the odometer dutifully scrolls as the ship moves.  You can also give it larger values - and the odometer will ‘jump’ that much (but still scroll the wheels to their final position.  Using this method will cause the odometer to ‘skip’ any intermediate values.

void IncrementSmooth (int val, float delay).  Will add the passed in value to the odometer ‘total’ and will increment (by one) each value in between.  The ‘delay’ param tells how long between each single increment.  Calling this twice (before the first time has had time to finish) will result in wonky values.  I started adding a callback mechanism to let you know when it was ‘done’ - and even contemplating a queueing system - but I decided that this function would only be used in specialty cases (like end of level when adding unused stuff to final score) and I would just let the user beware.  If I get any good feedback, I will revisit this.

If you want to update the ‘springiness’ value at runtime, just changing the value in the Odometer object won’t work - it has to propagate to each digit.  There is a helper function called UpdateOdometerSprigniess() for this.  Same with color changes on the fly - there is an Odometer level and individual Digit level functions called UpdateColors() which will update the underlying color settings if you change the values and then call the function.  I don’t expect these to be used much - I used them for some testing and sample scenes.

Play with the example scenes (and look at the code in OdometerExample.cs) to get a feel for how I used these functions.


Custom Odometers
- - - - - - - - -
 
The base default auto-populated odometer is probably sufficient for most needs (counters and such) - but when you need something more complex, must turn off the auto-populate option and start using the Digit prefab.  Most of the example scenes (all except the BaseDynamicExample scene) use customizations like this and you should take the time to look at how/what they do.

The basics of creating a custom odometer

1.  Drag the Odometer prefab onto your UI Root
2.  Uncheck the ‘Auto Populate’ flag in the editor on the Odometer.
3.  Drag a Digit prefab into the scene and child it to the DigitGrid game object under the Odometer GameObject (in the UI Root tree)
4.  Rename the digit to represent it’s place from left to right.  The name isn’t really important except that the names are in alphabetical order.  In addition to the odometer code which ‘pulls’ the children in this order, it’s also useful so you know which is which - especially if you are customizing individual digits.
5.  Repeat steps 3 and 4 until you have the number of digits you want in your odometer.
6.  Adjust the Max Value on the Odometer object using the editor to represent a value that has the same number of digits os Digit prefabs you have under the DigitGrid.  So if you have 3 Digits - the Max Value might be 999.  NOTE THAT YOU WILL GENERATE AN ERROR IF THE calculated digit count (based on Max Value) does not equal the number of Digit objects under the DigitGrid.
7.  Change ‘something’ on one of the digits to suit your needs.  Read the below section to see how the Digit prefab works


Digit Prefab
- - - - - - -

Essentially a gameobject that has the Odometer Digit script/component on it, this object has a hierarchy of children (including ScrollView and UIWrap Content) culminating in a set of sprites/lables that make up the numbers 0-9 for that digit.  Each of the labels for each numeric value can be overridden (for example to add a prefix (.) period for decimal point or to add a postfix (,) comma for a thousands separator.  You can/should do this only on the digits where you want such elements to occur for your purpose.  For example, I placed a period in front of the digit number in the label text for all the digit labels in the send to last digit - part of showing it as a currency. Again - drill down into the digits and look at the labels in the example scenes.

Other Odometer Digit Settings you can override:

Not a Digit - This destination (defaulting to false - meaning it IS a digit) tells the odometer to include (or exclude) that digit when scrolling and updating.  You can set this to false for elements you want to be auto-sized and included within the border - but aren’t scrollable elements.  This includes, currency symbols (like $ or Euro) or even spaces or you could use an entire digit space for a (,) comma or (.) period if you want.  Not that if you designate Digit prefab as “not a digit”, you should change the labels (or at least the label for the ‘0’ element) to represent the hardened value you want (like $ or whatever).

Override Colors (and the corresponding 3 color fields).  This boolean (defaulting to false) allows you to change the color scheme for a specific digit position.  







