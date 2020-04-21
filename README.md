# Hex Virtualization [![MIT license](https://img.shields.io/badge/License-MIT-blue.svg)](https://lbesson.mit-license.org/)
Built with ❤︎ by <a href="https://twitter.com/hexkgg">Hexk</a>
<br><br>

## :guardsman: Why do you need Hex Virtualization ? 

Hex.VM was built so that people can learn from it, for real-world use, other obfuscation techniques such as mutations, controlflow, and renamer would bring this vm to its full potential.

<a href="https://help.gapotchenko.com/eazfuscator.net/30/virtualization#Virtualization_Introduction"> Eazfuscator</a> describes it perfectly:<br>
_"Many of us consider particular pieces of code especially important. May it be a license code check algorithm implementation, an innovative optimization method, or anything else equally important so we would want to protect it by any means possible. As we know, the traditional obfuscation techniques basically do renaming of symbols and encryption, thus leaving the actual algorithms — cycles, conditional branches and arithmetics potentially naked to eye of the skilled intruder._

_Here a radical approach may be useful: to remove all the .NET bytecode instructions from an assembly, and replace it with something completely different and unknown to an external observer, but functionally equivalent to the original algorithm during runtime — this is what the code virtualization actually is."_

<br>

## :star: How does it work ?

- MSIL to VMIL
- Methods are stored as resources
- Bytes are encrypted with xor cipher
<br>

## :fire: What does it do ?

- [x] Virtualizes code into instructions which only Hex.VM can understand
- [x] Has support for a decent amount of opcodes, as said, this is made for educational purposes and as such I believe these opcodes are enough for people to build on
- [x] Easy to use, understand, and build on

<br>

## :bookmark_tabs: Examples
<img width="600" src="https://i.ibb.co/tpCT5wF/dn-Spy-x86-7-Ag-Txej-Zs-X.png" alt="Example">
<img width="600" src="https://i.ibb.co/xzjcB94/dn-Spy-x86-Ceo91j13-Gl.png" alt="Example">



## Resources used
https://www.ecma-international.org/publications/files/ECMA-ST/ECMA-334.pdf <br>
https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes?view=netframework-4.8

_If you got any questions feel free to contact me via discord Hexk#0001_

