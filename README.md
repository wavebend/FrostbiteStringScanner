# FrostbiteStringScanner

Scans a frostbite game executable for readable ascii or utf16 strings

How does this work?

This program is able to extract strings from the .exe in a way (similar to strings.exe by Sysinternals or strings2.exe) but for both ascii and utf16 (2-bytes encoding) and with extremely small amounts of junk, by leveraging 
- the fact that useful frostbite strings always end with a null terminator (0x00) and not 0x01 or anything below 0x20
- the presence of pointer values scattered throughout that specific section which are easy to detect + skip over, reducing the junk amount as well
- the fact that frostbite games only have useful strings at 0x04 alignment
- the fact that only specific sections of the .exe contain the useful strings
- that some junk strings (even though they are aligned at 0x04) have a particular set of bytes that follow them, so some heuristics to detect that and skip over those

A demo for some titles is provided in program.cs
