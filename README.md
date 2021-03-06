# bard-perform-converter-ffxiv
A small tool for the game Final Fantasy XIV: A Realm Reborn where you can simply input music notes for a song, press any key to the rhythm, and it will output XML text to be used with Logitech Gaming Software. Bards rejoice!

_This tool is basically a life-hack that will save you a ton of time (and $$$) on piano lessons._

**DISCLAIMER:** 
- Even if it's all fun and games, I'm pretty sure that Square Enix (the game's developper) is **NOT** ok with this kind of third-party tool. **Use at your own risk!**
- I literally did this tool in an hour and did not bother validating every input options, so it may very well crash on you if you enter invalid data.
- The hotkeys associated with each notes are hardcoded and are from **MY** in-game keybinds, so unless you have the **EXACT** same setup as mine, this tool won't be very useful for you unless you manually change them to yours in the code (I might add an option to personalize keybinds in the future).

Steps to create your own macros with this tool:

1. Find music notes to any song in the correct format (e.g. **D Eb E F# G F# D B-1**).
A good source is https://www.reddit.com/r/ffxiv/wiki/perform
2. Input the notes in the app and click "Next".
3. Press any key on your keyboard to the rhythm of the song.
4. The app outputs an XML string. Keep it aside for now.
5. In the Logitech Gaming Software, create a macro with literally anything inside (doesn't matter, can also be empty), but I suggest giving it the name of the song right now. Save the profile for good measure.
6. Navigate to the directory where all your profiles are saved (usually in C:\Users\YOUR_USER\AppData\Local\Logitech\Logitech Gaming Software\profiles).
7. All your profiles have a unique GUID, but you can easily find the one you're looking for by sorting by date modified (the newest being the profile you juste created a dummy macro for).
8. Open the .xml file for your profile with any text editor.
9. Find the dummy macro you created in step 5. If you did like I mentionned in that step, you can simply search for the name of the song with Ctrl+F (or any name you gave it).
10. Copy the XML string the app gave you as output in step 4 and replace everything INSIDE the "keystroke" tag by pasting it.
11. Save the .xml file and close it.
12. In order for the changes to take effect, restart the Logitech Gaming Software by right-clicking its icon in the taskbar, clicking "Exit" and re-opening it.
13. ???
14. Enjoy the hours of piano lessons you just saved.
