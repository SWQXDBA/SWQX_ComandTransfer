# SWQX_ComandTransfer

A Torch plugin for converting instructions.
Sometimes we want to set some shortcut instructions.

#For example
!lh, instead of !longhelp
or
!h load instead of! hanger load (although quick instructions are provided in the Quantum hangar plug-in, it is inconvenient to maintain).
#This plug-in can do this.
#This plug-in provides three commands.
#!addtsf [A] [B]: Translate A to B
#For example !Addtsf 123 longhelp ,and then you can enter!123 to get help.
#!alltsf , gets a list of all translated instructions.
#deletetsf [A], Delete the translation of A, for example! deletetsf 123 and then you can't use it!123 to get help.
